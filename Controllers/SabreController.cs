using CoreWebAPIs.Interfaces;
using CoreWebAPIs.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoreWebAPIs.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SabreController : ControllerBase
    {
        private readonly ISabreService _sabreService;

        public SabreController(ISabreService sabreService)
        {
            _sabreService = sabreService;
        }

        [Authorize(Roles = "Api.Sabre.Read")]
        [HttpPost("searchByFirstLastName")]
        [Produces("application/json")] 
        public async Task<IActionResult> SearchTrips([FromBody] TripSearchRequest request)
        {
            try
            {
                var result = await _sabreService.SearchTripsAsync(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = $"An error occurred: {ex.Message}" });
            }
        }

        [Authorize(Roles = "Api.Sabre.Read")]
        [HttpPost("detailsByPNR")]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> GetReservationDetails([FromBody] GetReservationRequest request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.Locator))
            {
                return BadRequest("Request body is missing or the locator is empty.");
            }
            try
            {
                var result = await _sabreService.GetReservationAsync(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = $"An error occurred: {ex.Message}" });
            }
        }

        [Authorize(Roles = "Api.Sabre.Read")]
        [HttpPost("detailsByPNRCert")]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> GetCertReservationDetails([FromBody] GetReservationRequest request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.Locator))
            {
                return BadRequest("Request body is missing or the locator is empty.");
            }
            try
            {
                var result = await _sabreService.CertReservationAsync(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = $"An error occurred: {ex.Message}" });
            }
        }

        [Authorize(Roles = "Api.Sabre.Read")]
        [HttpPost("searchByFFP")]
        [Produces("application/json")]
        public async Task<IActionResult> SearchByFfp([FromBody] FfpSearchRequest request)
        {
            try
            {
                var result = await _sabreService.SearchByFfpAsync(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = $"An error occurred: {ex.Message}" });
            }
        }

        //[HttpPost("searchByPhone")]
        //[Produces("application/json")]
        //public async Task<IActionResult> SearchByPhone([FromBody] PhoneSearchRequest request)
        //{
        //    try
        //    {
        //        var result = await _sabreService.SearchByPhoneAsync(request);
        //        return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, new { error = $"An error occurred: {ex.Message}" });
        //    }
        //}
    }
}
