using System.ComponentModel.DataAnnotations;

namespace CoreWebAPIs.Models
{
    public class AiportTimeZone
    {
        public int Id { get; set; }
        public string AirportCode { get; set; }
        public int? TimeDiff { get; set; }
    }
}
