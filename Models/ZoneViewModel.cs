namespace CoreWebAPIs.Models
{
    public class ZoneViewModel
    {
        public int ZoneId { get; set; }
        public string ZoneName { get; set; }

        public List<AirportsViewModel> Airports { get; set; }
    }

    public class AirportsViewModel
    {
        public string City { get; set; }

        public string Code { get; set; }

        public string AirportName { get; set; }

        public string? Country { get; set; }

        public string? ZoneName { get; set; }
    }
}
