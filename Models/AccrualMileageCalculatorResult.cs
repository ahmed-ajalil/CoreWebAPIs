using CoreWebAPIs.Helpers;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CoreWebAPIs.Models
{

    public class AccrualMileageCalculatorResult
    {
        
        [JsonPropertyName("programCode")]
        public string ProgramCode { get; set; }
        [JsonPropertyName("membershipNumber")]
        public string MembershipNumber { get; set; }
        [JsonPropertyName("partnerCode")]
        public string PartnerCode { get; set; }
        [JsonPropertyName("flightAttributePointDetails")]
        public List<FlightAttributePointDetail> FlightAttributePointDetails { get; set; }
    }

    public class FlightAttributePointDetail
    {
        [JsonPropertyName("carrierCode")]
        public string CarrierCode { get; set; }
        [JsonPropertyName("flightNumber")]
        public string FlightNumber { get; set; }
        [JsonPropertyName("flightDate")]
        [JsonConverter(typeof(CustomDateTimeConverter))]
        public DateTime FlightDate { get; set; }
        [JsonPropertyName("cabinClass")]
        public string CabinClass { get; set; }
        [JsonPropertyName("bookingClass")]
        public string BookingClass { get; set; }
        [JsonPropertyName("originAirport")]
        public string OriginAirport { get; set; }
        [JsonPropertyName("destinationAirport")]
        public string DestinationAirport { get; set; }
        [JsonPropertyName("status")]
        public string Status { get; set; }
        [JsonPropertyName("rejectionReason")]
        public string RejectionReason { get; set; }
        [JsonPropertyName("pointCreditDetails")]
        public List<PointCreditDetail> PointCreditDetails { get; set; }
    }

    public class PointCreditDetail
    {
        [JsonPropertyName("pointType")]
        public string PointType { get; set; }
        [JsonPropertyName("points")]
        public double Points { get; set; }
        [JsonPropertyName("creditLimit")]
        public double CreditLimit { get; set; }
    }
    public class AccrualResponse
    {
        public AccrualMileageCalculatorResult AccrualMileageCalculatorResult { get; set; }
    }
}
