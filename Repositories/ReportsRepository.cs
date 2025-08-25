using CoreWebAPIs.Context;
using CoreWebAPIs.Helpers;
using CoreWebAPIs.Interfaces;
using CoreWebAPIs.Models;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Net;

namespace CoreWebAPIs.Repositories
{
    public class ReportsRepository : IReportsInterface
    {
        private readonly ProfitabilityDbContext _context;

        public ReportsRepository(ProfitabilityDbContext context)
        {
            _context = context;
        }

        public async Task<ApiResponseModel> GetReportBetweenDateRange(DateTime? fromdate = null, DateTime? toDate = null, string? route = "", string? flightNumber = "", string? departure = "", string? arrival = "")
        {
            ApiResponseModel responseModel = new ApiResponseModel();
            try
            {
                FilterDataModel model = new FilterDataModel();
                var query = _context.RvChatbotMs.AsQueryable();
                query = !string.IsNullOrEmpty(route) ? query.Where(x => x.RouteName == route) : query;
                query = !string.IsNullOrEmpty(flightNumber) ? query.Where(x => x.Fltno == flightNumber) : query;
                query = !string.IsNullOrEmpty(departure) ? query.Where(x => x.Dep == departure) : query;
                query = !string.IsNullOrEmpty(arrival) ? query.Where(x => x.Arr == arrival) : query;

                query = HelperMethods.GetReportsQueryBasedOnDates(query, fromdate, toDate);
                List<RvChatbotM> reports = await query.ToListAsync();
                model.Count = reports.Count;
                model.Reports = reports;
                if (reports != null)
                {
                    responseModel.StatusCode = HttpStatusCode.OK;
                    responseModel.Data = model;
                    responseModel.Message = "Reports Fetched";
                }
            }
            catch (Exception ex)
            {
                responseModel.StatusCode = HttpStatusCode.InternalServerError;
                responseModel.Message = $"Error: {ex.Message}";
            }
            return responseModel;
        }


        public async Task<ApiResponseModel> GetReportByMonth(string fromMonth, string? route = "", string? flightNumber = "", string? toMonth = "", string? departure = "", string? arrival = "")
        {
            ApiResponseModel responseModel = new ApiResponseModel();
            if (string.IsNullOrEmpty(fromMonth))
            {
                responseModel.StatusCode = HttpStatusCode.InternalServerError;
                responseModel.Message = $"Error: please provide the month duration";
            }
            else
            {
                try
                {
                    FilterDataModel model = new FilterDataModel();
                    var query = _context.RvChatbotMs.AsQueryable();
                    query = !string.IsNullOrEmpty(route) ? query.Where(x => x.RouteName == route) : query;
                    query = !string.IsNullOrEmpty(flightNumber) ? query.Where(x => x.Fltno == flightNumber) : query;
                    query = !string.IsNullOrEmpty(departure) ? query.Where(x => x.Dep == departure) : query;
                    query = !string.IsNullOrEmpty(arrival) ? query.Where(x => x.Arr == arrival) : query;

                    query = !string.IsNullOrEmpty(toMonth) ?
                        HelperMethods.GetReportsQueryBasedOnMonth(query, HelperMethods.ValidateMonthYear(fromMonth), HelperMethods.ValidateMonthYear(toMonth)) :
                        HelperMethods.GetReportsQueryBasedOnMonth(query, HelperMethods.ValidateMonthYear(fromMonth));
                    List<RvChatbotM> reports = await query.ToListAsync();
                    model.Count = reports.Count;
                    model.Reports = reports;
                    if (reports != null)
                    {
                        responseModel.StatusCode = HttpStatusCode.OK;
                        responseModel.Data = model;
                        responseModel.Message = "Reports Fetched";
                    }
                }
                catch (Exception ex)
                {

                    responseModel.StatusCode = HttpStatusCode.InternalServerError;
                    responseModel.Message = $"Error: {ex.Message}";
                }

            }
            return responseModel;
        }


        public async Task<ApiResponseModel> GetReportByYear(string fromYear, string? route = "", string? flightNumber = "", string? toYear = "", string? departure = "", string? arrival = "")
        {
            ApiResponseModel responseModel = new ApiResponseModel();

            if (string.IsNullOrEmpty(fromYear))
            {
                responseModel.StatusCode = HttpStatusCode.InternalServerError;
                responseModel.Message = $"Error: please provide the Year duration";
            }
            else
            {
                try
                {
                    FilterDataModel model = new FilterDataModel();
                    var query = _context.RvChatbotMs.AsQueryable();
                    query = !string.IsNullOrEmpty(route) ? query.Where(x => x.RouteName == route) : query;
                    query = !string.IsNullOrEmpty(flightNumber) ? query.Where(x => x.Fltno == flightNumber) : query;
                    query = !string.IsNullOrEmpty(departure) ? query.Where(x => x.Dep == departure) : query;
                    query = !string.IsNullOrEmpty(arrival) ? query.Where(x => x.Arr == arrival) : query;

                    query = !string.IsNullOrEmpty(toYear) ?
                        HelperMethods.GetReportsQueryBasedOnYear(query, HelperMethods.ValidateYear(fromYear), HelperMethods.ValidateYear(toYear)) :
                        HelperMethods.GetReportsQueryBasedOnYear(query, HelperMethods.ValidateYear(fromYear));
                    List<RvChatbotM> reports = await query.ToListAsync();
                    model.Count = reports.Count;
                    model.Reports = reports;
                    if (reports != null)
                    {
                        responseModel.StatusCode = HttpStatusCode.OK;
                        responseModel.Data = model;
                        responseModel.Message = "Reports Fetched";
                    }
                }
                catch (Exception ex)
                {

                    responseModel.StatusCode = HttpStatusCode.InternalServerError;
                    responseModel.Message = $"Error: {ex.Message}";
                }

            }



            return responseModel;
        }


