using CoreWebAPIs.Context;
using CoreWebAPIs.Models;
using CoreWebAPIs.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace CoreWebAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IvrController : ControllerBase
    {
        private readonly PaxFlightsDbContext _airportDb;
        private readonly LoyaltyApiService _loyalty;
        private readonly HttpClient _httpClient;

        public IvrController(PaxFlightsDbContext airportDb, LoyaltyApiService loyalty, HttpClient httpClient)
        {
            _airportDb = airportDb;
            _loyalty = loyalty;
            _httpClient = httpClient;
        }

        // ============================ 
        // 1. Falcon Tier by Membership 
        // ============================ 
        [HttpPost("falcon/tier")]
        public async Task<IActionResult> GetFalconTier([FromBody] FalconTierRequest request)
        {
            var summary = await _loyalty.GetAccountSummaryAsync(request.MembershipNumber);
            if (summary is null || summary.ContainsKey("error"))
                return NotFound(new
                {
                    membershipNumber = request.MembershipNumber,
                    tier = "Unknown",
                    error = summary?["rawBody"]
                });

            return Ok(new
            {
                membershipNumber = summary["membershipNumber"]?.ToString(),
                tier = summary["tierName"]?.ToString()
            });
        }

        public class FalconTierRequest
        {
            public string MembershipNumber { get; set; }
        }

        // ==================== 
        // 2. Customer Details 
        // ==================== 
        [HttpPost("customer/details")]
        public async Task<IActionResult> GetCustomerDetails([FromBody] CustomerDetailsRequest request)
        {
            var summary = await _loyalty.GetAccountSummaryAsync(request.MembershipNumber);
            if (summary is null || summary.ContainsKey("error"))
                return NotFound(new
                {
                    membershipNumber = request.MembershipNumber,
                    error = summary?["rawBody"]
                });

            return Ok(new
            {
                customerName = $"{summary["givenName"]} {summary["familyName"]}",
                tier = summary["tierName"]?.ToString(),
                membershipNumber = summary["membershipNumber"]?.ToString(),
                status = summary["membershipStatus"]?.ToString()
            });
        }

        public class CustomerDetailsRequest
        {
            public string MembershipNumber { get; set; }
        }




        // ==================== 
        // 3.  Get Flight Details
        // ==================== 


        [HttpPost("flights/details")]
        public async Task<IActionResult> GetFlightDetails([FromBody] FlightDetailsRequest request)
        {
            try
            {
                if (!DateTime.TryParseExact(request.FlightDate, "yyyy-MM-dd",
                    CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime flightDate))
                {
                    return BadRequest(new { ErrorMessage = "Invalid date format. Use yyyy-MM-dd" });
                }

                var apiUrl = $"https://gfflightstatus.azurewebsites.net/api/flightStatus/{request.FlightNumber}/{flightDate:dd-MMM-yyyy}";
                var apiResponse = await _httpClient.GetAsync(apiUrl);

                if (!apiResponse.IsSuccessStatusCode)
                {
                    return StatusCode((int)apiResponse.StatusCode, new
                    {
                        Status = "Error",
                        Message = "Unable to fetch flight status from external API"
                    });
                }

                var json = await apiResponse.Content.ReadAsStringAsync();
                using var doc = JsonDocument.Parse(json);

                int flightsAvailable = doc.RootElement.GetProperty("totalFlights").GetInt32();
                bool within48Hours = Math.Abs((flightDate - DateTime.Now).TotalHours) <= 48;

                string disruptionFlag = "N";
                if (doc.RootElement.TryGetProperty("flights", out var flights) && flights.ValueKind == JsonValueKind.Array)
                {
                    var firstFlight = flights[0];

                    string status = firstFlight.TryGetProperty("status", out var st) ? st.GetString() ?? "Unknown" : "Unknown";
                    string currentStatus = firstFlight.TryGetProperty("currentStatus", out var cs) ? cs.GetString() ?? "Unknown" : "Unknown";

                    string statusLower = status.ToLower();
                    string currentStatusLower = currentStatus.ToLower();

                    if (statusLower.Contains("delay") ||
                        statusLower.Contains("cancel") ||
                        currentStatusLower.Contains("delay") ||
                        currentStatusLower.Contains("cancel") ||
                        !statusLower.Equals("on time"))  // anything not exactly "On Time"
                    {
                        disruptionFlag = "Y";
                    }
                }

                var pax = await _airportDb.PaxFlightDetails
                    .Where(f => f.FLT_NR == request.FlightNumber
                             && f.SCH_DEP_DT.HasValue
                             && f.SCH_DEP_DT.Value.Date == flightDate.Date
                             && f.FFPNUM == request.MembershipNumber)
                    .Select(f => f.PNR)
                    .FirstOrDefaultAsync();

                return Ok(new
                {
                    MembershipNumber = request.MembershipNumber,
                    FlightDate = request.FlightDate,
                    PNR = pax ?? "Not Found",
                    FlightsAvailable = flightsAvailable,
                    Within48Hours = within48Hours ? "Y" : "N",
                    Disruption = disruptionFlag
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ErrorMessage = ex.Message });
            }
        }

        // ✅ Request model with flightDate as string
        public class FlightDetailsRequest
        {
            public string FlightNumber { get; set; }
            public string FlightDate { get; set; }   // string instead of DateTime
            public string MembershipNumber { get; set; }
        }


        //// -------------------------------
        //// 5. Accounts Linked
        //// -------------------------------
        //[HttpPost("accounts/linked")]
        //public async Task<ActionResult<AccountsLinkedResponse>> GetAccountsLinked([FromBody] AccountsLinkedRequest request)
        //{
        //    var summary = await _loyalty.GetAccountSummaryAsync(request.MembershipNumber);

        //    if (summary is null || !summary.ContainsKey("expiryDetails"))
        //        return Ok(new AccountsLinkedResponse
        //        {
        //            NumberOfAccounts = 0,
        //            Accounts = new List<AccountInfo>()
        //        });

        //    var expiryDetails = (JsonElement)summary["expiryDetails"];
        //    var accounts = new List<AccountInfo>();

        //    foreach (var e in expiryDetails.EnumerateArray())
        //    {
        //        accounts.Add(new AccountInfo
        //        {
        //            AccountNo = summary["membershipNumber"]?.ToString(),
        //            Miles = e.GetProperty("points").GetDouble(),
        //            Expiry = e.GetProperty("expiryDate").GetString()
        //        });
        //    }

        //    return Ok(new AccountsLinkedResponse
        //    {
        //        NumberOfAccounts = accounts.Count,
        //        Accounts = accounts
        //    });
        //}

        public class AccountsLinkedRequest { public string MembershipNumber { get; set; } }
        public class AccountsLinkedResponse
        {
            public int NumberOfAccounts { get; set; }
            public List<AccountInfo> Accounts { get; set; }
        }
        public class AccountInfo
        {
            public string AccountNo { get; set; }
            public double Miles { get; set; }
            public string Expiry { get; set; }
        }


        // -------------------------------
        // 5. Disruption Flights
        // -------------------------------

        [HttpPost("flights/disruption")]
        public async Task<IActionResult> GetDisruptedFlights([FromBody] FlightsRequest request)
        {
            try
            {
                // Parse the date string into DateTime (expecting yyyy-MM-dd format)
                if (!DateTime.TryParseExact(request.FlightDate, "yyyy-MM-dd",
                    CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime flightDate))
                {
                    return BadRequest(new { ErrorMessage = "Invalid date format. Use yyyy-MM-dd" });
                }

                var flight = await _airportDb.PaxFlightDetails
                    .Where(f => f.FLT_NR == request.FlightNumber
                             && f.SCH_DEP_DT.HasValue
                             && f.SCH_DEP_DT.Value.Date == flightDate.Date)
                    .FirstOrDefaultAsync();

                string disrupted = "N";
                if (flight != null &&
                    ((flight.DISRUPTED_FLAG?.Equals("TRUE", StringComparison.OrdinalIgnoreCase) ?? false) ||
                     (flight.ACTUAL_DEP_DT > flight.SCH_DEP_DT) ||
                     (flight.ACTUAL_ARV_DT > flight.SCH_ARV_DT)))
                {
                    disrupted = "Y";
                }

                return Ok(new
                {
                    FlightNumber = request.FlightNumber,
                    FlightDate = flightDate.ToString("yyyy-MM-dd"),  // ✅ response in same format
                    Disrupted = disrupted
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ErrorMessage = ex.Message });
            }
        }



        // -------------------------------
        // 6. Existing Flight (Y/N + Business flag)
        // -------------------------------
        [HttpPost("flights/existing")]
        public async Task<IActionResult> CheckExistingFlight([FromBody] ExistingFlightRequest request)
        {
            try
            {
                var pax = await _airportDb.PaxFlightDetails
                    .Where(f => f.FFPNUM == request.MembershipNumber)
                    .FirstOrDefaultAsync();

                string existing = pax != null ? "Y" : "N";
                string business = "N";

                if (pax != null && !string.IsNullOrEmpty(pax.PAX_CLASS))
                {
                    if (pax.PAX_CLASS.Equals("J", StringComparison.OrdinalIgnoreCase))
                        business = "Y";
                    else if (pax.PAX_CLASS.Equals("Y", StringComparison.OrdinalIgnoreCase))
                        business = "N";
                }

                return Ok(new
                {
                    ExistingFlight = existing,
                    Business = business
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ErrorMessage = ex.Message });
            }
        }

        public class ExistingFlightRequest
        {
            public string MembershipNumber { get; set; }
        }

        public class FlightsRequest
        {
            public string FlightNumber { get; set; }
            public string FlightDate { get; set; }
            public string MembershipNumber { get; set; }
        }

        // -------------------------------
        // 7. Callback Request (Genesys Cloud)
        // -------------------------------
        [HttpPost("callback")]
        public async Task<IActionResult> RequestCallback([FromBody] CallbackRequest request)
        {
            try
            {
                // 🔑 Paste your Gamification/Genesys token here
                string token = "PASTE_YOUR_TOKEN_HERE";

                var body = new
                {
                    queueId = "2f5c1a43-27a1-4f8e-8ff9-d2a5aa8fda9d", // TODO: replace with Prod queueId
                    callbackUserName = request.Name,
                    callbackNumbers = new[] { request.Phone },
                    callerId = request.Phone,
                    callerIdName = "GulfAir"
                };

                _httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);

                var content = new StringContent(
                    JsonSerializer.Serialize(body),
                    Encoding.UTF8,
                    "application/json");

                var response = await _httpClient.PostAsync(
                    "https://api.mypurecloud.ie/api/v2/conversations/callbacks",
                    content);

                var result = await response.Content.ReadAsStringAsync();
                return StatusCode((int)response.StatusCode, result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ErrorMessage = ex.Message });
            }
        }

        public class CallbackRequest
        {
            public string Name { get; set; }
            public string Phone { get; set; }
        }

    }
}