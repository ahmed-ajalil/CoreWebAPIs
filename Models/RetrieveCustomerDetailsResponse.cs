namespace CoreWebAPIs.Models
{
    public class RetrieveCustomerDetailsResponse
    {
        public List<CustomerDetail> CustomerDetails { get; set; }
    }

    public class CustomerDetail
    {
        public string CustomerNumber { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string SurName { get; set; }
        public string MiddleName { get; set; }
        public string SecondName { get; set; }
        public string SecondLastName { get; set; }
        public string MarriedName { get; set; }
        public string EmailAddress { get; set; }
        public string DateOfBirth { get; set; }
        public string MobileNumber { get; set; }
        public string AddressLine1 { get; set; }
        public string PostalCode { get; set; }
        public string PhoneNumber { get; set; }
        public string CountryOfResidence { get; set; }
        public string Nationality { get; set; }
        public string CustomerStatus { get; set; }
        public string CustomerType { get; set; }
    }

    public class RCDynamicAttribute
    {
        public string AttributeGroupName { get; set; }
        public string GroupInstanceID { get; set; }
        public string AttributeName { get; set; }
        public string AttributeCode { get; set; }
        public string AttributeValue { get; set; }
    }

    public class PrivacySetting
    {
        public bool IsPrivate { get; set; }
    }

    public class RCTxnHeader
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