        public async Task<ApiResponseModel> GetReportsBasedOnRouteName(string route, DateTime? fromdate = null, DateTime? toDate = null)
        {
            ApiResponseModel responseModel = new ApiResponseModel();

            if (string.IsNullOrEmpty(route))
            {

                responseModel.StatusCode = HttpStatusCode.InternalServerError;
                responseModel.Message = $"Error: Route Name is required";
            }
            else
            {
                try
                {
                    FilterDataModel model = new FilterDataModel();
                    var query = _context.RvChatbotMs.AsQueryable().Where(x => x.RouteName == route);

                    query = HelperMethods.GetReportsQueryBasedOnDates(query, fromdate, toDate);
                    List<RvChatbotM> reports = await query.ToListAsync();
                    model.Count = reports.Count;
                    model.Reports = reports;
                    if (reports != null)
                    {
                        responseModel.StatusCode = HttpStatusCode.OK;
                        responseModel.Data = model;
                        responseModel.Message = "Reports Fetched";
                    }
                }
                catch (Exception ex)
                {

                    responseModel.StatusCode = HttpStatusCode.InternalServerError;
                    responseModel.Message = $"Error: {ex.Message}";
                }
            }

            return responseModel;
        }

        public async Task<ApiResponseModel> GetReportsBasedOnFlightNumber(string flightNumber, DateTime? fromdate = null, DateTime? toDate = null)
        {
            ApiResponseModel responseModel = new ApiResponseModel();

            if (string.IsNullOrEmpty(flightNumber))
            {

                responseModel.StatusCode = HttpStatusCode.InternalServerError;
                responseModel.Message = $"Error: Route Name is required";
            }
            else
            {
                try
                {
                    FilterDataModel model = new FilterDataModel();
                    var query = _context.RvChatbotMs.AsQueryable().Where(x => x.Fltno == flightNumber);

                    query = HelperMethods.GetReportsQueryBasedOnDates(query, fromdate, toDate);
                    List<RvChatbotM> reports = await query.ToListAsync();
                    model.Count = reports.Count;
                    model.Reports = reports;
                    if (reports != null)
                    {
                        responseModel.StatusCode = HttpStatusCode.OK;
                        responseModel.Data = model;
                        responseModel.Message = "Reports Fetched";
                    }
                }
                catch (Exception ex)
                {

                    responseModel.StatusCode = HttpStatusCode.InternalServerError;
                    responseModel.Message = $"Error: {ex.Message}";
                }
            }

            return responseModel;
        }




        public static IQueryable<RvChatbotM> GetReportsQueryBasedOnDates(IQueryable<RvChatbotM> query, DateTime? fromFlightDate = null, DateTime? toFlightDate = null)
        {
            if (fromFlightDate.HasValue && toFlightDate.HasValue)
            {
                query = query.Where(x =>
                    x.Localfltdate == null ||
                    (x.Localfltdate.Value.Date >= fromFlightDate.Value.Date && x.Localfltdate.Value.Date <= toFlightDate.Value.Date));
            }
            else if (fromFlightDate.HasValue)
            {
                query = query.Where(x =>
                    x.Localfltdate == null ||
                    x.Localfltdate.Value.Date == ConvertDateTimeToUTC(fromFlightDate.Value).Date);
            }
            else if (toFlightDate.HasValue)
            {
                query = query.Where(x =>
                    x.Localfltdate == null ||
                    x.Localfltdate.Value.Date == ConvertDateTimeToUTC(toFlightDate.Value).Date);
            }
            else
            {
                query = query.Where(x =>
                    x.Localfltdate == null ||
                    x.Localfltdate.Value.Date == DateTime.UtcNow.Date);
            }

            return query;
        }


        private static DateTime ConvertDateTimeToUTC(DateTime? dt)
        {
            DateTime date = DateTime.UtcNow;
            try
            {

                date = dt != null ? DateTime.Parse(dt?.ToString(), CultureInfo.InvariantCulture) : date;
            }
            catch (Exception)
            {

                throw new ArgumentException("Invalid date format.");
            }

            return date;
        }

    }
}
