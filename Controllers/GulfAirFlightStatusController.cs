using CoreWebAPIs.Context;
using CoreWebAPIs.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace CoreWebAPIs.Controllers
{
    [ApiController]
    public class GulfAirFlightStatusController : ControllerBase
    {
        private readonly EBriefingDbContext _context;

        public GulfAirFlightStatusController(EBriefingDbContext context)
        {
            _context = context;
        }

        [HttpGet("api/flightStatus/{flightNumber}/{flightDate}")]
        public async Task<ActionResult> GetFlightStatus(string flightNumber, string flightDate)
        {
            try
            {
                flightNumber = flightNumber.PadLeft(4, '0');

                if (!DateTime.TryParseExact(
                        flightDate, "dd-MMM-yyyy",
                        CultureInfo.InvariantCulture, DateTimeStyles.None,
                        out var date))
                {
                    return BadRequest(new FlightResult
                    {
                        Success = false,
                        Message = "Invalid date format. Use dd-MMM-yyyy (e.g., 30-Jun-2025)"
                    });
                }

                var candidateLegs = await _context.OtpFlightInfos
                                       .Where(f => f.FlightNumber == flightNumber &&
                                                   f.FlightDate.HasValue &&
                                                   (f.FlightDate.Value.Date == date.Date ||
                                                    f.FlightDate.Value.Date == date.Date.AddDays(1)))
                                       .OrderBy(f => f.FlightDate)
                                       .ThenBy(f => f.LegSequenceNumber)
                                       .ToListAsync();

                if (!candidateLegs.Any())
                {
                    return Ok(new FlightsResult
                    {
                        Success = true,
                        Message = "Flight not found",
                        TotalFlights = 0,
                        Flights = new List<FlightInfo>()
                    });
                }

                var selectedLegs = new List<OtpFlightInfo>();

                var leg1 = candidateLegs.FirstOrDefault(l =>
                             l.FlightDate!.Value.Date == date.Date &&
                             (l.LegSequenceNumber ?? 1) == 1);

                if (leg1 != null)
                {
                    selectedLegs.Add(leg1);

                    var leg2 = candidateLegs.FirstOrDefault(l =>
                                 (l.LegSequenceNumber ?? 1) > 1 &&
                                 l.ActualDepartureAirport == leg1.ActualArrivalAirport &&
                                 l.FlightDate!.Value.Date == date.Date.AddDays(1));

                    if (leg2 == null)
                    {
                        leg2 = candidateLegs.FirstOrDefault(l =>
                                 (l.LegSequenceNumber ?? 1) > 1 &&
                                 l.ActualDepartureAirport == leg1.ActualArrivalAirport &&
                                 l.FlightDate!.Value.Date == date.Date);
                    }

                    if (leg2 != null) selectedLegs.Add(leg2);
                }
                else
                {
                    selectedLegs = candidateLegs
                                   .Where(l => l.FlightDate!.Value.Date == date.Date)
                                   .ToList();
                }

                selectedLegs = selectedLegs
                               .OrderBy(l => l.FlightDate)
                               .ThenBy(l => l.LegSequenceNumber)
                               .ToList();

                var airports = selectedLegs
                    .SelectMany(l => new[] { l.ActualDepartureAirport, l.ActualArrivalAirport })
                    .Where(c => !string.IsNullOrEmpty(c))
                    .Distinct()
                    .ToList();

                var airportDict = await _context.AirportCodes
                                      .Where(a => airports.Contains(a.Code))
                                      .ToDictionaryAsync(a => a.Code);

                var tzDict = await _context.AiportTimeZones
                                  .Where(t => airports.Contains(t.AirportCode))
                                  .ToDictionaryAsync(t => t.AirportCode);

                var flights = new List<FlightInfo>();

                foreach (var leg in selectedLegs)
                {
                    var info = await MapToFlightInfo(leg);
                    info.Status = CalculateFlightStatus(leg);
                    info.LegSequenceNumber = leg.LegSequenceNumber ?? 1;

                    if (!string.IsNullOrEmpty(leg.ActualDepartureAirport))
                    {
                        airportDict.TryGetValue(leg.ActualDepartureAirport, out var dep);
                        tzDict.TryGetValue(leg.ActualDepartureAirport, out var depTz);

                        info.DepartureAirport = new AirportInfo
                        {
                            Code = dep?.Code ?? leg.ActualDepartureAirport,
                            AirportName = dep?.AirportName,
                            City = dep?.City,
                            Country = dep?.Country,
                            TimeDifference = depTz?.TimeDiff ?? 0
                        };
                    }

                    if (!string.IsNullOrEmpty(leg.ActualArrivalAirport))
                    {
                        airportDict.TryGetValue(leg.ActualArrivalAirport, out var arr);
                        tzDict.TryGetValue(leg.ActualArrivalAirport, out var arrTz);

                        info.ArrivalAirport = new AirportInfo
                        {
                            Code = arr?.Code ?? leg.ActualArrivalAirport,
                            AirportName = arr?.AirportName,
                            City = arr?.City,
                            Country = arr?.Country,
                            TimeDifference = arrTz?.TimeDiff ?? 0
                        };
                    }

                    flights.Add(info);
                }

                return Ok(new FlightsResult
                {
                    Success = flights.Count > 0,
                    Flights = flights,
                    TotalFlights = flights.Count,
                    Message = flights.Count == 0 ? "Flight not found" : null
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new FlightResult { Success = false, Message = ex.Message });
            }
        }


        private string CalculateFlightStatus(OtpFlightInfo flight)
        {
            const int delayThresholdMinutes = 15;

            // Check if flight has arrived
            if (flight.ActualArrivalDateTime.HasValue)
            {
                if (flight.ScheduledArrivalDateTime.HasValue)
                {
                    var arrivalDelay = flight.ActualArrivalDateTime.Value - flight.ScheduledArrivalDateTime.Value;

                    // If arrival is more than 15 minutes late
                    if (arrivalDelay.TotalMinutes > delayThresholdMinutes)
                    {
                        return "Delayed Arrival";
                    }
                }

                // Flight has arrived and is not delayed
                return "Arrived";
            }

            // Check if flight has departed but not arrived
            if (flight.ActualDepartureDateTime.HasValue)
            {
                return "In Flight";
            }

            // Flight has not departed yet - check for departure delay
            if (flight.ScheduledDepartureDateTime.HasValue)
            {
                var currentTime = DateTime.Now;

                // If we have actual departure time, use it; otherwise use current time for comparison
                var compareTime = flight.ActualDepartureDateTime ?? currentTime;

                // Check if departure is delayed
                if (flight.ActualDepartureDateTime.HasValue)
                {
                    var departureDelay = flight.ActualDepartureDateTime.Value - flight.ScheduledDepartureDateTime.Value;

                    if (departureDelay.TotalMinutes > delayThresholdMinutes)
                    {
                        return "Delayed Departure";
                    }
                    else
                    {
                        return "On Time";
                    }
                }
                else
                {
                    // Flight hasn't departed yet, check if scheduled time has passed
                    if (currentTime > flight.ScheduledDepartureDateTime.Value.AddMinutes(delayThresholdMinutes))
                    {
                        return "Delayed Departure";
                    }
                    else
                    {
                        return "Scheduled";
                    }
                }
            }

            // Default status if no sufficient data
            return "Unknown";
        }

        [HttpGet("api/FlightsSearch/{departure}/{arrival}/{fromDate?}/{toDate?}")]
        public async Task<ActionResult> GetFlightsBetweenDates(
            string departure,
            string arrival,
            string? fromDate = null,
            string? toDate = null)
        {
            try
            {
                // ─────────────── parse dates ───────────────
                DateTime from;
                if (string.IsNullOrEmpty(fromDate))
                    from = DateTime.UtcNow.Date;
                else if (!DateTime.TryParseExact(fromDate, "dd-MMM-yyyy",
                                                 CultureInfo.InvariantCulture,
                                                 DateTimeStyles.None, out from))
                    return BadRequest(new FlightResult
                    { Success = false, Message = "Invalid fromDate format. Use dd-MMM-yyyy (e.g., 30-Jun-2025)" });
                from = from.Date;

                DateTime to;
                if (string.IsNullOrEmpty(toDate))
                    to = from;
                else if (!DateTime.TryParseExact(toDate, "dd-MMM-yyyy",
                                                 CultureInfo.InvariantCulture,
                                                 DateTimeStyles.None, out to))
                    return BadRequest(new FlightResult
                    { Success = false, Message = "Invalid toDate format. Use dd-MMM-yyyy (e.g., 30-Jun-2025)" });
                to = to.Date;

                // ─────────────── leg-1: always departs from requested origin ──────────
                var leg1s = await _context.OtpFlightInfos
                             .AsNoTracking()
                             .Where(f => f.ActualDepartureAirport == departure &&
                                         f.FlightDate.HasValue &&
                                         f.FlightDate.Value.Date >= from &&
                                         f.FlightDate.Value.Date <= to &&
                                         (f.LegSequenceNumber ?? 1) == 1)
                             .ToListAsync();

                if (!leg1s.Any())
                    return Ok(new FlightsResult
                    { Success = true, Message = "No flights found", TotalFlights = 0, Flights = new List<FlightInfo>() });

                var flightNums = leg1s.Select(l => l.FlightNumber).Distinct().ToList();

                // ─────────────── potential onward legs (leg-2, leg-3, …) ──────────────
                var extraLegs = await _context.OtpFlightInfos
                                 .AsNoTracking()
                                 .Where(f => flightNums.Contains(f.FlightNumber) &&
                                             (f.LegSequenceNumber ?? 1) > 1 &&
                                             f.FlightDate.HasValue &&
                                             f.FlightDate.Value.Date >= from &&
                                             f.FlightDate.Value.Date <= to.AddDays(1)) // +1 to cover next-day legs
                                 .ToListAsync();

                var selectedLegs = new List<OtpFlightInfo>();

                foreach (var leg1 in leg1s.OrderBy(l => l.FlightDate))
                {
                    // direct flight
                    if (leg1.ActualArrivalAirport == arrival)
                    {
                        selectedLegs.Add(leg1);
                        continue;
                    }

                    // multi-leg flight: prefer next-day continuation, else same-day
                    var leg2 = extraLegs.FirstOrDefault(l =>
                                    l.FlightNumber == leg1.FlightNumber &&
                                    l.ActualDepartureAirport == leg1.ActualArrivalAirport &&
                                    l.ActualArrivalAirport == arrival &&
                                    l.FlightDate!.Value.Date == leg1.FlightDate!.Value.Date.AddDays(1));

                    if (leg2 == null)
                    {
                        leg2 = extraLegs.FirstOrDefault(l =>
                                    l.FlightNumber == leg1.FlightNumber &&
                                    l.ActualDepartureAirport == leg1.ActualArrivalAirport &&
                                    l.ActualArrivalAirport == arrival &&
                                    l.FlightDate!.Value.Date == leg1.FlightDate!.Value.Date);
                    }

                    if (leg2 != null)
                    {
                        selectedLegs.Add(leg1);
                        selectedLegs.Add(leg2);
                    }
                }

                if (!selectedLegs.Any())
                    return Ok(new FlightsResult
                    { Success = true, Message = "No flights found", TotalFlights = 0, Flights = new List<FlightInfo>() });

                // ─────────────── hydrate airports / time-zones ─────────────────────────
                var airportCodes = selectedLegs
                    .SelectMany(l => new[] { l.ActualDepartureAirport, l.ActualArrivalAirport })
                    .Where(c => !string.IsNullOrEmpty(c))
                    .Distinct()
                    .ToList();

                var airportDict = await _context.AirportCodes
                                     .AsNoTracking()
                                     .Where(a => airportCodes.Contains(a.Code))
                                     .ToDictionaryAsync(a => a.Code);

                var tzDict = await _context.AiportTimeZones
                                 .AsNoTracking()
                                 .Where(t => airportCodes.Contains(t.AirportCode))
                                 .ToDictionaryAsync(t => t.AirportCode);

                // ─────────────── map to DTOs ───────────────────────────────────────────
                var flights = new List<FlightInfo>();

                foreach (var leg in selectedLegs
                                    .OrderBy(l => l.FlightDate)
                                    .ThenBy(l => l.LegSequenceNumber))
                {
                    var info = await MapToFlightInfo(leg);
                    info.Status = CalculateFlightStatus(leg);
                    info.LegSequenceNumber = leg.LegSequenceNumber ?? 1;

                    if (!string.IsNullOrEmpty(leg.ActualDepartureAirport))
                    {
                        airportDict.TryGetValue(leg.ActualDepartureAirport, out var dep);
                        tzDict.TryGetValue(leg.ActualDepartureAirport, out var depTz);

                        info.DepartureAirport = new AirportInfo
                        {
                            Code = dep?.Code ?? leg.ActualDepartureAirport,
                            AirportName = dep?.AirportName ?? "Unknown Airport",
                            City = dep?.City ?? "Unknown",
                            Country = dep?.Country ?? "Unknown",
                            TimeDifference = depTz?.TimeDiff ?? 0
                        };
                    }

                    if (!string.IsNullOrEmpty(leg.ActualArrivalAirport))
                    {
                        airportDict.TryGetValue(leg.ActualArrivalAirport, out var arr);
                        tzDict.TryGetValue(leg.ActualArrivalAirport, out var arrTz);

                        info.ArrivalAirport = new AirportInfo
                        {
                            Code = arr?.Code ?? leg.ActualArrivalAirport,
                            AirportName = arr?.AirportName ?? "Unknown Airport",
                            City = arr?.City ?? "Unknown",
                            Country = arr?.Country ?? "Unknown",
                            TimeDifference = arrTz?.TimeDiff ?? 0
                        };
                    }

                    flights.Add(info);
                }

                return Ok(new FlightsResult
                {
                    Success = true,
                    TotalFlights = flights.Count,
                    Flights = flights
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new FlightResult { Success = false, Message = ex.Message });
            }
        }



        [HttpGet("api/airport/{airportCode}")]
        public async Task<ActionResult> GetAirportInfo(string airportCode)
        {
            if (string.IsNullOrEmpty(airportCode))
            {
                return BadRequest(new AirportInfo
                {
                    Code = "N/A",
                    Country = "N/A",
                    City = "N/A",
                    AirportName = "N/A",
                    TimeDifference = 0
                });
            }

            try
            {
                // 1) Load airport master data
                var airportDetails = await _context.AirportCodes
                    .AsNoTracking()
                    .FirstOrDefaultAsync(ac => ac.Code == airportCode);

                // 2) Then load timezone info
                var timezone = await _context.AiportTimeZones
                    .AsNoTracking()
                    .FirstOrDefaultAsync(tz => tz.AirportCode == airportCode);

                var result = new AirportInfo
                {
                    Code = airportCode,
                    Country = airportDetails?.Country ?? "Unknown",
                    City = airportDetails?.City ?? "Unknown",
                    AirportName = airportDetails?.AirportName ?? "Unknown Airport",
                    TimeDifference = timezone?.TimeDiff ?? 0
                };

                if (airportDetails == null)
                {
                    return NotFound(result);
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                // In case anything goes wrong, return sane defaults
                return StatusCode(500, new AirportInfo
                {
                    Code = airportCode,
                    Country = "Unknown",
                    City = "Unknown",
                    AirportName = "Unknown Airport",
                    TimeDifference = 0
                });
            }
        }

        [HttpGet("api/GulfairDestinations")]
        public async Task<ActionResult<List<AirportInfo>>> GulfairDestinations()
        {
            try
            {
                // Load all airport master data
                var airportDetails = await _context.AirportCodes
                    .AsNoTracking()
                    .ToListAsync();

                // Load all timezone info
                var timezones = await _context.AiportTimeZones
                    .AsNoTracking()
                    .ToListAsync();

                if (airportDetails == null || !airportDetails.Any())
                {
                    return NotFound("No airport destinations found");
                }

                // Join airport details with timezone info
                var result = airportDetails.Select(airport => new AirportInfo
                {
                    Code = airport.Code ?? "Unknown",
                    Country = airport.Country ?? "Unknown",
                    City = airport.City ?? "Unknown",
                    AirportName = airport.AirportName ?? "Unknown Airport",
                    TimeDifference = timezones
                        .FirstOrDefault(tz => tz.AirportCode == airport.Code)?.TimeDiff ?? 0
                }).ToList();

                return Ok(result);
            }
            catch (Exception ex)
            {
                // Log the exception if you have logging configured
                // _logger.LogError(ex, "Error retrieving Gulfair destinations");

                return StatusCode(500, "An error occurred while retrieving airport destinations");
            }
        }

        #region Private Helper Methods

        private async Task<FlightInfo> MapToFlightInfo(OtpFlightInfo flight)
        {
            var status = DetermineFlightStatus(flight);
            var flyingHours = await CalculateFlyingHoursAsync(flight);

            // Load airport details for both departure and arrival
            var departureAirport = await GetAirportInfoInternal(flight.ActualDepartureAirport);
            var arrivalAirport = await GetAirportInfoInternal(flight.ActualArrivalAirport);

            return new FlightInfo
            {
                FlightNumber = flight.FlightNumber ?? "N/A",
                FlightDate = flight.FlightDate?.ToString("dd-MMM-yyyy") ?? "N/A",

                // Flight status information
                Status = status,
                CurrentStatus = flight.CurrentStatus ?? status,
                FlyingHours = flyingHours,

                // Departure information
                DepartureAirport = departureAirport,
                ScheduledDeparture = flight.ScheduledDepartureDateTime?.ToString("HH:mm") ?? "N/A",
                ActualDeparture = flight.ActualDepartureDateTime?.ToString("HH:mm"),

                // Arrival information
                ArrivalAirport = arrivalAirport,
                ScheduledArrival = flight.ScheduledArrivalDateTime?.ToString("HH:mm") ?? "N/A",
                ActualArrival = flight.ActualArrivalDateTime?.ToString("HH:mm")
            };
        }

        private string DetermineFlightStatus(OtpFlightInfo flight)
        {
            // If we have actual arrival time, flight has arrived
            if (flight.ActualArrivalDateTime.HasValue)
            {
                return "Arrived";
            }

            // If we have actual departure time but no arrival, flight is in progress
            if (flight.ActualDepartureDateTime.HasValue)
            {
                return "In-Flight";
            }

            // Check for delays (more than 15 minutes)
            if (flight.PublishDepartureDateTime.HasValue && flight.ScheduledDepartureDateTime.HasValue)
            {
                var delay = flight.PublishDepartureDateTime.Value - flight.ScheduledDepartureDateTime.Value;
                if (delay.TotalMinutes > 15)
                {
                    return "Delayed";
                }
            }

            return "Scheduled";
        }

        private async Task<string> CalculateFlyingHoursAsync(OtpFlightInfo flight)
        {
            try
            {
                // Get departure and arrival times (prefer actual over scheduled)
                DateTime? departureTime = flight.ActualDepartureDateTime ?? flight.ScheduledDepartureDateTime;
                DateTime? arrivalTime = flight.ActualArrivalDateTime ?? flight.ScheduledArrivalDateTime;

                if (!departureTime.HasValue || !arrivalTime.HasValue)
                {
                    return "N/A";
                }

                // Get airport timezone differences
                var departureAirport = await GetAirportInfoInternal(flight.ActualDepartureAirport);
                var arrivalAirport = await GetAirportInfoInternal(flight.ActualArrivalAirport);

                // Calculate flying time using timezone difference
                var localDuration = arrivalTime.Value - departureTime.Value;
                var timezoneAdjustment = arrivalAirport.TimeDifference - departureAirport.TimeDifference;
                var actualDuration = localDuration.Subtract(TimeSpan.FromMinutes(timezoneAdjustment));

                // Handle negative durations (next day arrivals)
                if (actualDuration.TotalMinutes < 0)
                {
                    actualDuration = actualDuration.Add(TimeSpan.FromDays(1));
                }

                return $"{(int)actualDuration.TotalHours}:{actualDuration.Minutes:D2}";
            }
            catch
            {
                return "N/A";
            }
        }

        // Internal version of GetAirportInfo to avoid conflicts with the public API endpoint
        private async Task<AirportInfo> GetAirportInfoInternal(string? airportCode)
        {
            if (string.IsNullOrEmpty(airportCode))
            {
                return new AirportInfo
                {
                    Code = "N/A",
                    Country = "N/A",
                    City = "N/A",
                    AirportName = "N/A",
                    TimeDifference = 0
                };
            }

            try
            {
                // 1) Load airport master data
                var airportDetails = await _context.AirportCodes
                    .AsNoTracking()
                    .FirstOrDefaultAsync(ac => ac.Code == airportCode);

                // 2) Then load timezone info
                var timezone = await _context.AiportTimeZones
                    .AsNoTracking()
                    .FirstOrDefaultAsync(tz => tz.AirportCode == airportCode);

                return new AirportInfo
                {
                    Code = airportCode,
                    Country = airportDetails?.Country ?? "Unknown",
                    City = airportDetails?.City ?? "Unknown",
                    AirportName = airportDetails?.AirportName ?? "Unknown Airport",
                    TimeDifference = timezone?.TimeDiff ?? 0
                };
            }
            catch
            {
                // In case anything goes wrong, return sane defaults
                return new AirportInfo
                {
                    Code = airportCode,
                    Country = "Unknown",
                    City = "Unknown",
                    AirportName = "Unknown Airport",
                    TimeDifference = 0
                };
            }
        }

        #endregion
    }

}
