namespace CoreWebAPIs.Models
{
    public class UseMilesRequest
    {
        public string companyCode { get; set; }
        public string programCode { get; set; }
        public string partnerCode { get; set; }
        public string origin { get; set; }
        public string destination { get; set; }
        public string rewardGroup { get; set; }
        public int pageNumber { get; set; }
        public int absoluteIndex { get; set; }
        public int cost { get; set; }
        public int rewardDiscount { get; set; }
        public int actualCostofRedemption { get; set; }
        public int excessWeight { get; set; }
        public List<DynamicAttribute> dynamicAttributes { get; set; }
    }

    public class DynamicAttribute
    {
        public string attributeCode { get; set; }
        public string attributeValue { get; set; }
    }
}
