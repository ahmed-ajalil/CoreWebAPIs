using CoreWebAPIs.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Abstractions;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Net;
using Microsoft.Extensions.Azure;
using Microsoft.OData.Edm;
using CoreWebAPIs.Context;

namespace CoreWebAPIs.Controllers
{
    [ApiController]
    public class FlightsStatus : ControllerBase
    {
        private readonly EBriefingDbContext _context;
        public FlightsStatus(EBriefingDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Authorize(Roles = "Api.FlightStatus.Read")]
        [Route("api/GetCurrentDayScheduledFlights")]
        public async Task<ActionResult> GetCurrentDayScheduledFlights()
        {
            try
            {
                var flights = _context.OtpFlightStatuses.Where(a => a.SchDepDt.Date == DateTime.Now.Date).Select(a => new
                {
                    refNum = a.FltSeqNr,
                    flightDate = a.FltDt,
                    flightNumber = a.FltNr,
                    legNumber = a.LegSeqNr,
                    schDepartureDate = a.SchDepDt,
                    pubSchDepartureDate = a.PubSchDepDt,
                    schArrivalDate = a.SchArvDt,
                    pubSchArrivalDate = a.PubSchArvDt,
                    offBlocks = a.ActualOffblocks,
                    onblocks = a.ActualOnblocks,
                    airBone = a.ActualAirborne,
                    landing = a.ActualLanding,
                    latestDepDate = a.LatestDepDt,
                    latestArvDate = a.LatestArvDt,
                    origin = a.SchDepArpCd,
                    destination = a.SchArvArpCd,
                    acType = a.LatestEqpCd,
                    regustration = a.LatestTailNr,
                    status = a.LegStatus,
                    estDepDate = a.EstDepDt,
                    estArvDate = a.EstArvDt,
                    lastUpdated = a.LastUpdated,
                    gReturn = a.GrdReturn,
                    gat = a.DepGates
                }).ToList();

                if (flights.Count > 0)
                {
                    return Ok(flights);
                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ErrorMessage = ex.Message });
            }
        }

