using CoreWebAPIs.Helpers;
using CoreWebAPIs.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CoreWebAPIs.Controllers
{
    [ApiController]
    [Route("api/report")]
    public class RootVisionController : ControllerBase
    {
        private readonly IReportsInterface _reports;
        public RootVisionController(IReportsInterface reports)
        {
            _reports = reports;
        }
        [HttpGet]
        [Route("getreportbetweendaterange")]
        public async Task<ApiResponseModel> GetReportBetweenDateRange([FromQuery] DateTime? fromdate = null, DateTime? toDate = null, string? route = "", string? flightNumber = "", string? departure = "", string? arrival = "")
        {
            ApiResponseModel model = new ApiResponseModel();
            
            try
            {
                model = await _reports.GetReportBetweenDateRange(fromdate, toDate, route, flightNumber, departure, arrival);
            }
            catch (Exception ex)
            {

                model.StatusCode = HttpStatusCode.InternalServerError;
                model.Message = ex.Message;
            }


            return model;
        }
        [HttpGet]
        [Route("getreportbymonth")]
        public async Task<ApiResponseModel> GetReportByMonth([FromQuery] string fromMonth, string? route = "", string? flightNumber = "", string? toMonth = "", string? departure = "", string? arrival = "")
        {
            ApiResponseModel model = new ApiResponseModel();

            try
            {
                model = await _reports.GetReportByMonth(fromMonth, route, flightNumber, toMonth, departure, arrival);
            }
            catch (Exception ex)
            {

                model.StatusCode = HttpStatusCode.InternalServerError;
                model.Message = ex.Message;
            }


            return model;
        }
        [HttpGet]
        [Route("getreportbyyear")]
        public async Task<ApiResponseModel> GetReportByYear([FromQuery] string fromYear, string? route = "", string? flightNumber = "", string? toYear = "", string? departure = "", string? arrival = "")
        {
            ApiResponseModel model = new ApiResponseModel();

            try
            {
                model = await _reports.GetReportByYear(fromYear, route, flightNumber, toYear, departure, arrival);
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
