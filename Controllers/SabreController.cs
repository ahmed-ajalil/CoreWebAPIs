using CoreWebAPIs.Interfaces;
using CoreWebAPIs.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using CoreWebAPIs.Services;

namespace CoreWebAPIs.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    // Optional: Uncomment the below line if you later add Azure AD
    // [Authorize(Roles = "Api.MilesCalculator.Read")]
    public class SabreController : ControllerBase
    {
        private readonly ISabreService _sabreService;
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;

        public SabreController(ISabreService sabreService, HttpClient httpClient, IConfiguration config)
        {
            _sabreService = sabreService;
            _httpClient = httpClient;
            _config = config;
        }

        // 🔹 Trip Search by Passenger Name (to get Locator / PNR)
        [HttpPost("searchByFirstLastName")]
        public async Task<IActionResult> SearchTrips([FromBody] TripSearchRequest request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.FirstName) || string.IsNullOrWhiteSpace(request.LastName))
                return BadRequest("FirstName and LastName are required.");

            var result = await _sabreService.SearchTripsAsync(request);
            return Ok(result);
        }

        // 🔹 Trip Search by Frequent Flyer (to get Locator / PNR)
        [HttpPost("searchByFFP")]
        public async Task<IActionResult> SearchByFfp([FromBody] FfpSearchRequest request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.MembershipId))
                return BadRequest("MembershipId is required.");

            var result = await _sabreService.SearchByFfpAsync(request);
            return Ok(result);
        }

        // 🔒 Production endpoint — secured with API key
        [HttpPost("detailsByPNR")]
        public async Task<IActionResult> GetReservationDetails(
            [FromHeader(Name = "x-api-key")] string apiKey,
            [FromBody] GetReservationRequest request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.Locator))
                return BadRequest("Locator (PNR) is required.");

            // ✅ Step 1: Validate API key
            var configuredKey = _config["ProdApiKey"]; // Key in appsettings or Azure Configuration
            if (string.IsNullOrEmpty(configuredKey) || apiKey != configuredKey)
                return Unauthorized("Invalid or missing API key.");

            // ✅ Step 2: Forward request to internal Sabre service (local, not external URL)
            var result = await _sabreService.GetReservationAsync(request);
            return Ok(result);
        }

        // 🟢 Certification endpoint — for testing only, no API key required
        [HttpPost("detailsByPNRCert")]
        public async Task<IActionResult> GetCertReservationDetails([FromBody] GetReservationRequest request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.Locator))
                return BadRequest("Locator (PNR) is required.");

            var result = await _sabreService.CertReservationAsync(request);
            return Ok(result);
        }
    }
}