        [HttpGet]
        [Authorize(Roles = "Api.FlightStatus.Read")]
        [Route("api/GetFlightsChanges/{hour}")]
        public async Task<ActionResult> GetFlightsChanges(int hour)
        {
            try
            {
                if (hour >= 1 && hour <= 12)
                {
                    var flights = _context.OtpFlightStatuses.Where(a => a.LastUpdated > DateTime.UtcNow.AddHours(-hour)).Select(a => new
                    {
                        refNum = a.FltSeqNr,
                        flightDate = a.FltDt,
                        flightNumber = a.FltNr,
                        legNumber = a.LegSeqNr,
                        schDepartureDate = a.SchDepDt,
                        pubSchDepartureDate = a.PubSchDepDt,
                        schArrivalDate = a.SchArvDt,
                        pubSchArrivalDate = a.PubSchArvDt,
                        offBlocks = a.ActualOffblocks,
                        onblocks = a.ActualOnblocks,
                        airBone = a.ActualAirborne,
                        landing = a.ActualLanding,
                        latestDepDate = a.LatestDepDt,
                        latestArvDate = a.LatestArvDt,
                        origin = a.SchDepArpCd,
                        destination = a.SchArvArpCd,
                        acType = a.LatestEqpCd,
                        regustration = a.LatestTailNr,
                        status = a.LegStatus,
                        estDepDate = a.EstDepDt,
                        estArvDate = a.EstArvDt,
                        lastUpdated = a.LastUpdated,
                        gReturn = a.GrdReturn,
                        gat = a.DepGates
                    }).ToList();

                    if (flights.Count > 0)
                    {
                        return Ok(flights);
                    }
                    else
                    {
                        return NoContent();
                    }
                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ErrorMessage = ex.Message });
            }
        }

        [HttpGet]
        [Authorize(Roles = "Api.FlightStatus.Read")]
        [Route("api/GetCurrentDayScheduledFlight/{FlightNumber}")]
        public async Task<ActionResult> GetCurrentDayScheduledFlight(string FlightNumber)
        {
            try
            {
                var flights = _context.OtpFlightStatuses.Where(a => a.SchDepDt.Date == DateTime.Now.Date && a.FltNr == FlightNumber).Select(a => new
                {
                    refNum = a.FltSeqNr,
                    flightDate = a.FltDt,
                    flightNumber = a.FltNr,
                    legNumber = a.LegSeqNr,
                    schDepartureDate = a.SchDepDt,
                    pubSchDepartureDate = a.PubSchDepDt,
                    schArrivalDate = a.SchArvDt,
                    pubSchArrivalDate = a.PubSchArvDt,
                    offBlocks = a.ActualOffblocks,
                    onblocks = a.ActualOnblocks,
                    airBone = a.ActualAirborne,
                    landing = a.ActualLanding,
                    latestDepDate = a.LatestDepDt,
                    latestArvDate = a.LatestArvDt,
                    origin = a.SchDepArpCd,
                    destination = a.SchArvArpCd,
                    acType = a.LatestEqpCd,
                    regustration = a.LatestTailNr,
                    status = a.LegStatus,
                    estDepDate = a.EstDepDt,
                    estArvDate = a.EstArvDt,
                    lastUpdated = a.LastUpdated,
                    gReturn = a.GrdReturn,
                    gat = a.DepGates
                }).ToList();

                if (flights.Count > 0)
                {
                    return Ok(flights);
                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ErrorMessage = ex.Message });
            }
        }

        [HttpGet]
        [Authorize(Roles = "Api.FlightStatus.Read")]
        [Route("api/GetCurrentDayDepartureFlights/{Departure}")]
        public async Task<ActionResult> GetCurrentDayDepartureFlights(string Departure)
        {
            try
            {
                var flights = _context.OtpFlightStatuses.Where(a => a.SchDepDt.Date == DateTime.Now.Date && a.SchDepArpCd == Departure).Select(a => new
                {
                    refNum = a.FltSeqNr,
                    flightDate = a.FltDt,
                    flightNumber = a.FltNr,
                    legNumber = a.LegSeqNr,
                    schDepartureDate = a.SchDepDt,
                    pubSchDepartureDate = a.PubSchDepDt,
                    schArrivalDate = a.SchArvDt,
                    pubSchArrivalDate = a.PubSchArvDt,
                    offBlocks = a.ActualOffblocks,
                    onblocks = a.ActualOnblocks,
                    airBone = a.ActualAirborne,
                    landing = a.ActualLanding,
                    latestDepDate = a.LatestDepDt,
                    latestArvDate = a.LatestArvDt,
                    origin = a.SchDepArpCd,
                    destination = a.SchArvArpCd,
                    acType = a.LatestEqpCd,
                    regustration = a.LatestTailNr,
                    status = a.LegStatus,
                    estDepDate = a.EstDepDt,
                    estArvDate = a.EstArvDt,
                    lastUpdated = a.LastUpdated,
                    gReturn = a.GrdReturn,
                    gat = a.DepGates
                }).ToList();

                if (flights.Count > 0)
                {
                    return Ok(flights);
                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ErrorMessage = ex.Message });
            }
        }

        [HttpGet]
        [Authorize(Roles = "Api.FlightStatus.Read")]
        [Route("api/GetCurrentDayArivalFlights/{Arival}")]
        public async Task<ActionResult> GetCurrentDayArivalFlights(string Arival)
        {
            try
            {
                var flights = _context.OtpFlightStatuses.Where(a => a.SchDepDt.Date == DateTime.Now.Date && a.SchDepArpCd == Arival).Select(a => new
                {
                    refNum = a.FltSeqNr,
                    flightDate = a.FltDt,
                    flightNumber = a.FltNr,
                    legNumber = a.LegSeqNr,
                    schDepartureDate = a.SchDepDt,
                    pubSchDepartureDate = a.PubSchDepDt,
                    schArrivalDate = a.SchArvDt,
                    pubSchArrivalDate = a.PubSchArvDt,
                    offBlocks = a.ActualOffblocks,
                    onblocks = a.ActualOnblocks,
                    airBone = a.ActualAirborne,
                    landing = a.ActualLanding,
                    latestDepDate = a.LatestDepDt,
                    latestArvDate = a.LatestArvDt,
                    origin = a.SchDepArpCd,
                    destination = a.SchArvArpCd,
                    acType = a.LatestEqpCd,
                    regustration = a.LatestTailNr,
                    status = a.LegStatus,
                    estDepDate = a.EstDepDt,
                    estArvDate = a.EstArvDt,
                    lastUpdated = a.LastUpdated,
                    gReturn = a.GrdReturn,
                    gat = a.DepGates
                }).ToList();

                if (flights.Count > 0)
                {
                    return Ok(flights);
                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ErrorMessage = ex.Message });
            }
        }

        [HttpGet]
        [Authorize(Roles = "Api.FlightStatus.Read")]
        [Route("api/GetCurrentDayDepartureArivalFlights/{Departure}/{Arival}")]
        public async Task<ActionResult> GetCurrentDayDepartureArivalFlights(string Departure, string Arival)
        {
            try
            {
                var flights = _context.OtpFlightStatuses.Where(a => a.SchDepDt.Date == DateTime.Now.Date && a.SchDepArpCd == Departure && a.SchArvArpCd == Arival).Select(a => new
                {
                    refNum = a.FltSeqNr,
                    flightDate = a.FltDt,
                    flightNumber = a.FltNr,
                    legNumber = a.LegSeqNr,
                    schDepartureDate = a.SchDepDt,
                    pubSchDepartureDate = a.PubSchDepDt,
                    schArrivalDate = a.SchArvDt,
                    pubSchArrivalDate = a.PubSchArvDt,
                    offBlocks = a.ActualOffblocks,
                    onblocks = a.ActualOnblocks,
                    airBone = a.ActualAirborne,
                    landing = a.ActualLanding,
                    latestDepDate = a.LatestDepDt,
                    latestArvDate = a.LatestArvDt,
                    origin = a.SchDepArpCd,
                    destination = a.SchArvArpCd,
                    acType = a.LatestEqpCd,
                    regustration = a.LatestTailNr,
                    status = a.LegStatus,
                    estDepDate = a.EstDepDt,
                    estArvDate = a.EstArvDt,
                    lastUpdated = a.LastUpdated,
                    gReturn = a.GrdReturn,
                    gat = a.DepGates
                }).ToList();

                if (flights.Count > 0)
                {
                    return Ok(flights);
                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ErrorMessage = ex.Message });
            }
        }


        [HttpGet]
        [Authorize(Roles = "Api.FlightStatus.Read")]
        [Route("api/GetAllDepGates")]
        public async Task<ActionResult> GetAllDepGates()
        {
            try
            {
                var flights = _context.otpGatesdep.Select(a => new
                {
                    FLTDT = a.FltDt,
                    FLN = a.FltNr,
                    DEP = a.Dep,
                    ARR = a.Arr,
                    GAT = a.Gat,
                    BSTPLN = a.Bstpln,
                    GCLPLN = a.Gclpln
                }).ToList();

                if (flights.Count > 0)
                {
                    return Ok(flights);
                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ErrorMessage = ex.Message });
            }
        }

        [HttpGet]
        [Authorize(Roles = "Api.FlightStatus.Read")]
        [Route("api/GetAllDepGatesByDate/{Date}")]
        public async Task<ActionResult> GetAllDepGatesByDate(string Date)
        {
            try
            {
                DateTime date = Convert.ToDateTime(Date);

                var flights = _context.otpGatesdep.Select(a => new
                {
                    FLTDT = a.FltDt,
                    FLN = a.FltNr,
                    DEP = a.Dep,
                    ARR = a.Arr,
                    GAT = a.Gat,
                    BSTPLN = a.Bstpln,
                    GCLPLN = a.Gclpln
                }).Where(a => a.FLTDT.Value.Date == date.Date).ToList();

                if (flights.Count > 0)
                {
                    return Ok(flights);
                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ErrorMessage = ex.Message });
            }
        }

        [HttpGet]
        [Authorize(Roles = "Api.FlightStatus.Read")]
        [Route("api/GetDepGatesByFlight/{FlightNumber}/{Date}")]
        public async Task<ActionResult> GetDepGatesByFlight(string FlightNumber, string Date)
        {
            try
            {
                DateTime date = Convert.ToDateTime(Date);

                var flights = _context.otpGatesdep.Select(a => new
                {
                    FLTDT = a.FltDt,
                    FLN = a.FltNr,
                    DEP = a.Dep,
                    ARR = a.Arr,
                    GAT = a.Gat,
                    BSTPLN = a.Bstpln,
                    GCLPLN = a.Gclpln
                }).Where(a => a.FLTDT.Value.Date == date.Date && a.FLN == FlightNumber).ToList();

                if (flights.Count > 0)
                {
                    return Ok(flights);
                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ErrorMessage = ex.Message });
            }
        }

        [HttpGet]
        [Authorize(Roles = "Api.FlightStatus.Read")]
        [Route("api/GetDepGatesByDepArr/{Dep}/{Arr}/{Date}")]
        public async Task<ActionResult> GetDepGatesByDepArr(string Dep, string Arr, string Date)
        {
            try
            {
                DateTime date = Convert.ToDateTime(Date);

                var flights = _context.otpGatesdep.Select(a => new
                {
                    FLTDT = a.FltDt,
                    FLN = a.FltNr,
                    DEP = a.Dep,
                    ARR = a.Arr,
                    GAT = a.Gat,
                    BSTPLN = a.Bstpln,
                    GCLPLN = a.Gclpln
                }).Where(a => a.FLTDT.Value.Date == date.Date && a.DEP == Dep && a.ARR == Arr).ToList();

                if (flights.Count > 0)
                {
                    return Ok(flights);
                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ErrorMessage = ex.Message });
            }
        }
    }
}
