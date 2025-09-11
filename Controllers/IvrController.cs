using CoreWebAPIs.Context;
using CoreWebAPIs.Models;
using CoreWebAPIs.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Text.Json;

namespace CoreWebAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IvrController : ControllerBase
    {
        private readonly PaxFlightsDbContext _airportDb;
        private readonly LoyaltyApiService _loyalty;

        public IvrController(PaxFlightsDbContext airportDb, LoyaltyApiService loyalty)
        {
            _airportDb = airportDb;
            _loyalty = loyalty;
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
        // Stage 3 – Flight Details
        // -------------------------------
        [HttpPost("flights/details")]
        public async Task<IActionResult> GetFlightsDetails([FromBody] FlightsRequest request)
        {
            var today = DateTime.Today;
            var now = DateTime.Now;
            var cutoff = now.AddHours(48);

            var allFlights = await _airportDb.PaxFlightDetails
                .Where(f => f.FFPNUM.Trim() == request.MembershipNumber.Trim())
                .ToListAsync();

            if (!allFlights.Any())
            {
                return Ok(new
                {
                    success = false,
                    message = $"No flights found for membership {request.MembershipNumber}",
                    totalFlights = 0,
                    hasDisruption = "N",
                    flights = new List<FlightInfo>()
                });
            }

            var combined = allFlights
                .Where(f => f.SCH_DEP_DT.HasValue &&
                           (f.SCH_DEP_DT.Value.Date == today ||
                           (f.SCH_DEP_DT.Value >= now && f.SCH_DEP_DT.Value <= cutoff)))
                .Select(f => MapFlight(f))
                .ToList();

            var hasDisruption = allFlights.Any(f =>
                f.DISRUPTED_FLAG == "TRUE" ||
                !string.IsNullOrEmpty(f.CNCL_CD) ||
                (f.ACTUAL_DEP_DT != null && f.SCH_DEP_DT != null && f.ACTUAL_DEP_DT > f.SCH_DEP_DT) ||
                (f.ACTUAL_ARV_DT != null && f.SCH_ARV_DT != null && f.ACTUAL_ARV_DT > f.SCH_ARV_DT)
            ) ? "Y" : "N";

            return Ok(new
            {
                success = true,
                message = $"Found {combined.Count} flights for {request.MembershipNumber}",
                totalFlights = combined.Count,
                hasDisruption = hasDisruption,
                flights = combined
            });
        }


        // ✅ Mapper
        private static FlightInfo MapFlight(PaxFlightDetail f)
        {
            return new FlightInfo
            {
                FlightNumber = f.FLT_NR ?? "",
                FlightDate = f.SCH_DEP_DT?.ToString("yyyy-MM-dd") ?? "",
                Status = f.STATUS ?? "",
                CurrentStatus = f.DISRUPTED_FLAG == "Y" ? "Disrupted" :
                                f.CNCL_CD != null ? "Cancelled" : "On Time",
                LegSequenceNumber = f.LEG_SEQ_NR != null ? (int)f.LEG_SEQ_NR : 0,

                DepartureAirport = new AirportInfo
                {
                    Code = f.ACTUAL_DEP_ARP_CD ?? "",
                    AirportName = f.DEP_STATION_NAME ?? "",
                    Country = "",
                    City = "",
                    TimeDifference = 0
                },

                // 👇 Updated handling for times
                ScheduledDeparture = (f.SCH_DEP_DT == null) ? "--"
                                   : (f.SCH_DEP_DT.Value.TimeOfDay == TimeSpan.Zero
                                       ? f.SCH_DEP_DT.Value.ToString("yyyy-MM-dd")
                                       : f.SCH_DEP_DT.Value.ToString("yyyy-MM-dd HH:mm")),

                ActualDeparture = (f.ACTUAL_DEP_DT == null) ? "--"
                                 : (f.ACTUAL_DEP_DT.Value.TimeOfDay == TimeSpan.Zero
                                     ? f.ACTUAL_DEP_DT.Value.ToString("yyyy-MM-dd")
                                     : f.ACTUAL_DEP_DT.Value.ToString("yyyy-MM-dd HH:mm")),

                ArrivalAirport = new AirportInfo
                {
                    Code = f.ACTUAL_ARV_ARP_CD ?? "",
                    AirportName = f.ARV_STATION_NAME ?? "",
                    Country = "",
                    City = "",
                    TimeDifference = 0
                },

                ScheduledArrival = (f.SCH_ARV_DT == null) ? "--"
                                   : (f.SCH_ARV_DT.Value.TimeOfDay == TimeSpan.Zero
                                       ? f.SCH_ARV_DT.Value.ToString("yyyy-MM-dd")
                                       : f.SCH_ARV_DT.Value.ToString("yyyy-MM-dd HH:mm")),

                ActualArrival = (f.ACTUAL_ARV_DT == null) ? "--"
                                 : (f.ACTUAL_ARV_DT.Value.TimeOfDay == TimeSpan.Zero
                                     ? f.ACTUAL_ARV_DT.Value.ToString("yyyy-MM-dd")
                                     : f.ACTUAL_ARV_DT.Value.ToString("yyyy-MM-dd HH:mm"))
            };
        }








        public class FlightsRequest { public string MembershipNumber { get; set; } }
        public class FlightDetailsResponse
        {
            public int NumberOfFlights { get; set; }
            public string HasFlightWithin48h { get; set; }
            public string HasDisruption { get; set; }
            public List<FlightInfo> Flights { get; set; } = new();
        }

        // -------------------------------
        // Stage 5 – Accounts Linked
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
        // Stage 6 – Flight Disruptions
        // -------------------------------
        [HttpPost("flights/disruption")]
        public async Task<ActionResult<List<DisruptionResponse>>> GetDisruptedFlights([FromBody] FlightsRequest request)
        {
            var disruptions = await _airportDb.PaxFlightDetails
                .Where(f => f.FFPNUM == request.MembershipNumber &&
                            f.PUB_DEP_DT >= DateTime.UtcNow.AddDays(-1) &&
                            f.PUB_DEP_DT <= DateTime.UtcNow.AddHours(48))
                .Select(f => new DisruptionResponse
                {
                    FlightNumber = f.FLT_NR,

                    Status =
                        (f.DISRUPTED_FLAG == "TRUE") ? "Disrupted"
                        : (!string.IsNullOrEmpty(f.CNCL_CD)) ? "Cancelled"
                        : ((f.ACTUAL_DEP_DT != null && f.SCH_DEP_DT != null && f.ACTUAL_DEP_DT > f.SCH_DEP_DT)
                            || (f.ACTUAL_ARV_DT != null && f.SCH_ARV_DT != null && f.ACTUAL_ARV_DT > f.SCH_ARV_DT))
                            ? "Delayed"
                            : "On Time",

                    Message =
                        (f.DISRUPTED_FLAG == "TRUE") ? "Your flight is disrupted"
                        : (!string.IsNullOrEmpty(f.CNCL_CD)) ? "Your flight has been cancelled"
                        : ((f.ACTUAL_DEP_DT != null && f.SCH_DEP_DT != null && f.ACTUAL_DEP_DT > f.SCH_DEP_DT)
                            || (f.ACTUAL_ARV_DT != null && f.SCH_ARV_DT != null && f.ACTUAL_ARV_DT > f.SCH_ARV_DT))
                            ? "Your flight was delayed"
                            : "Your flight is operating normally",

                    NewItinerary =
                        (f.DISRUPTED_FLAG == "TRUE" || !string.IsNullOrEmpty(f.CNCL_CD))
                            ? "Please contact support to rebook"
                            : "No action needed"
                })
                .ToListAsync();

            return Ok(disruptions);
        }



        // 👇 Already defined inside your controller, keep it
        public class DisruptionResponse
        {
            public string FlightNumber { get; set; }
            public string Status { get; set; }
            public string Message { get; set; }
            public string NewItinerary { get; set; }
        }

    }
}

