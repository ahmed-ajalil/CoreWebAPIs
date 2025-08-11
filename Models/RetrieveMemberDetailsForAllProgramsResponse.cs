namespace CoreWebAPIs.Models
{
    public class RetrieveMemberDetailsForAllProgramsResponse
    {
        public string CompanyCode { get; set; }
        public string MembershipNumber { get; set; }
        public ProfileDetails ProfileDetails { get; set; }
        public List<ProgramDetail> ProgramDetails { get; set; }
    }

    public class ProfileDetails
    {
        public string CustomerNumber { get; set; }
        public string CustomerType { get; set; }
        public string EnrollmentSource { get; set; }
        public string CustomerSince { get; set; }
        public bool WebLoginDisabled { get; set; }
        public string WebLoginReason { get; set; }
        public string Status { get; set; }
        public IndividualInfo IndividualInfo { get; set; }
    }

    public class IndividualInfo
    {
        public string Nationality { get; set; }
        public string PreferredLanguage { get; set; }
        public string PreferredAddress { get; set; }
        public string PreferredEmailAddress { get; set; }
        public string PreferredPhoneNumber { get; set; }
        public string Title { get; set; }
        public string GivenName { get; set; }
        public string FamilyName { get; set; }
        public string DisplayName { get; set; }
        public string SecondName { get; set; }
        public string SecondLastName { get; set; }
        public string MarriedName { get; set; }
        public string Initials { get; set; }
        public string Gender { get; set; }
        public string MaritalStatus { get; set; }
        public string DateOfBirth { get; set; }
        public string CountryOfResidence { get; set; }
        public string CompanyName { get; set; }
        public string Position { get; set; }
        public List<MemberContactInfo> MemberContactInfos { get; set; }
        public string StaffId { get; set; }
        public string TypeOfIndustry { get; set; }
        public string IncomeBand { get; set; }
    }

    public class MemberContactInfo
    {
        public string AddressType { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneISDCode { get; set; }
        public string PhoneAreaCode { get; set; }
        public string PhoneNumber { get; set; }
        public string MobileISDCode { get; set; }
        public string MobileAreaCode { get; set; }
        public string MobileNumber { get; set; }
        public string FaxISDCode { get; set; }
        public string FaxAreaCode { get; set; }
        public string Fax { get; set; }
        public string SkypeID { get; set; }
        public string PostalAddressStatus { get; set; }
        public string EmailAddressStatus { get; set; }
        public string PhoneNumberStatus { get; set; }
        public string MobileNumberStatus { get; set; }
        public string FaxNumberStatus { get; set; }
    }

    public class Preference
    {
        public string PreferenceCode { get; set; }
        public int SequenceNumber { get; set; }
        public string PreferenceGroupName { get; set; }
        public string PreferenceValue { get; set; }
    }

    public class AllProgDynamicAttribute
    {
        public string AttributeCode { get; set; }
        public string GroupInstanceID { get; set; }
        public string AttributeValue { get; set; }
        public string DynamicAttributeGroupName { get; set; }
    }

    public class ProgramDetail
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
        public List<PointDetail> PointDetails { get; set; }
    }

    public class PointDetail
    {
        public string PointType { get; set; }
        public double Points { get; set; }
    }

    public class AllProgTxnHeader
    {
        public string UserName { get; set; }
        public string ChannelUserCode { get; set; }
        public string TransactionToken { get; set; }
        public string DeviceId { get; set; }
        public string DeviceIP { get; set; }
        public string DeviceOperatingSystem { get; set; }
        public string DeviceLocationLatitude { get; set; }
        public string DeviceLocationLongitude { get; set; }
        public string DeviceCountryCode { get; set; }
        public string AdditionalInfo { get; set; }
        public string Remarks { get; set; }
        public string CachedService { get; set; }
    }

}
