namespace CoreWebAPIs.Models
{
    public class UpgradeMilesResponse
    {
        public List<Rewards> Reward { get; set; }
    }
    public class Rewards
    {
        public string CompanyCode { get; set; }
        public string ProgramCode { get; set; }
        public string PartnerCode { get; set; }
        public string RewardCode { get; set; }
        public string RewardName { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public bool IsGroupRedeemable { get; set; }
        public bool IsExternal { get; set; }
        public string Description { get; set; }
        public string RewardGroup { get; set; }
        public string RewardSubCategory { get; set; }
        public string RewardCertificateType { get; set; }
        public string ValidityType { get; set; }
        public int RewardCertificateValidity { get; set; }
        public DateTime? ValidityStartDate { get; set; }
        public DateTime? ValidityEndDate { get; set; }
        public string RewardCostType { get; set; }
        public double RewardCost { get; set; }
        public string RewardCostCurrency { get; set; }
        public string Status { get; set; }
        public string UserCode { get; set; }
        public string BillingPartner { get; set; }
        public int CancellationAllowedPeriod { get; set; }
        public int ExtendExpiryPeriod { get; set; }
        public string ExtendExpiryFrom { get; set; }
        public RewardPricingDetails RewardPricingDetail { get; set; }
        public string TripType { get; set; }
        public bool HasNextPage { get; set; }
        public int AbsoluteIndex { get; set; }
    }

    public class RewardPricingDetails
    {
        public double RewardPoints { get; set; }
        public double MinimumPoints { get; set; }
        public double MaximumPoints { get; set; }
    }
}
