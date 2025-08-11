
using CoreWebAPIs.Models;

namespace CoreWebAPIs.Interfaces
{
    public interface IPhoneParserService
    {
        ParsedPhoneNumberResponse ParsePhoneNumber(string phoneNumber);
    }
}
