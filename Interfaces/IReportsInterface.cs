

using CoreWebAPIs.Helpers;

namespace CoreWebAPIs.Interfaces
{
    public interface IReportsInterface
    {

        Task<ApiResponseModel> GetReportBetweenDateRange(DateTime? fromdate = null, DateTime? toDate = null, string? route = "", string? flightNumber = "", string? departure = "", string? arrival = "");
        Task<ApiResponseModel> GetReportsBasedOnRouteName(string route, DateTime? fromdate = null, DateTime? toDate = null);

        Task<ApiResponseModel> GetReportsBasedOnFlightNumber(string flightNumber, DateTime? fromdate = null, DateTime? toDate = null);

        Task<ApiResponseModel> GetReportByMonth(string fromMonth, string? route = "", string? flightNumber = "", string? toMonth = "", string? departure = "", string? arrival = "");

        Task<ApiResponseModel> GetReportByYear(string fromYear, string? route = "", string? flightNumber = "", string? toYear = "", string? departure = "", string? arrival = "");

    }
}
