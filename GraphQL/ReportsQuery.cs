

using CoreWebAPIs.Helpers;
using CoreWebAPIs.Interfaces;
using CoreWebAPIs.Models;

namespace CoreWebAPIs.GraphQL
{
    public class ReportsQuery
    {
        private readonly IReportsInterface _reports;
        public ReportsQuery(IReportsInterface reports)
        {
            _reports = reports;

        }

        public async Task<FilterDataModel> GetReportBetweenDateRange(DateTime? fromdate = null,
            DateTime? toDate = null, string? route = "", string? flightNumber = "",
            string? departure = "", string? arrival = "")
            => (await _reports.GetReportBetweenDateRange(fromdate, toDate, route, flightNumber, departure, arrival))?.Data;

        public async Task<ApiResponseModel> GetReportByMonth(string fromMonth, string? route = "",
            string? flightNumber = "", string? toMonth = "", string? departure = "",
            string? arrival = "") => (await _reports.GetReportByMonth(fromMonth, route, flightNumber, toMonth, departure, arrival))?.Data;
        public async Task<ApiResponseModel> GetReportByYear(string fromYear, string? route = "",
            string? flightNumber = "", string? toYear = "", string? departure = "",
            string? arrival = "") => (await _reports.GetReportByYear(fromYear, route, flightNumber, toYear, departure, arrival))?.Data;

        public async Task<ApiResponseModel> GetReportsBasedOnRouteName(string route, DateTime? fromdate = null,
            DateTime? toDate = null) => (await _reports.GetReportsBasedOnRouteName(route, fromdate, toDate))?.Data;

        public async Task<ApiResponseModel> GetReportsBasedOnFlightNumber(string flightNumber, DateTime? fromdate = null,
            DateTime? toDate = null) => (await _reports.GetReportsBasedOnFlightNumber(flightNumber, fromdate, toDate))?.Data;
    }
}
