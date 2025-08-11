namespace CoreWebAPIs.Models
{
    public class MilesCalculatorRequest
    {
        public string CabinClass { get; set; }
        public string OriginAirport { get; set; }
        public string DestinationAirport { get; set; }
        public int AttributeValue { get; set; }
    }

    public class UsedMilesCalculatorRequest
    {
        public string OriginAirport { get; set; }
        public string DestinationAirport { get; set; }
        public string AttributeValue { get; set; }
    }
    public class RequiredMilesCalculatorRequest
    {
        public string OriginAirport { get; set; }
        public string DestinationAirport { get; set; }
        public string BookingClass { get; set; }
    }

}
