namespace CoreWebAPIs.Models
{
    public class BlackMemberDetailsResponse
    {
        public MemberIndividualInfo IndividualInfo { get; set; }
        public FilteredProgramDetail ProgramDetails { get; set; }
        public BasicCustomerDetail CustomerDetails { get; set; }
    }

    public class FilteredProgramDetail
    {
        public string ProgramCode { get; set; }
        public string ProgramName { get; set; }
        public string EnrollmentDate { get; set; }
        public string ExpiryDate { get; set; }
        public string AccountStatus { get; set; }
        public bool Suspended { get; set; }
        public string TierCode { get; set; }
        public string TierName { get; set; }
        public string TierFromDate { get; set; }
        public string TierToDate { get; set; }
        public string PreviousTierCode { get; set; }
        public string LastTierChangeReasonCode { get; set; }
        public List<MemberPointDetail> PointDetails { get; set; }
    }
}
