using CoreWebAPIs.Interfaces;
using CoreWebAPIs.Models;
using PhoneNumbers;

namespace CoreWebAPIs.Services
{
    public class PhoneParserService : IPhoneParserService
    {
        private readonly PhoneNumberUtil _phoneUtil = PhoneNumberUtil.GetInstance();

        public ParsedPhoneNumberResponse ParsePhoneNumber(string phoneNumber)
        {
            var request = new PhoneNumberRequest
            {
                PhoneNumber = phoneNumber,
                RegionCode = "BH" 
            };

            var response = new ParsedPhoneNumberResponse
            {
                OriginalNumber = request.PhoneNumber
            };

            try
            {
                PhoneNumber phoneNumberProto = _phoneUtil.Parse(request.PhoneNumber, request.RegionCode.ToUpper());

                response.IsValid = _phoneUtil.IsValidNumber(phoneNumberProto);

                if (response.IsValid)
                {
                    response.CountryCode = phoneNumberProto.CountryCode;
                    response.NationalNumber = phoneNumberProto.NationalNumber;
                    response.E164Format = _phoneUtil.Format(phoneNumberProto, PhoneNumberFormat.E164);
                }
                else
                {
                    response.ErrorMessage = "The phone number is not valid for the specified region.";
                }
            }
            catch (NumberParseException e)
            {
                response.IsValid = false;
                response.ErrorMessage = $"Failed to parse phone number: {e.Message}";
            }

            return response;


        }
    }
}
