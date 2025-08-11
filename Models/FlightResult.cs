namespace CoreWebAPIs.Models
{
    public class FlightResult
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public int TotalFlights { get; set; }
        public FlightInfo? Flight { get; set; }
    }

    public class FlightsResult
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public int TotalFlights { get; set; }
        public List<FlightInfo>? Flights { get; set; }
    }

    public class FlightInfo
    {
        public string FlightNumber { get; set; } = string.Empty;
        public string FlightDate { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string CurrentStatus { get; set; } = string.Empty;
        public string FlyingHours { get; set; } = string.Empty;

        public int LegSequenceNumber { get; set; }

        public AirportInfo? DepartureAirport { get; set; }
        public string ScheduledDeparture { get; set; } = string.Empty;
        public string? ActualDeparture { get; set; }

        public AirportInfo? ArrivalAirport { get; set; }
        public string ScheduledArrival { get; set; } = string.Empty;
        public string? ActualArrival { get; set; }
    }

    public class AirportInfo
    {
        public string Code { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string AirportName { get; set; } = string.Empty;
        public int TimeDifference { get; set; }
    }
}
