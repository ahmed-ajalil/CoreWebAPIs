namespace CoreWebAPIs.Models
{
    public class FlightsModel
    {
        public string? FlightNumber { get; set; }

        public DateTime? FlightDate { get; set; }

        public string? ActualDepartedAirport { get; set; }

        public string? ActualArrivedAirport { get; set; }

        public DateTime? SchedualDepartureDate { get; set; }

        public DateTime? SchedualArrivedDate { get; set; }

        public DateTime? PublishedDepartureDate { get; set; }

        public DateTime? PublishedArrivedDate { get; set; }

        public DateTime? ActualDepartureDate { get; set; }

        public DateTime? ActualArrivedDate { get; set; }

        public string? LatestTailNubmer { get; set; }

        public string? LatestEqpCd { get; set; }

        public int IsFlightCancled { get; set; }

        public int IsFlightDelayed { get; set; }

        public int IsFlightDeparted { get; set; }

        public int IsFlightArrived { get; set; }
    }
}
