using CoreWebAPIs.Context;
using CoreWebAPIs.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Abstractions;
using System.Collections.Generic;

namespace CoreWebAPIs.Controllers
{
    [ApiController]
    public class FlightsController : ControllerBase
    {
        private const string BAH_AIRPORT = "BAH";

        private readonly EBriefingDbContext _context;
        public FlightsController(EBriefingDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        //[Authorize(Roles = "Api.FlightStatus.Read")]
        [Route("api/GetTodayFlightsStatus")]
        public async Task<ActionResult> GetTodayFlightsStatus()
        {
            DateTime date = DateTime.Now;

            try
            {
                var getTodayFlights = _context.OtpFlightInfos.Where(a => a.FlightDate.Value.Date == DateTime.Now.Date).ToList();

                if (getTodayFlights.Count > 0)
                {
                    var airportCodes = _context.AirportCodes.ToList();

                    var response = new
                    {
                        date = DateTime.Now.ToString("dd-MMM-yyyy"),
                        flights = getTodayFlights.Select(a => new
                        {
                            Flight_Date = a.FlightDate,
                            Flight_Number = a.FlightNumber,
                            Scheduled_Departure_Time = a.ScheduledDepartureDateTime.HasValue ? a.ScheduledDepartureDateTime.Value.ToString("hh:mm tt") : null,
                            Scheduled_Arrival_Time = a.ScheduledArrivalDateTime.HasValue ? a.ScheduledArrivalDateTime.Value.ToString("hh:mm tt") : null,
                            Departure_Time = a.ScheduledDepartureDateTime.HasValue ? a.ScheduledDepartureDateTime.Value.ToString("hh:mm tt") : null,
                            Arrival_Time = a.ScheduledArrivalDateTime.HasValue ? a.ScheduledArrivalDateTime.Value.ToString("hh:mm tt") : null,
                            Departure_Airport_Code = a.ActualDepartureAirport,
                            Arrival_Airport_Code = a.ActualArrivalAirport,
                            PassengerDetail = new
                            {
                                Total_Passengers = a.OtpPassengerDetail.Total,
                                Business_Class = a.OtpPassengerDetail.Business,
                                Economy_Class = a.OtpPassengerDetail.Coach,
                                Total_Adult = a.OtpPassengerDetail.Adult,
                                Total_Child = a.OtpPassengerDetail.Child,
                                Total_Infant = a.OtpPassengerDetail.Infant,
                                Aircraft_Total_Seat_Config = a.OtpPassengerDetail.SeatConfig,
                                Aircraft_Total_Business_Class_Config = a.OtpPassengerDetail.JSeatConfig,
                                Aircraft_Total_Economy_Class_Config = a.OtpPassengerDetail.YSeatConfig,
                                Total_Booked_Business_Class = a.OtpPassengerDetail.BookedJ,
                                Total_Booked_Economy_Class = a.OtpPassengerDetail.BookedY,
                            },
                            Departure_Airport = airportCodes.Where(ac => ac.Code == a.ActualDepartureAirport).Select(ac => new
                            {
                                ac.Country,
                                ac.City,
                                ac.Code,
                                ac.AirportName
                            }).FirstOrDefault(),
                            Arrival_Airport = airportCodes.Where(ac => ac.Code == a.ActualArrivalAirport).Select(ac => new
                            {
                                ac.Country,
                                ac.City,
                                ac.Code,
                                ac.AirportName
                            }).FirstOrDefault(),
                            a.Status
                        }).ToList()
                    };

                    return Ok(response);
                }

                return NotFound(new { Status = "No Data" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ErrorMessage = ex.Message });
            }
        }

        [HttpGet]
        [Route("api/GetTodayFlights")]
        //[Authorize(Roles = "Api.FlightStatus.Read")]
        public async Task<ActionResult> GetTodayFlights()
        {
            DateTime date = DateTime.Now;
            try
            {
                var TodayFlights = _context.OtpFlightInfos.Where(a => a.FlightDate.Value.Date == DateTime.Now.Date).ToList();
                if (TodayFlights.Count > 0)
                {
                    return Ok(TodayFlights);
                }
                return NotFound(new { Status = "No Data" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ErrorMessage = ex.Message });
            }
        }

        [HttpGet]
        [Route("api/GetFlightStatus/{FlightNumber}/{FlightDate}")]
        //[Authorize(Roles = "Api.FlightStatus.Read")]
        public async Task<ActionResult> GetFlightStatus(string FlightNumber, DateTime FlightDate)
        {
            try
            {
                var getTodayFlights = await _context.OtpFlightInfos
                    .Where(a => a.FlightNumber == FlightNumber && a.FlightDate.Value.Date == FlightDate.Date)
                    .ToListAsync();

                if (getTodayFlights != null && getTodayFlights.Any())
                {
                    var airportCodes = await _context.AirportCodes.ToListAsync();
                    var airportTimeZones = await _context.AiportTimeZones.ToListAsync();

                    var response = new
                    {
                        date = FlightDate.ToString("dd-MMM-yyyy"),
                        flights = getTodayFlights.Select(flight => new
                        {
                            Flight_Date = flight.FlightDate,
                            Flight_Number = flight.FlightNumber,
                            Scheduled_Departure_Date_Time = flight.ScheduledDepartureDateTime,
                            Scheduled_Arrival_Date_Time = flight.ScheduledArrivalDateTime,
                            Actual_Departure_Date_Time = flight.ActualDepartureDateTime,
                            Actual_Arrival_Date_Time = flight.ActualArrivalDateTime,
                            Publish_Departure_Date_Time = flight.PublishDepartureDateTime,
                            Publish_Arrival_Date_Time = flight.PublishArrivalDateTime,
                            Departure_Airport_Code = flight.ActualDepartureAirport,
                            Arrival_Airport_Code = flight.ActualArrivalAirport,
                            Departure_Airport = airportCodes.Where(ac => ac.Code == flight.ActualDepartureAirport)
                                                            .Select(ac => new
                                                            {
                                                                ac.Country,
                                                                ac.City,
                                                                ac.Code,
                                                                ac.AirportName,
                                                                Time_Difference = airportTimeZones
                                                                    .FirstOrDefault(atz => atz.AirportCode == ac.Code)?.TimeDiff ?? 0
                                                            }).FirstOrDefault(),
                            Arrival_Airport = airportCodes.Where(ac => ac.Code == flight.ActualArrivalAirport)
                                                           .Select(ac => new
                                                           {
                                                               ac.Country,
                                                               ac.City,
                                                               ac.Code,
                                                               ac.AirportName,
                                                               Time_Difference = airportTimeZones
                                                                   .FirstOrDefault(atz => atz.AirportCode == ac.Code)?.TimeDiff ?? 0
                                                           }).FirstOrDefault(),
                            flight.Status,
                            flight.CurrentStatus,
                        }).ToList()
                    };

                    return Ok(response);
                }

                return NotFound(new { Status = "No Data" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ErrorMessage = ex.Message });
            }
        }

        [HttpGet]
        [Route("api/GetFlightsStatusByDestination/{Departure}/{Arrival}/{FlightDate}")]
        //[Authorize(Roles = "Api.FlightStatus.Read")]
        public async Task<ActionResult> GetFlightsStatusByDestination(string Departure, string Arrival, DateTime FlightDate)
        {
            try
            {
                var airportCodes = _context.AirportCodes.ToList();

                // Find the corresponding airport codes for the departure and arrival locations
                var departureAirportCode = airportCodes
                    .Where(ac => ac.City == Departure || ac.Country == Departure || ac.Code == Departure)
                    .Select(ac => ac.Code)
                    .FirstOrDefault();

                var arrivalAirportCode = airportCodes
                    .Where(ac => ac.City == Arrival || ac.Country == Arrival || ac.Code == Arrival)
                    .Select(ac => ac.Code)
                    .FirstOrDefault();

                if (string.IsNullOrEmpty(departureAirportCode) || string.IsNullOrEmpty(arrivalAirportCode))
                {
                    return NotFound(new { Status = "No Data", Message = "Departure or Arrival location not found." });
                }

                var airportTimeZones = await _context.AiportTimeZones.ToListAsync();

                // Initialize the list to hold the full route flights
                var fullRouteFlights = new List<OtpFlightInfo>();

                // Start with the flights from the specified departure to arrival location
                var currentFlights = _context.OtpFlightInfos
                    .Where(a => a.ActualDepartureAirport == departureAirportCode && a.FlightDate.Value.Date == FlightDate.Date)
                    .OrderBy(a => a.ScheduledDepartureDateTime)
                    .ToList();

                while (currentFlights.Any())
                {
                    // Add the current flights to the full route
                    fullRouteFlights.AddRange(currentFlights);

                    // Check if the current arrival airport is the final destination
                    var nextArrivalAirport = currentFlights.First().ActualArrivalAirport;
                    if (nextArrivalAirport == arrivalAirportCode)
                    {
                        break;
                    }

                    // Continue tracing through the next leg of the journey
                    currentFlights = _context.OtpFlightInfos
                        .Where(a => a.ActualDepartureAirport == nextArrivalAirport && a.FlightDate.Value.Date == FlightDate.Date)
                        .OrderBy(a => a.ScheduledDepartureDateTime)
                        .ToList();
                }

                // Ensure the flights are ordered by Scheduled_Departure_Date_Time
                fullRouteFlights = fullRouteFlights.OrderBy(f => f.ScheduledDepartureDateTime).ToList();

                if (fullRouteFlights.Count > 0)
                {
                    var response = new
                    {
                        date = FlightDate.ToString("dd-MMM-yyyy"),
                        count = fullRouteFlights.Count,
                        flights = fullRouteFlights.Select(a => new
                        {
                            Flight_Date = a.FlightDate.HasValue ? a.FlightDate.Value.ToString("dd-MMM-yyyy") : null,
                            Flight_Number = a.FlightNumber ?? "N/A",
                            Scheduled_Departure_Date_Time = a.ScheduledDepartureDateTime?.ToString("hh:mm tt") ?? null,
                            Scheduled_Arrival_Date_Time = a.ScheduledArrivalDateTime?.ToString("hh:mm tt") ?? null,
                            Actual_Departure_Date_Time = a.ActualDepartureDateTime?.ToString("hh:mm tt") ?? null,
                            Actual_Arrival_Date_Time = a.ActualArrivalDateTime?.ToString("hh:mm tt") ?? null,
                            Publish_Departure_Date_Time = a.PublishDepartureDateTime?.ToString("hh:mm tt") ?? null,
                            Publish_Arrival_Date_Time = a.PublishArrivalDateTime?.ToString("hh:mm tt") ?? null,
                            Departure_Airport_Code = a.ActualDepartureAirport ?? "N/A",
                            Arrival_Airport_Code = a.ActualArrivalAirport ?? "N/A",
                            Departure_Airport = airportCodes.Where(ac => ac.Code == a.ActualDepartureAirport).Select(ac => new
                            {
                                ac.Country,
                                ac.City,
                                ac.Code,
                                ac.AirportName,
                                Time_Difference = airportTimeZones
                                                  .FirstOrDefault(atz => atz.AirportCode == ac.Code)?.TimeDiff ?? 0
                            }).FirstOrDefault(),
                            Arrival_Airport = airportCodes.Where(ac => ac.Code == a.ActualArrivalAirport).Select(ac => new
                            {
                                ac.Country,
                                ac.City,
                                ac.Code,
                                ac.AirportName,
                                Time_Difference = airportTimeZones
                                                  .FirstOrDefault(atz => atz.AirportCode == ac.Code)?.TimeDiff ?? 0
                            }).FirstOrDefault(),
                            a.Status,
                            a.CurrentStatus
                        }).ToList()
                    };

                    return Ok(response);
                }

                return NotFound(new { Status = "No Data" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ErrorMessage = ex.Message });
            }
        }

        [HttpGet]
        [Route("api/GetFlightsStatusByArrival/{Arrival}/{FlightDate}")]
        //[Authorize(Roles = "Api.FlightStatus.Read")]
        public async Task<ActionResult> GetFlightsStatusByArrival(string Arrival, DateTime FlightDate)
        {
            try
            {
                var airportCodes = _context.AirportCodes.ToList();

                // Find the corresponding airport code for the arrival location
                var arrivalAirportCode = airportCodes
                    .Where(ac => ac.City == Arrival || ac.Country == Arrival || ac.Code == Arrival)
                    .Select(ac => ac.Code)
                    .FirstOrDefault();

                if (string.IsNullOrEmpty(arrivalAirportCode))
                {
                    return NotFound(new { Status = "No Data", Message = "Arrival location not found." });
                }

                var airportTimeZones = await _context.AiportTimeZones.ToListAsync();

                // Initialize the list to hold the full route flights
                var fullRouteFlights = new List<OtpFlightInfo>();

                // Start with the flights arriving at the specified location
                var currentFlights = _context.OtpFlightInfos
                    .Where(a => a.ActualArrivalAirport == arrivalAirportCode && a.FlightDate.Value.Date == FlightDate.Date)
                    .OrderBy(a => a.ScheduledDepartureDateTime)
                    .ToList();

                while (currentFlights.Any())
                {
                    // Add the current flights to the full route
                    fullRouteFlights.AddRange(currentFlights);

                    // Check if the departure airport is BAH; if so, stop tracing back
                    var nextDepartureAirport = currentFlights.First().ActualDepartureAirport;
                    if (nextDepartureAirport == "BAH")
                    {
                        break;
                    }

                    // Otherwise, continue tracing back using the current departure airport as the new arrival airport
                    currentFlights = _context.OtpFlightInfos
                        .Where(a => a.ActualArrivalAirport == nextDepartureAirport &&
                                    a.FlightDate.Value.Date == FlightDate.Date)
                        .OrderBy(a => a.ScheduledDepartureDateTime)
                        .ToList();
                }

                // Ensure the flights are ordered by Scheduled_Departure_Date_Time
                fullRouteFlights = fullRouteFlights.OrderBy(f => f.ScheduledDepartureDateTime).ToList();

                if (fullRouteFlights.Count > 0)
                {
                    var response = new
                    {
                        date = FlightDate.ToString("dd-MMM-yyyy"),
                        count = fullRouteFlights.Count,
                        flights = fullRouteFlights.Select(a => new
                        {
                            Flight_Date = a.FlightDate,
                            Flight_Number = a.FlightNumber,
                            Scheduled_Departure_Date_Time = a.ScheduledDepartureDateTime,
                            Scheduled_Arrival_Date_Time = a.ScheduledArrivalDateTime,
                            Actual_Departure_Date_Time = a.ActualDepartureDateTime,
                            Actual_Arrival_Date_Time = a.ActualArrivalDateTime,
                            Publish_Departure_Date_Time = a.PublishDepartureDateTime,
                            Publish_Arrival_Date_Time = a.PublishArrivalDateTime,
                            Departure_Airport_Code = a.ActualDepartureAirport,
                            Arrival_Airport_Code = a.ActualArrivalAirport,
                            Departure_Airport = airportCodes.Where(ac => ac.Code == a.ActualDepartureAirport).Select(ac => new
                            {
                                ac.Country,
                                ac.City,
                                ac.Code,
                                ac.AirportName,
                                Time_Difference = airportTimeZones
                  .FirstOrDefault(atz => atz.AirportCode == ac.Code)?.TimeDiff ?? 0
                            }).FirstOrDefault(),
                            Arrival_Airport = airportCodes.Where(ac => ac.Code == a.ActualArrivalAirport).Select(ac => new
                            {
                                ac.Country,
                                ac.City,
                                ac.Code,
                                ac.AirportName,
                                Time_Difference = airportTimeZones
                  .FirstOrDefault(atz => atz.AirportCode == ac.Code)?.TimeDiff ?? 0
                            }).FirstOrDefault(),
                            a.Status,
                            a.CurrentStatus
                        }).ToList()
                    };

                    return Ok(response);
                }

                return NotFound(new { Status = "No Data" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ErrorMessage = ex.Message });
            }
        }

        [HttpGet]
        [Route("api/GetFlightsByArrivalBtweenDates/{Arrival}/{FlightDateFrom}/{FlightsDateTo}")]
        //[Authorize(Roles = "Api.FlightStatus.Read")]
        public async Task<ActionResult> GetFlightsByArrivalBtweenDates(string Arrival, DateTime FlightDateFrom, DateTime FlightsDateTo)
        {
            try
            {
                var airportCodes = _context.AirportCodes.ToList();

                // Find the corresponding airport code for the arrival location (e.g., CMN or DXB)
                var arrivalAirportCode = airportCodes
                    .Where(ac => ac.City == Arrival || ac.Country == Arrival || ac.Code == Arrival)
                    .Select(ac => ac.Code)
                    .FirstOrDefault();

                if (string.IsNullOrEmpty(arrivalAirportCode))
                {
                    return NotFound(new { Status = "No Data", Message = "Arrival location not found." });
                }

                var airportTimeZones = await _context.AiportTimeZones.ToListAsync();

                // Initialize the list to hold the full route flights
                var fullRouteFlights = new List<OtpFlightInfo>();

                // Start with the flights arriving at the specified location
                var currentFlights = _context.OtpFlightInfos
                    .Where(a => a.ActualArrivalAirport == arrivalAirportCode && a.FlightDate.Value.Date >= FlightDateFrom.Date &&
                                a.FlightDate.Value.Date <= FlightsDateTo.Date)
                    .OrderBy(a => a.ScheduledDepartureDateTime)
                    .ToList();

                while (currentFlights.Any())
                {
                    // Add the current flights to the full route
                    fullRouteFlights.AddRange(currentFlights);

                    // Check if the departure airport is BAH; if so, stop tracing back
                    var nextDepartureAirport = currentFlights.First().ActualDepartureAirport;
                    if (nextDepartureAirport == "BAH")
                    {
                        break;
                    }

                    // Otherwise, continue tracing back using the current departure airport as the new arrival airport
                    currentFlights = _context.OtpFlightInfos
                        .Where(a => a.ActualArrivalAirport == nextDepartureAirport &&
                                    a.FlightDate.Value.Date >= FlightDateFrom.Date && a.FlightDate.Value.Date <= FlightsDateTo.Date)
                        .OrderBy(a => a.ScheduledDepartureDateTime)
                        .ToList();
                }

                if (fullRouteFlights.Count > 0)
                {
                    var response = new
                    {
                        dateFrom = FlightDateFrom.ToString("dd-MMM-yyyy"),
                        dateTo = FlightsDateTo.ToString("dd-MMM-yyyy"),
                        count = fullRouteFlights.Count,
                        flights = fullRouteFlights.Select(a => new
                        {
                            Flight_Date = a.FlightDate,
                            Flight_Number = a.FlightNumber,
                            Scheduled_Departure_Date_Time = a.ScheduledDepartureDateTime,
                            Scheduled_Arrival_Date_Time = a.ScheduledArrivalDateTime,
                            Actual_Departure_Date_Time = a.ActualDepartureDateTime,
                            Actual_Arrival_Date_Time = a.ActualArrivalDateTime,
                            Publish_Departure_Date_Time = a.PublishDepartureDateTime,
                            Publish_Arrival_Date_Time = a.PublishArrivalDateTime,
                            Departure_Airport_Code = a.ActualDepartureAirport,
                            Arrival_Airport_Code = a.ActualArrivalAirport,
                            Departure_Airport = airportCodes.Where(ac => ac.Code == a.ActualDepartureAirport).Select(ac => new
                            {
                                ac.Country,
                                ac.City,
                                ac.Code,
                                ac.AirportName,
                                Time_Difference = airportTimeZones
                  .FirstOrDefault(atz => atz.AirportCode == ac.Code)?.TimeDiff ?? 0
                            }).FirstOrDefault(),
                            Arrival_Airport = airportCodes.Where(ac => ac.Code == a.ActualArrivalAirport).Select(ac => new
                            {
                                ac.Country,
                                ac.City,
                                ac.Code,
                                ac.AirportName,
                                Time_Difference = airportTimeZones
                  .FirstOrDefault(atz => atz.AirportCode == ac.Code)?.TimeDiff ?? 0
                            }).FirstOrDefault(),
                            a.Status,
                            a.CurrentStatus
                        }).Where(a => a.Departure_Airport_Code != arrivalAirportCode).OrderBy(a => a.Scheduled_Departure_Date_Time).ToList()
                    };

                    return Ok(response);
                }

                return NotFound(new { Status = "No Data" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ErrorMessage = ex.Message });
            }
        }

        [HttpGet]
        [Route("api/GetFlightsStatusBtweenDates/{Departure}/{Arrival}/{FlightDateFrom}/{FlightsDateTo}")]
        //[Authorize(Roles = "Api.FlightStatus.Read")]
        public async Task<ActionResult> GetFlightsStatusBtweenDates(string Departure, string Arrival, DateTime FlightDateFrom, DateTime FlightsDateTo)
        {
            try
            {
                var airportCodes = _context.AirportCodes.ToList();

                var departureAirports = airportCodes
                    .Where(ac => ac.City == Departure || ac.Country == Departure || ac.Code == Departure)
                    .Select(ac => ac.Code)
                    .FirstOrDefault();

                var arrivalAirports = airportCodes
                    .Where(ac => ac.City == Arrival || ac.Country == Arrival || ac.Code == Arrival)
                    .Select(ac => ac.Code)
                    .FirstOrDefault();

                var getTodayFlights = _context.OtpFlightInfos
                    .Where(a => departureAirports.Contains(a.ActualDepartureAirport) && arrivalAirports.Contains(a.ActualArrivalAirport) && 
                    (a.FlightDate.Value.Date >= FlightDateFrom.Date && a.FlightDate.Value.Date <= FlightsDateTo.Date))
                    .OrderBy(a => a.ScheduledDepartureDateTime)
                    .ToList();

                var airportTimeZones = await _context.AiportTimeZones.ToListAsync();

                if (getTodayFlights.Count > 0)
                {
                    var response = new
                    {
                        dateFrom = FlightDateFrom.ToString("dd-MMM-yyyy"),
                        dateTo = FlightsDateTo.ToString("dd-MMM-yyyy"),
                        count = getTodayFlights.Count,
                        flights = getTodayFlights.Select(a => new
                        {
                            Flight_Date = a.FlightDate,
                            Flight_Number = a.FlightNumber,
                            Scheduled_Departure_Date_Time = a.ScheduledDepartureDateTime.HasValue ? a.ScheduledDepartureDateTime.Value : (DateTime?)null,
                            Scheduled_Arrival_Date_Time = a.ScheduledArrivalDateTime.HasValue ? a.ScheduledArrivalDateTime.Value : (DateTime?)null,
                            Actual_Departure_Date_Time = a.ActualDepartureDateTime.HasValue ? a.ActualDepartureDateTime.Value : (DateTime?)null,
                            Actual_Arrival_Date_Time = a.ActualArrivalDateTime.HasValue ? a.ActualArrivalDateTime.Value : (DateTime?)null,
                            Publish_Departure_Date_Time = a.PublishDepartureDateTime.HasValue ? a.PublishDepartureDateTime.Value : (DateTime?)null,
                            Publish_Arrival_Date_Time = a.PublishArrivalDateTime.HasValue ? a.PublishArrivalDateTime.Value : (DateTime?)null,
                            Departure_Airport_Code = a.ActualDepartureAirport,
                            Arrival_Airport_Code = a.ActualArrivalAirport,
                            Departure_Airport = airportCodes.Where(ac => ac.Code == departureAirports).Select(ac => new
                            {
                                ac.Country,
                                ac.City,
                                ac.Code,
                                ac.AirportName,
                                Time_Difference = airportTimeZones
                  .FirstOrDefault(atz => atz.AirportCode == ac.Code)?.TimeDiff ?? 0
                            }).FirstOrDefault(),
                            Arrival_Airport = airportCodes.Where(ac => ac.Code == arrivalAirports).Select(ac => new
                            {
                                ac.Country,
                                ac.City,
                                ac.Code,
                                ac.AirportName,
                                Time_Difference = airportTimeZones
                  .FirstOrDefault(atz => atz.AirportCode == ac.Code)?.TimeDiff ?? 0
                            }).FirstOrDefault(),
                            a.Status,
                            a.CurrentStatus
                        }).ToList()
                    };

                    return Ok(response);
                }

                return NotFound(new { Status = "No Data" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ErrorMessage = ex.Message });
            }
        }

        [HttpGet]
        [Route("api/GetCancelledFlight/{FlightDate}")]
        //[Authorize(Roles = "Api.FlightStatus.Read")]
        public async Task<ActionResult> GetCancelledFlight(DateTime FlightDate)
        {
            DateTime date = DateTime.Now;

            try
            {
                var getFlights = _context.OtpFlightInfos.Where(a => a.FlightDate.Value.Date == FlightDate.Date && a.Status == "Cancelled").OrderBy(a => a.ScheduledDepartureDateTime).ToList();

                if (getFlights.Count > 0)
                {
                    var airportCodes = _context.AirportCodes.ToList();
                    var airportTimeZones = await _context.AiportTimeZones.ToListAsync();

                    var response = new
                    {
                        date = FlightDate.ToString("dd-MMM-yyyy"),
                        count = getFlights.Count,
                        flights = getFlights.Select(a => new
                        {
                            Flight_Date = a.FlightDate,
                            Flight_Number = a.FlightNumber,
                            Scheduled_Departure_Date_Time = a.ScheduledDepartureDateTime.HasValue ? a.ScheduledDepartureDateTime.Value : (DateTime?)null,
                            Scheduled_Arrival_Date_Time = a.ScheduledArrivalDateTime.HasValue ? a.ScheduledArrivalDateTime.Value : (DateTime?)null,
                            Actual_Departure_Date_Time = a.ActualDepartureDateTime.HasValue ? a.ActualDepartureDateTime.Value : (DateTime?)null,
                            Actual_Arrival_Date_Time = a.ActualArrivalDateTime.HasValue ? a.ActualArrivalDateTime.Value : (DateTime?)null,
                            Publish_Departure_Date_Time = a.PublishDepartureDateTime.HasValue ? a.PublishDepartureDateTime.Value : (DateTime?)null,
                            Publish_Arrival_Date_Time = a.PublishArrivalDateTime.HasValue ? a.PublishArrivalDateTime.Value : (DateTime?)null,
                            Departure_Airport_Code = a.ActualDepartureAirport,
                            Arrival_Airport_Code = a.ActualArrivalAirport,
                            Departure_Airport = airportCodes.Where(ac => ac.Code == a.ActualDepartureAirport).Select(ac => new
                            {
                                ac.Country,
                                ac.City,
                                ac.Code,
                                ac.AirportName,
                                Time_Difference = airportTimeZones
                  .FirstOrDefault(atz => atz.AirportCode == ac.Code)?.TimeDiff ?? 0
                            }).FirstOrDefault(),
                            Arrival_Airport = airportCodes.Where(ac => ac.Code == a.ActualArrivalAirport).Select(ac => new
                            {
                                ac.Country,
                                ac.City,
                                ac.Code,
                                ac.AirportName,
                                Time_Difference = airportTimeZones
                  .FirstOrDefault(atz => atz.AirportCode == ac.Code)?.TimeDiff ?? 0
                            }).FirstOrDefault(),
                            a.Status,
                            a.CurrentStatus
                        }).ToList()
                    };

                    return Ok(response);
                }

                return NotFound(new { Status = "No Data" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ErrorMessage = ex.Message });
            }
        }

        [HttpGet]
        [Route("api/GetTotalFlights")]
        //[Authorize(Roles = "Api.FlightStatus.Read")]
        public async Task<ActionResult> GetTotalFlights(DateTime? DateFrom, DateTime? DateTo, string? from, string? to)
        {
            try
            {
                string dep;
                DateTime? dateFrom = DateFrom ?? DateTime.UtcNow;
                DateTime? dateTo = DateTo ?? DateTime.UtcNow;

                var airportCodes = _context.AirportCodes.ToList();

                if (from != null)
                {
                    dep = airportCodes.Where(ac => ac.Code == from || ac.City == from || ac.Country == from)
                                      .Select(ac => ac.Code)
                                      .FirstOrDefault() ?? "BAH";
                }
                else
                {
                    dep = "BAH";
                }

                List<string> arrCodes = new List<string>();
                if (!string.IsNullOrEmpty(to))
                {
                    var toList = to.Split(',').Select(t => t.Trim()).ToList();
                    arrCodes = airportCodes.Where(ac => toList.Any(t => t == ac.Code || t == ac.City || t == ac.Country))
                                           .Select(ac => ac.Code)
                                           .ToList();
                }

                var query = _context.OtpFlightInfos.AsQueryable();

                var groupedFlights = await query.Where(a => a.ActualDepartureAirport == dep &&
                                                            arrCodes.Contains(a.ActualArrivalAirport) &&
                                                            a.FlightDate.Value.Date >= dateFrom.Value.Date &&
                                                            a.FlightDate.Value.Date <= dateTo.Value.Date)
                                                .GroupBy(a => a.ActualArrivalAirport)
                                                .Select(g => new
                                                {
                                                    ArrivalAirport = g.Key,
                                                    FlightsCount = g.Count()
                                                })
                                                .Where(g => g.FlightsCount > 0)
                                                .ToListAsync();

                if (groupedFlights.Any())
                {
                    var response = new
                    {
                        DepartureAirport = dep,
                        ArrivalAirports = groupedFlights.Select(g => g.ArrivalAirport).ToList(),
                        DateFrom = dateFrom.Value.ToString("dd-MMM-yyyy"),
                        DateTo = dateTo.Value.ToString("dd-MMM-yyyy"),
                        FlightsCount = groupedFlights.Sum(g => g.FlightsCount)
                    };

                    return Ok(response);
                }

                return NotFound(new { Status = "No Data" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ErrorMessage = ex.Message });
            }
        }

        [HttpGet]
        [Route("api/GetConnectingFlights/{Departure}/{Destination}/{FlightDate}")]
        //[Authorize(Roles = "Api.FlightStatus.Read")]
        public async Task<ActionResult> GetConnectingFlights(string Departure, string Destination, DateTime FlightDate)
        {
            try
            {
                // Fetch airport codes for Departure and Destination
                var airportCodes = await _context.AirportCodes.ToListAsync();
                var departureAirportCode = airportCodes
                    .FirstOrDefault(ac => ac.City == Departure || ac.Country == Departure || ac.Code == Departure)?.Code;

                var destinationAirportCode = airportCodes
                    .FirstOrDefault(ac => ac.City == Destination || ac.Country == Destination || ac.Code == Destination)?.Code;

                if (string.IsNullOrEmpty(departureAirportCode) || string.IsNullOrEmpty(destinationAirportCode))
                {
                    return NotFound(new { Status = "No Data", Message = "Departure or Destination location not found." });
                }

                // Step 1: Get flights from Departure (BAH) to any intermediate airport
                var firstLegFlights = await _context.OtpFlightInfos
                    .Where(f => f.ActualDepartureAirport == departureAirportCode && f.FlightDate.Value.Date == FlightDate.Date)
                    .ToListAsync();

                if (!firstLegFlights.Any())
                {
                    return NotFound(new { Status = "No Data", Message = "No flights from Departure location found." });
                }

                var connectingFlights = new List<object>();

                // Process each possible first-leg flight
                foreach (var firstLeg in firstLegFlights)
                {
                    var intermediateAirport = firstLeg.ActualArrivalAirport;

                    // Step 2: Get flights from the intermediate airport (e.g., MLE) to the final destination (e.g., CMB)
                    var secondLegFlightsSameDay = await _context.OtpFlightInfos
                        .Where(f => f.ActualDepartureAirport == intermediateAirport
                                 && f.ActualArrivalAirport == destinationAirportCode
                                 && f.FlightDate.Value.Date == FlightDate.Date
                                 && f.ScheduledDepartureDateTime > firstLeg.ScheduledArrivalDateTime)  // Ensure second leg departs after first leg arrives
                        .ToListAsync();

                    // Also search for flights departing the next day (for overnight connections)
                    var secondLegFlightsNextDay = await _context.OtpFlightInfos
                        .Where(f => f.ActualDepartureAirport == intermediateAirport
                                 && f.ActualArrivalAirport == destinationAirportCode
                                 && f.FlightDate.Value.Date == FlightDate.AddDays(1).Date)  // Search the next day for overnight connections
                        .ToListAsync();

                    // Combine same-day and next-day flights
                    var secondLegFlights = secondLegFlightsSameDay.Concat(secondLegFlightsNextDay).ToList();

                    // Log the second-leg flights count
                    Console.WriteLine($"Second Leg Flights Count for {intermediateAirport} to {destinationAirportCode}: {secondLegFlights.Count}");

                    // For each valid second-leg flight, add the full route
                    foreach (var secondLeg in secondLegFlights)
                    {
                        connectingFlights.Add(new
                        {
                            FirstLeg = new
                            {
                                Flight_Number = firstLeg.FlightNumber,
                                Flight_Date = firstLeg.FlightDate.Value.ToString("dd-MMM-yyyy"),
                                Departure_Airport_Code = firstLeg.ActualDepartureAirport,
                                Arrival_Airport_Code = firstLeg.ActualArrivalAirport,
                                Scheduled_Departure_Date_Time = firstLeg.ScheduledDepartureDateTime?.ToString("hh:mm tt"),
                                Scheduled_Arrival_Date_Time = firstLeg.ScheduledArrivalDateTime?.ToString("hh:mm tt"),
                                Actual_Departure_Date_Time = firstLeg.ActualDepartureDateTime?.ToString("hh:mm tt"),
                                Actual_Arrival_Date_Time = firstLeg.ActualArrivalDateTime?.ToString("hh:mm tt"),
                                Status = firstLeg.Status,
                                CurrentStatus = firstLeg.CurrentStatus
                            },
                            SecondLeg = new
                            {
                                Flight_Number = secondLeg.FlightNumber,
                                Flight_Date = secondLeg.FlightDate.Value.ToString("dd-MMM-yyyy"),
                                Departure_Airport_Code = secondLeg.ActualDepartureAirport,
                                Arrival_Airport_Code = secondLeg.ActualArrivalAirport,
                                Scheduled_Departure_Date_Time = secondLeg.ScheduledDepartureDateTime?.ToString("hh:mm tt"),
                                Scheduled_Arrival_Date_Time = secondLeg.ScheduledArrivalDateTime?.ToString("hh:mm tt"),
                                Actual_Departure_Date_Time = secondLeg.ActualDepartureDateTime?.ToString("hh:mm tt"),
                                Actual_Arrival_Date_Time = secondLeg.ActualArrivalDateTime?.ToString("hh:mm tt"),
                                Status = secondLeg.Status,
                                CurrentStatus = secondLeg.CurrentStatus
                            }
                        });
                    }
                }

                if (connectingFlights.Any())
                {
                    return Ok(new
                    {
                        Date = FlightDate.ToString("dd-MMM-yyyy"),
                        Total_Connections = connectingFlights.Count,
                        Flights = connectingFlights
                    });
                }

                return NotFound(new { Status = "No Data", Message = "No connecting flights found for the specified route." });
            }
            catch (Exception ex)
            {
                // Log the error for debugging
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500, new { ErrorMessage = ex.Message });
            }
        }

