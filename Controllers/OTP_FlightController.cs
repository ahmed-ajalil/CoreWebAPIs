using CoreWebAPIs.Helpers;
using CoreWebAPIs.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CoreWebAPIs.Controllers
{
    [ApiController]
    [Route("api/flight")]
    public class OTPFlightsController : Controller
    {

        private readonly IFlightsInterface _flights;
        public OTPFlightsController(IFlightsInterface flightsInterface)
        {
            _flights = flightsInterface;
        }



        [HttpGet]
        [Route("getflightsbydate")]
        public async Task<ApiResponseModel> GetFlightsByDate([FromQuery] DateTime? fromFlightDate = null, DateTime? toFlightDate = null)
        {
            ApiResponseModel model = new ApiResponseModel();

            try
            {
                model = await _flights.GetFlightsByDate(fromFlightDate, toFlightDate);
            }
            catch (Exception ex)
            {

                model.StatusCode = HttpStatusCode.InternalServerError;
                model.Message = ex.Message;
            }


            return model;
        }

        [HttpGet]
        [Route("getflightsbydestination")]
        public async Task<ApiResponseModel> GetFlightsByDestination([FromQuery] string destination, DateTime? fromFlightDate = null, DateTime? toFlightDate = null)
        {
            ApiResponseModel model = new ApiResponseModel();

            try
            {
                model = await _flights.GetFlightsByDestination(destination, fromFlightDate, toFlightDate);
            }
            catch (Exception ex)
            {

                model.StatusCode = HttpStatusCode.InternalServerError;
                model.Message = ex.Message;
            }


            return model;
        }

        [HttpGet]
        [Route("getflightsbydestinationcount")]
        public async Task<ApiResponseModel> GetFlightsByDestinationCount([FromQuery] string destination, DateTime? fromFlightDate = null, DateTime? toFlightDate = null)
        {
            ApiResponseModel model = new ApiResponseModel();

            try
            {
                model = await _flights.GetFlightsByDestinationCount(destination, fromFlightDate, toFlightDate);
            }
            catch (Exception ex)
            {

                model.StatusCode = HttpStatusCode.InternalServerError;
                model.Message = ex.Message;
            }


            return model;
        }


        [HttpGet]
        [Route("getflightbyflightnumber")]
        public async Task<ApiResponseModel> GetFlightByFlightNumber([FromQuery] string flightNumber, DateTime? fromFlightDate = null, DateTime? toFlightDate = null)
        {
            ApiResponseModel model = new ApiResponseModel();

            try
            {
                model = await _flights.GetFlightByFlightNumber(flightNumber, fromFlightDate, toFlightDate);
            }
            catch (Exception ex)
            {

                model.StatusCode = HttpStatusCode.InternalServerError;
                model.Message = ex.Message;
            }


            return model;
        }

        [HttpGet]
        [Route("getcancelledflights")]
        public async Task<ApiResponseModel> GetCancelledFlights([FromQuery] DateTime? fromFlightDate = null)
        {
            ApiResponseModel model = new ApiResponseModel();

            try
            {
                model = await _flights.GetCanclledFlights(fromFlightDate);
            }
            catch (Exception ex)
            {

                model.StatusCode = HttpStatusCode.InternalServerError;
                model.Message = ex.Message;
            }


            return model;
        }

        [HttpGet]
        [Route("getflightsbydeparture")]
        public async Task<ApiResponseModel> GetFlightsByDeparture([FromQuery] string departure, DateTime? fromFlightDate = null, DateTime? toFlightDate = null)
        {
            ApiResponseModel model = new ApiResponseModel();

            try
            {
                model = await _flights.GetFlightsByDeparture(departure, fromFlightDate, toFlightDate);
            }
            catch (Exception ex)
            {

                model.StatusCode = HttpStatusCode.InternalServerError;
                model.Message = ex.Message;
            }


            return model;
        }

        [HttpGet]
        [Route("getdelayedflights")]
        public async Task<ApiResponseModel> GetDelayedFlights([FromQuery] DateTime? fromFlightDate = null, DateTime? toFlightDate = null)
        {
            ApiResponseModel model = new ApiResponseModel();

            try
            {
                model = await _flights.GetDelayedFlights(fromFlightDate, toFlightDate);
            }
            catch (Exception ex)
            {

                model.StatusCode = HttpStatusCode.InternalServerError;
                model.Message = ex.Message;
            }


            return model;
        }

        [HttpGet]
        [Route("getdepartedflights")]
        public async Task<ApiResponseModel> GetDepartedFlights([FromQuery] string? DepartedFrom, DateTime flightDate)
        {
            ApiResponseModel model = new ApiResponseModel();

            try
            {
                model = await _flights.GetDepartedFlights(DepartedFrom, flightDate);
            }
            catch (Exception ex)
            {

                model.StatusCode = HttpStatusCode.InternalServerError;
                model.Message = ex.Message;
            }


            return model;
        }

        [HttpGet]
        [Route("getarrivedflights")]
        public async Task<ApiResponseModel> GetArrivedFlights([FromQuery] string? ArrivedTo, DateTime? fromFlightDate = null, DateTime? toFlightDate = null)
        {
            ApiResponseModel model = new ApiResponseModel();

            try
            {
                model = await _flights.GetArrivedFlights(ArrivedTo, fromFlightDate, toFlightDate);
            }
            catch (Exception ex)
            {

                model.StatusCode = HttpStatusCode.InternalServerError;
                model.Message = ex.Message;
            }


            return model;
        }

        [HttpGet]
        [Route("getflightstatusbetweendates")]
        public async Task<ApiResponseModel> GetFlightStatusBetweenDates([FromQuery] string? departure, string? arrival, DateTime fromFlightDate, DateTime toFlightDate)
        {
            ApiResponseModel model = new ApiResponseModel();

            try
            {
                model = await _flights.GetFlightsStatusBetweenDates(departure, arrival, fromFlightDate, toFlightDate);
            }
            catch (Exception ex)
            {

                model.StatusCode = HttpStatusCode.InternalServerError;
                model.Message = ex.Message;
            }


            return model;
        }

        [HttpGet]
        [Route("getdivertedflights")]
        public async Task<ApiResponseModel> GetDivertedFlights([FromQuery] DateTime fromFlightDate, DateTime toFlightDate)
        {
            ApiResponseModel model = new ApiResponseModel();

            try
            {
                model = await _flights.GetDiversionFlights(fromFlightDate, toFlightDate);
            }
            catch (Exception ex)
            {

                model.StatusCode = HttpStatusCode.InternalServerError;
                model.Message = ex.Message;
            }


            return model;
        }
    }
}
