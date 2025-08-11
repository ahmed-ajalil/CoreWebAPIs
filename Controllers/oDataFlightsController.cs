using CoreWebAPIs.Context;
using CoreWebAPIs.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;

namespace FlightsWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class oDataFlightsController : ControllerBase
    {
        private readonly EBriefingDbContext _context;
        public oDataFlightsController(EBriefingDbContext context)
        {
            _context = context;
        }


        [EnableQuery]
        [HttpGet(Name = "GetFlightsStatus")]
        public ActionResult<IQueryable<OtpFlightInfo>> GetFlightsStatus(ODataQueryOptions<OtpFlightInfo> queryOptions)
        {
            try
            {
                var flightsQuery = _context.OtpFlightInfos;

                // Apply OData query options to the query
                var filteredFlights = (IQueryable<OtpFlightInfo>)queryOptions.ApplyTo(flightsQuery);

                if (!filteredFlights.Any())
                {
                    return NotFound(new { Status = "No Data" });
                }

                return Ok(filteredFlights);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ErrorMessage = ex.Message });
            }
        }

    }
}
