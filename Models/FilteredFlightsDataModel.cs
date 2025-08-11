namespace CoreWebAPIs.Models
{
    public class FilteredFlightsDataModel
    {
        public int TotalFlights { get; set; }

        public List<FlightsModel>? Flights { get; set; }
    }
}
