namespace CoreWebAPIs.Models
{
    public class AccrualMileageCalculatorFilter
    {
        public string CompanyCode { get; set; } 
        public string ProgramCode { get; set; } 
        public string PartnerCode { get; set; } 
        public List<FlightAttribute> FlightAttributes { get; set; }
    }

    public class FlightAttribute
    {
        public string CarrierCode { get; set; } 
        public string CabinClass { get; set; }
        public string OriginAirport { get; set; }
        public string DestinationAirport { get; set; }
        public List<PartnerAccrualDynamicAttribute> PartnerAccrualDynamicAttributes { get; set; }
    }

    public class PartnerAccrualDynamicAttribute
    {
        public string AttributeKey { get; set; } 
        public string AttributeValue { get; set; }
        public string AttributeName { get; set; } 
        public string FieldType { get; set; } 
        public string AttributeMapping { get; set; } 
        public string Key { get; set; } 
    }
}
