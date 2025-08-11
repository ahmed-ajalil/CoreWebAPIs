

using CoreWebAPIs.Helpers;

namespace CoreWebAPIs.Interfaces
{
    public interface IFlightsInterface
    {

        Task<ApiResponseModel> GetCanclledFlights(DateTime? FlightDate = null);

        Task<ApiResponseModel> GetFlightsByDate(DateTime? fromFlightDate = null, DateTime? toFlightDate = null);

        Task<ApiResponseModel> GetFlightByFlightNumber(string flightNumber, DateTime? fromFlightDate = null, DateTime? toFlightDate = null);
        Task<ApiResponseModel> GetFlightsByDestination(string SCHDep, DateTime? fromFlightDate = null, DateTime? toFlightDate = null);

        Task<ApiResponseModel> GetFlightsByDestinationCount(string destination, DateTime? fromFlightDate = null, DateTime? toFlightDate = null);

        Task<ApiResponseModel> GetFlightsByScheduleArrival(string SCHArr, DateTime fromFlightDate, DateTime toFlightDate);

        Task<ApiResponseModel> GetFlightsByActualDeparture(string ACTUDep, DateTime fromFlightDate, DateTime toFlightDate);

        Task<ApiResponseModel> GetFlightsByDeparture(string departure, DateTime? fromFlightDate = null, DateTime? toFlightDate = null);
        Task<ApiResponseModel> GetDelayedFlights(DateTime? fromFlightDate = null, DateTime? toFlightDate = null);
        Task<ApiResponseModel> GetDepartedFlights(string? DepartedFrom, DateTime flightDate);

        Task<ApiResponseModel> GetAirReturnFlights(DateTime? fromFlightDate = null, DateTime? toFlightDate = null);
        Task<ApiResponseModel> GetDiversionFlights(DateTime? fromFlightDate = null, DateTime? toFlightDate = null);
        Task<ApiResponseModel> GetArrivedFlights(string? ArrivedTo, DateTime? fromFlightDate = null, DateTime? toFlightDate = null);

        Task<ApiResponseModel> GetFlightsStatusBetweenDates(string? departure, string? arrival, DateTime fromFlightDate, DateTime toFlightDate);

        Task<ApiResponseModel> GetFlightsByActualArrival(string ACTUArr, DateTime fromFlightDate, DateTime toFlightDate);

        Task<ApiResponseModel> GetFlightsBySchedulFleet(string SCHFleet, DateTime fromFlightDate, DateTime toFlightDate);
        Task<ApiResponseModel> GetFlightsByActualFleet(string ACTUFleet, DateTime fromFlightDate, DateTime toFlightDate);

    }
}
