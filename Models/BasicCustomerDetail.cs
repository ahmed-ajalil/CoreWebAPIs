using System.Text.Json.Serialization;

namespace CoreWebAPIs.Models
{
    public class BasicCustomerDetail
    {
        public string MembershipNumber { get; set; }

        [JsonPropertyName("customerNumber")]
        public string CustomerNumber { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("firstName")]
        public string FirstName { get; set; }

        [JsonPropertyName("surName")]
        public string SurName { get; set; }

        [JsonPropertyName("middleName")]
        public string MiddleName { get; set; }

        [JsonPropertyName("secondName")]
        public string SecondName { get; set; }

        [JsonPropertyName("secondLastName")]
        public string SecondLastName { get; set; }

        [JsonPropertyName("marriedName")]
        public string MarriedName { get; set; }

        [JsonPropertyName("emailAddress")]
        public string EmailAddress { get; set; }

        [JsonPropertyName("dateOfBirth")]
        public string DateOfBirth { get; set; }

        [JsonPropertyName("mobileNumber")]
        public string MobileNumber { get; set; }

        [JsonPropertyName("addressLine1")]
        public string AddressLine1 { get; set; }

        [JsonPropertyName("postalCode")]
        public string PostalCode { get; set; }

        [JsonPropertyName("phoneNumber")]
        public string PhoneNumber { get; set; }

        [JsonPropertyName("countryOfResidence")]
        public string CountryOfResidence { get; set; }

        [JsonPropertyName("nationality")]
        public string Nationality { get; set; }

        [JsonPropertyName("customerStatus")]
        public string CustomerStatus { get; set; }

        [JsonPropertyName("customerType")]
        public string CustomerType { get; set; }
    }
}
