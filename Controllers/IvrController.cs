using CoreWebAPIs.Context;
using CoreWebAPIs.Models;
using CoreWebAPIs.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Http;
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

        // -------------------------------
        // 3. Flight Details (External API)
        // -------------------------------
        [HttpPost("flights/details")]
        public async Task<IActionResult> GetFlightDetails([FromBody] FlightDetailsRequest request)
        {
            try
            {
                var apiUrl = $"https://gfflightstatus.azurewebsites.net/api/flightStatus/{request.FlightNumber}/{request.FlightDate:dd-MMM-yyyy}";
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
                bool within48Hours = Math.Abs((request.FlightDate - DateTime.Now).TotalHours) <= 48;

                bool isDisrupted = false;
                string status = "Unknown";
                string currentStatus = "Unknown";

                if (doc.RootElement.TryGetProperty("flights", out var flights) && flights.ValueKind == JsonValueKind.Array)
                {
                    var firstFlight = flights[0];
                    status = firstFlight.TryGetProperty("status", out var st) ? st.GetString() ?? "Unknown" : "Unknown";
                    currentStatus = firstFlight.TryGetProperty("currentStatus", out var cs) ? cs.GetString() ?? "Unknown" : "Unknown";

                    if (!string.Equals(status, "On Time", StringComparison.OrdinalIgnoreCase))
                        isDisrupted = true;
                }

                return Ok(new
                {
                    FlightNumber = request.FlightNumber,
                    FlightDate = request.FlightDate.ToString("dd-MMM-yyyy"),
                    FlightsAvailable = flightsAvailable,
                    Within48Hours = within48Hours ? "Y" : "N",
                    IsDisrupted = isDisrupted ? "Y" : "N",
                    Status = status,
                    CurrentStatus = currentStatus
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ErrorMessage = ex.Message });
            }
        }

        public class FlightDetailsRequest
        {
            public string FlightNumber { get; set; }
            public DateTime FlightDate { get; set; }
            public string MembershipNumber { get; set; }
        }

        // -------------------------------
        // 5. Accounts Linked
        // -------------------------------
        [HttpPost("accounts/linked")]
        public async Task<ActionResult<AccountsLinkedResponse>> GetAccountsLinked([FromBody] AccountsLinkedRequest request)
        {
            var summary = await _loyalty.GetAccountSummaryAsync(request.MembershipNumber);

            if (summary is null || !summary.ContainsKey("expiryDetails"))
                return Ok(new AccountsLinkedResponse
                {
                    NumberOfAccounts = 0,
                    Accounts = new List<AccountInfo>()
                });

            var expiryDetails = (JsonElement)summary["expiryDetails"];
            var accounts = new List<AccountInfo>();

            foreach (var e in expiryDetails.EnumerateArray())
            {
                accounts.Add(new AccountInfo
                {
                    AccountNo = summary["membershipNumber"]?.ToString(),
                    Miles = e.GetProperty("points").GetDouble(),
                    Expiry = e.GetProperty("expiryDate").GetString()
                });
            }

            return Ok(new AccountsLinkedResponse
            {
                NumberOfAccounts = accounts.Count,
                Accounts = accounts
            });
        }

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
        // 6. Flight Disruptions (same logic as Details, simplified to requirements)
        // -------------------------------
        [HttpPost("flights/disruption")]
        public async Task<IActionResult> GetDisruptedFlights([FromBody] FlightsRequest request)
        {
            try
            {
                var apiUrl = $"https://gfflightstatus.azurewebsites.net/api/flightStatus/{request.FlightNumber}/{request.FlightDate:dd-MMM-yyyy}";
                var apiResponse = await _httpClient.GetAsync(apiUrl);

                if (!apiResponse.IsSuccessStatusCode)
                {
                    return Ok(new
                    {
                        FlightNumber = request.FlightNumber,
                        FlightDate = request.FlightDate.ToString("dd-MMM-yyyy"),
                        Status = "Unknown"
                    });
                }

                var json = await apiResponse.Content.ReadAsStringAsync();
                using var doc = JsonDocument.Parse(json);

                string disruptionStatus = "On Time"; // default

                if (doc.RootElement.TryGetProperty("flights", out var flights) && flights.ValueKind == JsonValueKind.Array && flights.GetArrayLength() > 0)
                {
                    var firstFlight = flights[0];

                    string status = firstFlight.TryGetProperty("status", out var st) ? st.GetString() ?? "Unknown" : "Unknown";
                    string currentStatus = firstFlight.TryGetProperty("currentStatus", out var cs) ? cs.GetString() ?? "Unknown" : "Unknown";

                    // normalize to requirements
                    if (status.Contains("Cancel", StringComparison.OrdinalIgnoreCase) ||
                        currentStatus.Contains("Cancel", StringComparison.OrdinalIgnoreCase))
                    {
                        disruptionStatus = "Cancelled";
                    }
                    else if (status.Contains("Delay", StringComparison.OrdinalIgnoreCase) ||
                             currentStatus.Contains("Delay", StringComparison.OrdinalIgnoreCase))
                    {
                        disruptionStatus = "Delayed";
                    }
                    else
                    {
                        disruptionStatus = "On Time"; // Success
                    }
                }

                return Ok(new
                {
                    FlightNumber = request.FlightNumber,
                    FlightDate = request.FlightDate.ToString("dd-MMM-yyyy"),
                    Status = disruptionStatus
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ErrorMessage = ex.Message });
            }
        }



        // -------------------------------
        // 7. Existing Flight (Y/N + Business flag using PAX_CLASS)
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
            public DateTime FlightDate { get; set; }
            public string MembershipNumber { get; set; }
        }
    }
}
