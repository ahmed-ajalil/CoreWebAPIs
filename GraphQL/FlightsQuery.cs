

using CoreWebAPIs.Interfaces;
using CoreWebAPIs.Models;

namespace CoreWebAPIs.GraphQL
{
    public class FlightsQuery
    {
        private readonly IFlightsInterface _flights;
        public FlightsQuery(IFlightsInterface flights)
        {
            _flights = flights;
        }


        public async Task<FilteredFlightsDataModel> GetFlightByFlightNumber
            (string flightNumber, DateTime? fromFlightDate = null,
            DateTime? toFlightDate = null) => (await _flights.GetFlightByFlightNumber(flightNumber, fromFlightDate, toFlightDate))?.Data;

        public async Task<FilteredFlightsDataModel> GetFlightsByDate
            (DateTime? fromFlightDate = null, DateTime? toFlightDate = null) => (await _flights.GetFlightsByDate(fromFlightDate, toFlightDate))?.Data;

        public async Task<FilteredFlightsDataModel> GetCanclledFlights(DateTime? FlightDate = null) => (await _flights.GetCanclledFlights(FlightDate))?.Data;

        public async Task<FilteredFlightsDataModel> GetFlightsByDestination
            (string SCHDep, DateTime? fromFlightDate = null,
            DateTime? toFlightDate = null) => (await _flights.GetFlightsByDestination(SCHDep, fromFlightDate, toFlightDate))?.Data;

        public async Task<FilteredFlightsDataModel> GetFlightsByDestinationCount
            (string destination, DateTime? fromFlightDate = null,
            DateTime? toFlightDate = null) => (await _flights.GetFlightsByDestinationCount(destination, fromFlightDate, toFlightDate))?.Data;

        public async Task<FilteredFlightsDataModel> GetFlightsByDeparture
            (string departure, DateTime? fromFlightDate = null,
            DateTime? toFlightDate = null) => (await _flights.GetFlightsByDeparture(departure, fromFlightDate, toFlightDate))?.Data;

        public async Task<FilteredFlightsDataModel> GetDelayedFlights
            (DateTime? fromFlightDate = null,
            DateTime? toFlightDate = null) => (await _flights.GetDelayedFlights(fromFlightDate, toFlightDate))?.Data;

        public async Task<FilteredFlightsDataModel> GetArrivedFlights
            (string? ArrivedTo, DateTime? fromFlightDate = null,
            DateTime? toFlightDate = null) => (await _flights.GetArrivedFlights(ArrivedTo, fromFlightDate, toFlightDate))?.Data;

        public async Task<FilteredFlightsDataModel> GetDepartedFlights
            (string? DepartedFrom, DateTime flightDate) => (await _flights.GetDepartedFlights(DepartedFrom, flightDate))?.Data;


        public async Task<FilteredFlightsDataModel> GetFlightsStatusBetweenDates
            (string? departure, string? arrival, DateTime fromFlightDate,
            DateTime toFlightDate) => (await _flights.GetFlightsStatusBetweenDates(departure, arrival, fromFlightDate, toFlightDate))?.Data;

        public async Task<FilteredFlightsDataModel> GetDiversionFlights
            (DateTime? fromFlightDate = null,
            DateTime? toFlightDate = null) => (await _flights.GetDiversionFlights(fromFlightDate, toFlightDate))?.Data;


    }
}