        [HttpGet]
        [Route("api/GetTransitFlights/{Departure}/{Destination}/{FlightDate}")]
        //[Authorize(Roles = "Api.FlightStatus.Read")]
        public async Task<ActionResult> GetTransitFlights(string Departure, string Destination, DateTime FlightDate)
        {
            try
            {
                var airportCodes = await _context.AirportCodes.ToListAsync();
                var departureAirportCode = airportCodes
                    .FirstOrDefault(ac => ac.City == Departure || ac.Country == Departure || ac.Code == Departure);

                var destinationAirportCode = airportCodes
                    .FirstOrDefault(ac => ac.City == Destination || ac.Country == Destination || ac.Code == Destination);

                if (string.IsNullOrEmpty(departureAirportCode.Code) || string.IsNullOrEmpty(destinationAirportCode.Code))
                {
                    return NotFound(new { Status = "No Data", Message = "Departure or Destination location not found." });
                }

                // Case 1: If BAH is destination or departure, get direct flights only
                if (destinationAirportCode.Code == BAH_AIRPORT || departureAirportCode.Code == BAH_AIRPORT)
                {
                    return await GetDirectFlights(departureAirportCode.Code, destinationAirportCode.Code, FlightDate);
                }

                // Case 2: For all other routes, use BAH as transit point
                var transitRoutes = new List<object>();

                // Get flights to BAH
                var firstLegFlights = await _context.OtpFlightInfos
                    .Where(f => f.ActualDepartureAirport == departureAirportCode.Code
                           && f.ActualArrivalAirport == BAH_AIRPORT
                           && f.FlightDate.Value.Date == FlightDate.Date)
                    .ToListAsync();

                foreach (var firstLeg in firstLegFlights)
                {
                    // Get connecting flights from BAH to destination
                    var secondLegFlights = await _context.OtpFlightInfos
                        .Where(f => f.ActualDepartureAirport == BAH_AIRPORT
                               && f.ActualArrivalAirport == destinationAirportCode.Code
                               && (f.FlightDate.Value.Date == FlightDate.Date || f.FlightDate.Value.Date == FlightDate.AddDays(1).Date)
                               && f.ScheduledDepartureDateTime > firstLeg.ScheduledArrivalDateTime)
                        .ToListAsync();

                    foreach (var secondLeg in secondLegFlights)
                    {
                        var connectionTime = secondLeg.ScheduledDepartureDateTime - firstLeg.ScheduledArrivalDateTime;
                        var minimumConnectionTime = TimeSpan.FromMinutes(60);
                        var maximumConnectionTime = TimeSpan.FromHours(12);

                        if (connectionTime >= minimumConnectionTime && connectionTime <= maximumConnectionTime)
                        {
                            transitRoutes.Add(new
                            {
                                Transit_Point = BAH_AIRPORT,
                                Connection_Time = connectionTime.Value.ToString(@"hh\:mm"),
                                FirstLeg = new
                                {
                                    Flight_Number = firstLeg.FlightNumber,
                                    Flight_Date = firstLeg.FlightDate.Value.ToString("dd-MMM-yyyy"),
                                    Departure_Airport_Code = firstLeg.ActualDepartureAirport,
                                    Arrival_Airport_Code = firstLeg.ActualArrivalAirport,
                                    Scheduled_Departure_Date_Time = firstLeg.ScheduledDepartureDateTime?.ToString("hh:mm tt"),
                                    Scheduled_Arrival_Date_Time = firstLeg.ScheduledArrivalDateTime?.ToString("hh:mm tt"),
                                    Actual_Departure_Date_Time = firstLeg.ActualDepartureDateTime?.ToString("hh:mm tt"),
                                    Actual_Arrival_Date_Time = firstLeg.ActualArrivalDateTime?.ToString("hh:mm tt"),
                                    Status = firstLeg.Status,
                                    CurrentStatus = firstLeg.CurrentStatus,
                                    departureAirportCode.City,
                                    departureAirportCode.Country,
                                    departureAirportCode.AirportName,
                                },
                                SecondLeg = new
                                {
                                    Flight_Number = secondLeg.FlightNumber,
                                    Flight_Date = secondLeg.FlightDate.Value.ToString("dd-MMM-yyyy"),
                                    Departure_Airport_Code = secondLeg.ActualDepartureAirport,
                                    Arrival_Airport_Code = secondLeg.ActualArrivalAirport,
                                    Scheduled_Departure_Date_Time = secondLeg.ScheduledDepartureDateTime?.ToString("hh:mm tt"),
                                    Scheduled_Arrival_Date_Time = secondLeg.ScheduledArrivalDateTime?.ToString("hh:mm tt"),
                                    Actual_Departure_Date_Time = secondLeg.ActualDepartureDateTime?.ToString("hh:mm tt"),
                                    Actual_Arrival_Date_Time = secondLeg.ActualArrivalDateTime?.ToString("hh:mm tt"),
                                    Status = secondLeg.Status,
                                    CurrentStatus = secondLeg.CurrentStatus,
                                    destinationAirportCode.City,
                                    destinationAirportCode.Country,
                                    destinationAirportCode.AirportName
                                },
                                Total_Journey_Time = (secondLeg.ScheduledArrivalDateTime - firstLeg.ScheduledDepartureDateTime)?.ToString(@"hh\:mm")
                            });
                        }
                    }
                }

                if (transitRoutes.Any())
                {
                    var sortedRoutes = transitRoutes.OrderBy(r => TimeSpan.Parse(((dynamic)r).Total_Journey_Time));

                    return Ok(new
                    {
                        Date = FlightDate.ToString("dd-MMM-yyyy"),
                        From = departureAirportCode,
                        To = destinationAirportCode,
                        Route_Type = "Transit via BAH",
                        Total_Routes = transitRoutes.Count,
                        Routes = sortedRoutes
                    });
                }

                return NotFound(new
                {
                    Status = "No Data",
                    Message = $"No flights found from {departureAirportCode} to {destinationAirportCode} via BAH on {FlightDate:dd-MMM-yyyy}."
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetTransitFlights: {ex.Message}");
                return StatusCode(500, new { ErrorMessage = "An error occurred while processing your request." });
            }
        }

        [HttpGet]
        [Route("api/GetFlightStatusByFlightNumber/{FlightNumber}/{FlightDate}")]
        //[Authorize(Roles = "Api.FlightStatus.Read")]
        public async Task<ActionResult> GetFlightStatusByFlightNumber(string FlightNumber, DateTime FlightDate)
        {
            try
            {
                var getTodayFlights = await _context.OtpFlightInfos
                    .Where(a => a.FlightNumber == FlightNumber &&
                           a.FlightDate.Value.Date == FlightDate.Date &&
                           (a.ActualArrivalAirport == BAH_AIRPORT || a.ActualDepartureAirport == BAH_AIRPORT))
                    .ToListAsync();

                if (getTodayFlights != null && getTodayFlights.Any())
                {
                    var airportCodes = await _context.AirportCodes.ToListAsync();
                    var airportTimeZones = await _context.AiportTimeZones.ToListAsync();

                    // Get the first flight's departure and final destination for From/To
                    var firstFlight = getTodayFlights.First();
                    var departureAirportCode = airportCodes.FirstOrDefault(ac => ac.Code == firstFlight.ActualDepartureAirport);
                    var destinationAirportCode = airportCodes.FirstOrDefault(ac => ac.Code == firstFlight.ActualArrivalAirport);

                    var response = new
                    {
                        Date = FlightDate.ToString("dd-MMM-yyyy"),
                        From = new
                        {
                            Id = departureAirportCode.Id,
                            departureAirportCode.Country,
                            departureAirportCode.City,
                            departureAirportCode.Code,
                            departureAirportCode.AirportName
                        },
                        To = new
                        {
                            Id = destinationAirportCode.Id,
                            destinationAirportCode.Country,
                            destinationAirportCode.City,
                            destinationAirportCode.Code,
                            destinationAirportCode.AirportName
                        },
                        Route_Type = "Transit via BAH",
                        Total_Routes = getTodayFlights.Count,
                        Routes = getTodayFlights.Select(flight => new
                        {
                            Transit_Point = BAH_AIRPORT,
                            Connection_Time = "00:00", // You might want to calculate this based on your business logic
                            FirstLeg = new
                            {
                                Flight_Number = flight.FlightNumber,
                                Flight_Date = flight.FlightDate?.ToString("dd-MMM-yyyy"),
                                Scheduled_Departure_Date_Time = flight.ScheduledDepartureDateTime?.ToString("hh:mm tt"),
                                Scheduled_Arrival_Date_Time = flight.ScheduledArrivalDateTime?.ToString("hh:mm tt"),
                                Actual_Departure_Date_Time = flight.ActualDepartureDateTime?.ToString("hh:mm tt"),
                                Actual_Arrival_Date_Time = flight.ActualArrivalDateTime?.ToString("hh:mm tt"),
                                Departure_Airport_Code = flight.ActualDepartureAirport,
                                Arrival_Airport_Code = flight.ActualArrivalAirport,
                                Status = flight.Status,
                                CurrentStatus = flight.CurrentStatus,
                                City = departureAirportCode.City,
                                Country = departureAirportCode.Country,
                                AirportName = departureAirportCode.AirportName
                            },
                            Total_Journey_Time = (flight.ScheduledArrivalDateTime - flight.ScheduledDepartureDateTime)?.ToString(@"hh\:mm")
                        }).OrderBy(r => TimeSpan.Parse(r.Total_Journey_Time))
                    };

                    return Ok(response);
                }

                return NotFound(new { Status = "No Data" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ErrorMessage = ex.Message });
            }
        }

        [HttpGet]
        [Route("api/GetFlightStatusByDestination/{Departure}/{Arrival}/{FlightDate}")]
        //[Authorize(Roles = "Api.FlightStatus.Read")]
        public async Task<ActionResult> GetFlightStatusByDestination(string Departure, string Arrival, DateTime FlightDate)
        {
            try
            {
                // Get all airport codes for lookup
                var airportCodes = await _context.AirportCodes.ToListAsync();

                // Find departure and arrival airport codes
                var departureAirports = airportCodes
                    .Where(ac => ac.City == Departure || ac.Country == Departure || ac.Code == Departure)
                    .Select(ac => ac.Code)
                    .FirstOrDefault();

                var arrivalAirports = airportCodes
                    .Where(ac => ac.City == Arrival || ac.Country == Arrival || ac.Code == Arrival)
                    .Select(ac => ac.Code)
                    .FirstOrDefault();

                // Get flights for the specified route and date
                var getTodayFlights = await _context.OtpFlightInfos
                    .Where(a => departureAirports.Contains(a.ActualDepartureAirport) &&
                               arrivalAirports.Contains(a.ActualArrivalAirport) &&
                               a.FlightDate.Value.Date == FlightDate.Date)
                    .OrderBy(a => a.ScheduledDepartureDateTime)
                    .ToListAsync();

                if (getTodayFlights != null && getTodayFlights.Any())
                {
                    var airportTimeZones = await _context.AiportTimeZones.ToListAsync();

                    // Get departure and destination airport details
                    var departureAirportCode = airportCodes.FirstOrDefault(ac => ac.Code == departureAirports);
                    var destinationAirportCode = airportCodes.FirstOrDefault(ac => ac.Code == arrivalAirports);

                    var response = new
                    {
                        Date = FlightDate.ToString("dd-MMM-yyyy"),
                        From = new
                        {
                            Id = departureAirportCode.Id,
                            departureAirportCode.Country,
                            departureAirportCode.City,
                            departureAirportCode.Code,
                            departureAirportCode.AirportName,
                            Time_Difference = airportTimeZones
                                .FirstOrDefault(atz => atz.AirportCode == departureAirportCode.Code)?.TimeDiff ?? 0
                        },
                        To = new
                        {
                            Id = destinationAirportCode.Id,
                            destinationAirportCode.Country,
                            destinationAirportCode.City,
                            destinationAirportCode.Code,
                            destinationAirportCode.AirportName,
                            Time_Difference = airportTimeZones
                                .FirstOrDefault(atz => atz.AirportCode == destinationAirportCode.Code)?.TimeDiff ?? 0
                        },
                        Total_Flights = getTodayFlights.Count,
                        Flights = getTodayFlights.Select(flight => new
                        {
                            Flight_Number = flight.FlightNumber,
                            Flight_Date = flight.FlightDate?.ToString("dd-MMM-yyyy"),
                            Scheduled_Departure_Date_Time = flight.ScheduledDepartureDateTime?.ToString("hh:mm tt"),
                            Scheduled_Arrival_Date_Time = flight.ScheduledArrivalDateTime?.ToString("hh:mm tt"),
                            Actual_Departure_Date_Time = flight.ActualDepartureDateTime?.ToString("hh:mm tt"),
                            Actual_Arrival_Date_Time = flight.ActualArrivalDateTime?.ToString("hh:mm tt"),
                            Publish_Departure_Date_Time = flight.PublishDepartureDateTime?.ToString("hh:mm tt"),
                            Publish_Arrival_Date_Time = flight.PublishArrivalDateTime?.ToString("hh:mm tt"),
                            Departure_Airport_Code = flight.ActualDepartureAirport,
                            Arrival_Airport_Code = flight.ActualArrivalAirport,
                            Status = flight.Status,
                            CurrentStatus = flight.CurrentStatus,
                            Total_Journey_Time = (flight.ScheduledArrivalDateTime - flight.ScheduledDepartureDateTime)?.ToString(@"hh\:mm")
                        }).OrderBy(f => f.Scheduled_Departure_Date_Time).ToList()
                    };

                    return Ok(response);
                }

                return NotFound(new { Status = "No Data" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ErrorMessage = ex.Message });
            }
        }

        [HttpGet]
        [Route("api/GetNextFlights")]
        //[Authorize(Roles = "Api.FlightStatus.Read")]
        public async Task<ActionResult> GetNextFlights(string? from = null, string? to = null)
        {
            try
            {
                string dep = "";
                string arr = "";
                DateTime date = DateTime.UtcNow;

                var airportCodes = _context.AirportCodes.ToList();

                if (from != null)
                {
                    dep = airportCodes.Where(ac => ac.Code == from || ac.City == from || ac.Country == from).Select(ac => ac.Code).FirstOrDefault();
                }
                else
                {
                    dep = "BAH";
                }

                if (to != null)
                {
                    arr = airportCodes.Where(ac => ac.Code == to || ac.City == to || ac.Country == to).Select(ac => ac.Code).FirstOrDefault();
                }

                var query = _context.OtpFlightInfos.AsQueryable();

                query = query.Where(a => a.FlightDate == date.Date && a.ScheduledDepartureDateTime.Value >= date && a.ActualDepartureAirport == dep && a.ActualArrivalAirport == arr);

                var getFlights = await query.OrderBy(a => a.ScheduledDepartureDateTime).ToListAsync();

                if (getFlights.Count > 0)
                {
                    var response = new
                    {
                        Date = date.ToString("dd-MMM-yyyy"),
                        flights = getFlights.Select(a => new
                        {
                            Flight_Date = a.FlightDate,
                            Flight_Number = a.FlightNumber,
                            Scheduled_Departure_Date_Time = a.ScheduledDepartureDateTime.HasValue ? a.ScheduledDepartureDateTime.Value : (DateTime?)null,
                            Scheduled_Arrival_Date_Time = a.ScheduledArrivalDateTime.HasValue ? a.ScheduledArrivalDateTime.Value : (DateTime?)null,
                            Actual_Departure_Date_Time = a.ActualDepartureDateTime.HasValue ? a.ActualDepartureDateTime.Value : (DateTime?)null,
                            Actual_Arrival_Date_Time = a.ActualArrivalDateTime.HasValue ? a.ActualArrivalDateTime.Value : (DateTime?)null,
                            Publish_Departure_Date_Time = a.PublishDepartureDateTime.HasValue ? a.PublishDepartureDateTime.Value : (DateTime?)null,
                            Publish_Arrival_Date_Time = a.PublishArrivalDateTime.HasValue ? a.PublishArrivalDateTime.Value : (DateTime?)null,
                            Departure_Airport_Code = a.ActualDepartureAirport,
                            Arrival_Airport_Code = a.ActualArrivalAirport,
                            Departure_Airport = airportCodes.Where(ac => ac.Code == a.ActualDepartureAirport).Select(ac => new
                            {
                                ac.Country,
                                ac.City,
                                ac.Code,
                                ac.AirportName
                            }).FirstOrDefault(),
                            Arrival_Airport = airportCodes.Where(ac => ac.Code == a.ActualArrivalAirport).Select(ac => new
                            {
                                ac.Country,
                                ac.City,
                                ac.Code,
                                ac.AirportName
                            }).FirstOrDefault(),
                            a.Status,
                            a.CurrentStatus
                        }).ToList()
                    };

                    return Ok(response);
                }

                return NotFound(new { Status = "No Data" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ErrorMessage = ex.Message });
            }
        }

        [HttpGet]
        [Route("api/GetDelayedFlight/{FlightDate}")]
        //[Authorize(Roles = "Api.FlightStatus.Read")]
        public async Task<ActionResult> GetDelayedFlight(DateTime FlightDate)
        {
            DateTime date = DateTime.Now;

            try
            {
                var getFlights = _context.OtpFlightInfos.Where(a => a.FlightDate.Value.Date == FlightDate.Date && a.Status == "Delayed").OrderBy(a => a.ScheduledDepartureDateTime).ToList();

                if (getFlights.Count > 0)
                {
                    var airportCodes = _context.AirportCodes.ToList();
                    var airportTimeZones = await _context.AiportTimeZones.ToListAsync();

                    var response = new
                    {
                        date = DateTime.Now.ToString("dd-MMM-yyyy"),
                        count = getFlights.Count,
                        flights = getFlights.Select(a => new
                        {
                            Flight_Date = a.FlightDate,
                            Flight_Number = a.FlightNumber,
                            Scheduled_Departure_Date_Time = a.ScheduledDepartureDateTime.HasValue ? a.ScheduledDepartureDateTime.Value : (DateTime?)null,
                            Scheduled_Arrival_Date_Time = a.ScheduledArrivalDateTime.HasValue ? a.ScheduledArrivalDateTime.Value : (DateTime?)null,
                            Actual_Departure_Date_Time = a.ActualDepartureDateTime.HasValue ? a.ActualDepartureDateTime.Value : (DateTime?)null,
                            Actual_Arrival_Date_Time = a.ActualArrivalDateTime.HasValue ? a.ActualArrivalDateTime.Value : (DateTime?)null,
                            Publish_Departure_Date_Time = a.PublishDepartureDateTime.HasValue ? a.PublishDepartureDateTime.Value : (DateTime?)null,
                            Publish_Arrival_Date_Time = a.PublishArrivalDateTime.HasValue ? a.PublishArrivalDateTime.Value : (DateTime?)null,
                            Departure_Airport_Code = a.ActualDepartureAirport,
                            Arrival_Airport_Code = a.ActualArrivalAirport,
                            Departure_Airport = airportCodes.Where(ac => ac.Code == a.ActualDepartureAirport).Select(ac => new
                            {
                                ac.Country,
                                ac.City,
                                ac.Code,
                                ac.AirportName,
                                Time_Difference = airportTimeZones
                  .FirstOrDefault(atz => atz.AirportCode == ac.Code)?.TimeDiff ?? 0
                            }).FirstOrDefault(),
                            Arrival_Airport = airportCodes.Where(ac => ac.Code == a.ActualArrivalAirport).Select(ac => new
                            {
                                ac.Country,
                                ac.City,
                                ac.Code,
                                ac.AirportName,
                                Time_Difference = airportTimeZones
                  .FirstOrDefault(atz => atz.AirportCode == ac.Code)?.TimeDiff ?? 0
                            }).FirstOrDefault(),
                            a.Status,
                            a.CurrentStatus
                        }).ToList()
                    };

                    return Ok(response);
                }

                return NotFound(new { Status = "No Data" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ErrorMessage = ex.Message });
            }
        }

        [HttpGet]
        [Route("api/GetDepartedFlight/{FlightDate}")]
        //[Authorize(Roles = "Api.FlightStatus.Read")]
        public async Task<ActionResult> GetDepartedFlight(DateTime FlightDate)
        {
            DateTime date = DateTime.Now;

            try
            {
                var getFlights = _context.OtpFlightInfos.Where(a => a.FlightDate.Value.Date == FlightDate.Date && a.CurrentStatus == "Departed").OrderBy(a => a.ScheduledDepartureDateTime).ToList();

                if (getFlights.Count > 0)
                {
                    var airportCodes = _context.AirportCodes.ToList();
                    var airportTimeZones = await _context.AiportTimeZones.ToListAsync();

                    var response = new
                    {
                        date = DateTime.Now.ToString("dd-MMM-yyyy"),
                        count = getFlights.Count,
                        flights = getFlights.Select(a => new
                        {
                            Flight_Date = a.FlightDate,
                            Flight_Number = a.FlightNumber,
                            Scheduled_Departure_Date_Time = a.ScheduledDepartureDateTime.HasValue ? a.ScheduledDepartureDateTime.Value : (DateTime?)null,
                            Scheduled_Arrival_Date_Time = a.ScheduledArrivalDateTime.HasValue ? a.ScheduledArrivalDateTime.Value : (DateTime?)null,
                            Actual_Departure_Date_Time = a.ActualDepartureDateTime.HasValue ? a.ActualDepartureDateTime.Value : (DateTime?)null,
                            Actual_Arrival_Date_Time = a.ActualArrivalDateTime.HasValue ? a.ActualArrivalDateTime.Value : (DateTime?)null,
                            Publish_Departure_Date_Time = a.PublishDepartureDateTime.HasValue ? a.PublishDepartureDateTime.Value : (DateTime?)null,
                            Publish_Arrival_Date_Time = a.PublishArrivalDateTime.HasValue ? a.PublishArrivalDateTime.Value : (DateTime?)null,
                            Departure_Airport_Code = a.ActualDepartureAirport,
                            Arrival_Airport_Code = a.ActualArrivalAirport,
                            Departure_Airport = airportCodes.Where(ac => ac.Code == a.ActualDepartureAirport).Select(ac => new
                            {
                                ac.Country,
                                ac.City,
                                ac.Code,
                                ac.AirportName,
                                Time_Difference = airportTimeZones
                  .FirstOrDefault(atz => atz.AirportCode == ac.Code)?.TimeDiff ?? 0
                            }).FirstOrDefault(),
                            Arrival_Airport = airportCodes.Where(ac => ac.Code == a.ActualArrivalAirport).Select(ac => new
                            {
                                ac.Country,
                                ac.City,
                                ac.Code,
                                ac.AirportName,
                                Time_Difference = airportTimeZones
                  .FirstOrDefault(atz => atz.AirportCode == ac.Code)?.TimeDiff ?? 0
                            }).FirstOrDefault(),
                            a.Status,
                            a.CurrentStatus
                        }).ToList()
                    };

                    return Ok(response);
                }

                return NotFound(new { Status = "No Data" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ErrorMessage = ex.Message });
            }
        }

        [HttpGet]
        [Route("api/GetArrivedFlight/{FlightDate}")]
        //[Authorize(Roles = "Api.FlightStatus.Read")]
        public async Task<ActionResult> GetArrivedFlight(DateTime FlightDate)
        {
            DateTime date = DateTime.Now;

            try
            {
                var getFlights = _context.OtpFlightInfos.Where(a => a.FlightDate.Value.Date == FlightDate.Date && a.CurrentStatus == "Arrived").OrderBy(a => a.ScheduledDepartureDateTime).ToList();

                if (getFlights.Count > 0)
                {
                    var airportCodes = _context.AirportCodes.ToList();
                    var airportTimeZones = await _context.AiportTimeZones.ToListAsync();

                    var response = new
                    {
                        date = DateTime.Now.ToString("dd-MMM-yyyy"),
                        count = getFlights.Count,
                        flights = getFlights.Select(a => new
                        {
                            Flight_Date = a.FlightDate,
                            Flight_Number = a.FlightNumber,
                            Scheduled_Departure_Date_Time = a.ScheduledDepartureDateTime.HasValue ? a.ScheduledDepartureDateTime.Value : (DateTime?)null,
                            Scheduled_Arrival_Date_Time = a.ScheduledArrivalDateTime.HasValue ? a.ScheduledArrivalDateTime.Value : (DateTime?)null,
                            Actual_Departure_Date_Time = a.ActualDepartureDateTime.HasValue ? a.ActualDepartureDateTime.Value : (DateTime?)null,
                            Actual_Arrival_Date_Time = a.ActualArrivalDateTime.HasValue ? a.ActualArrivalDateTime.Value : (DateTime?)null,
                            Publish_Departure_Date_Time = a.PublishDepartureDateTime.HasValue ? a.PublishDepartureDateTime.Value : (DateTime?)null,
                            Publish_Arrival_Date_Time = a.PublishArrivalDateTime.HasValue ? a.PublishArrivalDateTime.Value : (DateTime?)null,
                            Departure_Airport_Code = a.ActualDepartureAirport,
                            Arrival_Airport_Code = a.ActualArrivalAirport,
                            Departure_Airport = airportCodes.Where(ac => ac.Code == a.ActualDepartureAirport).Select(ac => new
                            {
                                ac.Country,
                                ac.City,
                                ac.Code,
                                ac.AirportName,
                                Time_Difference = airportTimeZones
                  .FirstOrDefault(atz => atz.AirportCode == ac.Code)?.TimeDiff ?? 0
                            }).FirstOrDefault(),
                            Arrival_Airport = airportCodes.Where(ac => ac.Code == a.ActualArrivalAirport).Select(ac => new
                            {
                                ac.Country,
                                ac.City,
                                ac.Code,
                                ac.AirportName,
                                Time_Difference = airportTimeZones
                  .FirstOrDefault(atz => atz.AirportCode == ac.Code)?.TimeDiff ?? 0
                            }).FirstOrDefault(),
                            a.Status,
                            a.CurrentStatus
                        }).ToList()
                    };

                    return Ok(response);
                }

                return NotFound(new { Status = "No Data" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ErrorMessage = ex.Message });
            }
        }

        [HttpGet]
        [Route("api/GetPassengers/{FlightNumber}/{FlightDate}")]
        //[Authorize(Roles = "Api.FlightStatus.Read")]
        public async Task<ActionResult> GetPassengers(string FlightNumber, DateTime FlightDate)
        {
            try
            {
                var getTodayFlights = _context.OtpFlightInfos.Include(a => a.OtpPassengerDetail).Where(a => a.FlightDate.Value.Date == FlightDate && a.FlightNumber == FlightNumber).ToList();

                if (getTodayFlights.Count > 0)
                {
                    var airportCodes = _context.AirportCodes.ToList();
                    var airportTimeZones = await _context.AiportTimeZones.ToListAsync();

                    var response = new
                    {
                        date = DateTime.Now.ToString("dd-MMM-yyyy"),
                        flight = getTodayFlights.Select(a => new
                        {
                            Flight_Date = a.FlightDate,
                            Flight_Number = a.FlightNumber,
                            Scheduled_Departure_Date_Time = a.ScheduledDepartureDateTime,
                            Scheduled_Arrival_Date_Time = a.ScheduledArrivalDateTime,
                            Actual_Departure_Date_Time = a.ActualDepartureDateTime,
                            Actual_Arrival_Date_Time = a.ActualArrivalDateTime,
                            Publish_Departure_Date_Time = a.PublishDepartureDateTime,
                            Publish_Arrival_Date_Time = a.PublishArrivalDateTime,
                            Departure_Airport_Code = a.ActualDepartureAirport,
                            Arrival_Airport_Code = a.ActualArrivalAirport,
                            Departure_Airport = airportCodes.Where(ac => ac.Code == a.ActualDepartureAirport)
                                                            .Select(ac => new
                                                            {
                                                                ac.Country,
                                                                ac.City,
                                                                ac.Code,
                                                                ac.AirportName,
                                                            }).FirstOrDefault(),
                            Arrival_Airport = airportCodes.Where(ac => ac.Code == a.ActualArrivalAirport)
                                                           .Select(ac => new
                                                           {
                                                               ac.Country,
                                                               ac.City,
                                                               ac.Code,
                                                               ac.AirportName,
                                                           }).FirstOrDefault(),
                            PassengerDetail = new
                            {
                                Total_Passengers = a.OtpPassengerDetail?.Total ?? 0,
                                Business_Class = a.OtpPassengerDetail?.Business ?? 0,
                                Economy_Class = a.OtpPassengerDetail?.Coach ?? 0,
                                Total_Adult = a.OtpPassengerDetail?.Adult ?? 0,
                                Total_Child = a.OtpPassengerDetail?.Child ?? 0,
                                Total_Infant = a.OtpPassengerDetail?.Infant ?? 0,
                                Aircraft_Total_Seat_Config = a.OtpPassengerDetail?.SeatConfig ?? "0",
                                Aircraft_Total_Business_Class_Config = a.OtpPassengerDetail?.JSeatConfig ?? "0",
                                Aircraft_Total_Economy_Class_Config = a.OtpPassengerDetail?.YSeatConfig ?? "0",
                                Total_Booked_Business_Class = a.OtpPassengerDetail?.BookedJ ?? 0,
                                Total_Booked_Economy_Class = a.OtpPassengerDetail?.BookedY ?? 0,
                            }
                        }).FirstOrDefault()
                    };

                    return Ok(response);
                }

                return NotFound(new { Status = "No Data" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ErrorMessage = ex.Message });
            }
        }

        [HttpGet]
        [Route("api/GetAirportLocalTime/{AirportCode}")]
        //[Authorize(Roles = "Api.FlightStatus.Read")]
        public async Task<ActionResult> GetAirportLocalTime(string AirportCode)
        {
            DateTime date = DateTime.Now;

            try
            {
                var getLocalTime = _context.AiportTimeZones.Where(a => a.AirportCode == AirportCode).FirstOrDefault();

                if (getLocalTime != null)
                {
                    var response = new
                    {
                        Airport_Code = getLocalTime.AirportCode,
                        Time_Difference = getLocalTime.TimeDiff
                    };

                    return Ok(response);
                }

                return NotFound(new { Status = "No Data" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ErrorMessage = ex.Message });
            }
        }

        private async Task<ActionResult> GetDirectFlights(string departure, string destination, DateTime flightDate)
        {
            var directFlights = await _context.OtpFlightInfos
                .Where(f => f.ActualDepartureAirport == departure
                       && f.ActualArrivalAirport == destination
                       && f.FlightDate.Value.Date == flightDate.Date)
                .ToListAsync();

            if (directFlights.Any())
            {
                var routes = directFlights.Select(flight => new
                {
                    Flight_Number = flight.FlightNumber,
                    Flight_Date = flight.FlightDate.Value.ToString("dd-MMM-yyyy"),
                    Departure_Airport_Code = flight.ActualDepartureAirport,
                    Arrival_Airport_Code = flight.ActualArrivalAirport,
                    Scheduled_Departure_Date_Time = flight.ScheduledDepartureDateTime?.ToString("hh:mm tt"),
                    Scheduled_Arrival_Date_Time = flight.ScheduledArrivalDateTime?.ToString("hh:mm tt"),
                    Actual_Departure_Date_Time = flight.ActualDepartureDateTime?.ToString("hh:mm tt"),
                    Actual_Arrival_Date_Time = flight.ActualArrivalDateTime?.ToString("hh:mm tt"),
                    Status = flight.Status,
                    CurrentStatus = flight.CurrentStatus
                });

                return Ok(new
                {
                    Date = flightDate.ToString("dd-MMM-yyyy"),
                    From = departure,
                    To = destination,
                    Route_Type = "Direct",
                    Total_Routes = directFlights.Count,
                    Routes = routes
                });
            }

            return NotFound(new
            {
                Status = "No Data",
                Message = $"No direct flights found from {departure} to {destination} on {flightDate:dd-MMM-yyyy}."
            });
        }
    }
}
