using CoreWebAPIs.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace CoreWebAPIs.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class MilesCalculatorController : ControllerBase
    {
        private readonly HttpClient _httpClient;

        public MilesCalculatorController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [HttpPost]
        [Authorize(Roles = "Api.MilesCalculato.Read")]
        [Route("api/GetAccrualMileage")]
        public async Task<ActionResult> GetAccrualMileage(MilesCalculatorRequest request)
        {
            if (request == null || string.IsNullOrEmpty(request.CabinClass) ||
            string.IsNullOrEmpty(request.OriginAirport) || string.IsNullOrEmpty(request.DestinationAirport))
            {
                return BadRequest("Invalid input.");
            }
            var filter = new
            {
                AccrualMileageCalculatorFilter = new
                {
                    companyCode = "GF",
                    programCode = "FF",
                    partnerCode = "GF",
                    flightAttributes = new[]
            {
                new
                {
                    carrierCode = "GF",
                    cabinClass = request.CabinClass,
                    originAirport = request.OriginAirport,
                    destinationAirport = request.DestinationAirport,
                    partnerAccrualDynamicAttributes = new object[]
                    {
                        new
                        {
                            attributeKey = "MLGCAL",
                            attributeValue = "Y"
                        },
                        new
                        {
                            attributeKey = "MCTIRCOD",
                            attributeName = "MCTIR",
                            attributeValue = request.AttributeValue,
                            fieldType = "TIRCOD",
                            key = "tier_code",
                            attributeMapping = "state.selectedTier.value"
                        }
                    }
                }
            }
                },
                txnHeader = new
                {
                    userName = "GFINTERNAL",
                    channelUserCode = "GFINTERNAL"
                }
            };
            var jsonContent = JsonSerializer.Serialize(filter);
            var httpRequest = new HttpRequestMessage(HttpMethod.Post, "https://gulfair.ibsplc.aero/iflyloyalty/api/common-services/v50/rest/AccrualMileageCalculatorService/accrualMileageCalculator")
            {
                Headers =
            {
                { "Accept", "application/json" },
                { "x-auth-channel", "GFINTERNAL@GF" },
                { "X-auth-token", "GFinternaL@111" }
            },
                Content = new StringContent(jsonContent, Encoding.UTF8, "application/json")
            };

            var result = await _httpClient.SendAsync(httpRequest);
            result.EnsureSuccessStatusCode();
            if (!result.IsSuccessStatusCode)
            {
                var errorContent = await result.Content.ReadAsStringAsync();
                return StatusCode((int)result.StatusCode, errorContent);
            }


            var resultContent = await result.Content.ReadAsStringAsync();
            var completeResponse = JsonSerializer.Deserialize<AccrualResponse>(resultContent);
            var filterResponse = completeResponse.AccrualMileageCalculatorResult.FlightAttributePointDetails.Select(a => a.PointCreditDetails).ToList();
            var response = filterResponse.SelectMany
                (a => a)
                .Select(x => new PointCreditDetail()
                {
                    CreditLimit = Math.Ceiling(x.CreditLimit),
                    Points = Math.Ceiling(x.Points),
                    PointType = x.PointType
                }).ToList();

            return Ok(response);
        }

        [HttpPost]
        [Authorize(Roles = "Api.MilesCalculato.Read")]
        [Route("api/GetUseAccrualMileage")]
        public async Task<ActionResult> GetUseAccrualMileage(UsedMilesCalculatorRequest request)
        {
            if (request == null || string.IsNullOrEmpty(request.OriginAirport) || string.IsNullOrEmpty(request.DestinationAirport) || string.IsNullOrEmpty(request.AttributeValue))
            {
                return BadRequest("Invalid input.");
            }
            var filter = new
            {
                companyCode = "GF",
                programCode = "FF",
                partnerCode = "GF",
                origin = request.OriginAirport,
                destination = request.DestinationAirport,
                rewardGroup = "F",
                pageNumber = 1,
                absoluteIndex = 1,
                cost = 0,
                rewardDiscount = 0,
                actualCostofRedemption = 0,
                excessWeight = 0,
                dynamicAttributes = new List<object>
            {
                new
                {
                    attributeCode = "BKGCL",
                    attributeValue = request.AttributeValue
                }
            }
            };

            // Serialize request to JSON
            var json = JsonSerializer.Serialize(filter);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Add headers
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpClient.DefaultRequestHeaders.Add("x-auth-channel", "GFINTERNAL@GF");
            _httpClient.DefaultRequestHeaders.Add("x-auth-token", "GFinternaL@111");

            // Make the HTTP POST request
            var response = await _httpClient.PostAsync("https://gulfair.ibsplc.aero/iflyloyalty/api/common-services/v50/rest/RetrieveRewardsWithPricingDetailService/retrieveRewardsWithPricingDetail", content);
            response.EnsureSuccessStatusCode();
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                return StatusCode((int)response.StatusCode, errorContent);
            }

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true // Allow case-insensitive matching
            };
            var resultContent = await response.Content.ReadAsStringAsync();
            var completeResponse = JsonSerializer.Deserialize<UseMilesResponse>(resultContent, options);
            var rewardPoints = 0;
            var rewardClass = request.AttributeValue == "E" ? "Economy" : request.AttributeValue == "B" ? "Bussines" : "Add Class";
            if (completeResponse != null)
            {
                rewardPoints = (int)completeResponse.Reward[0].RewardPricingDetail.RewardPoints;
                var data = new
                {
                    Class = rewardClass,
                    Rewards = rewardPoints,

                };
                return Ok(data);
            }
            return NotFound();
        }

        [HttpPost]
        [Authorize(Roles = "Api.MilesCalculato.Read")]
        [Route("api/GetRequiredMilesForUpgrade")]
        public async Task<ActionResult> GetRequiredMilesForUpgrade(RequiredMilesCalculatorRequest request)
        {
            if (request == null || string.IsNullOrEmpty(request.OriginAirport) || string.IsNullOrEmpty(request.DestinationAirport) || string.IsNullOrEmpty(request.BookingClass))
            {
                return BadRequest("Invalid input.");
            }
            var filter = new
            {
                companyCode = "GF",
                programCode = "FF",
                partnerCode = "GF",
                originalBookingClass = request.BookingClass,
                origin = request.OriginAirport,
                destination = request.DestinationAirport,
                rewardGroup = "U",
                pageNumber = 1,
                absoluteIndex = 1,
                cost = 0,
                rewardDiscount = 0,
                actualCostofRedemption = 0,
                excessWeight = 0
            };

            // Serialize request to JSON
            var json = JsonSerializer.Serialize(filter);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Add headers
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpClient.DefaultRequestHeaders.Add("x-auth-channel", "GFINTERNAL@GF");
            _httpClient.DefaultRequestHeaders.Add("x-auth-token", "GFinternaL@111");

            // Make the HTTP POST request
            var response = await _httpClient.PostAsync("https://gulfair.ibsplc.aero/iflyloyalty/api/common-services/v50/rest/RetrieveRewardsWithPricingDetailService/retrieveRewardsWithPricingDetail", content);
            response.EnsureSuccessStatusCode();
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                return StatusCode((int)response.StatusCode, errorContent);
            }
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true // Allow case-insensitive matching
            };
            var rewardPoints = 0;
            var rewardClass = "Falcon Gold";
            var resultContent = await response.Content.ReadAsStringAsync();
            var completeResponse = JsonSerializer.Deserialize<UpgradeMilesResponse>(resultContent, options);
            if (completeResponse != null)
            {
                rewardPoints = (int)completeResponse.Reward[0].RewardPricingDetail.RewardPoints;
                var data = new
                {
                    Class = rewardClass,
                    Rewards = rewardPoints,

                };
                return Ok(data);
            }
            return NotFound();
        }

        [HttpPost]
        [Authorize(Roles = "Api.MilesCalculato.Read")]
        [Route("api/GetMultiSegmentUseAccrualMileage")]
        public async Task<ActionResult> GetMultiSegmentUseMileage(UsedMilesCalculatorRequest request)
        {
            if (request == null || string.IsNullOrEmpty(request.OriginAirport) ||
                string.IsNullOrEmpty(request.DestinationAirport) ||
                string.IsNullOrEmpty(request.AttributeValue))
            {
                return BadRequest("Invalid input.");
            }

            try
            {
                async Task<dynamic> CalculateSegmentMiles(string origin, string destination, string attributeValue)
                {
                    var filter = new
                    {
                        companyCode = "GF",
                        programCode = "FF",
                        partnerCode = "GF",
                        origin = origin,
                        destination = destination,
                        rewardGroup = "F",
                        pageNumber = 1,
                        absoluteIndex = 1,
                        cost = 0,
                        rewardDiscount = 0,
                        actualCostofRedemption = 0,
                        excessWeight = 0,
                        dynamicAttributes = new List<object>
                {
                    new
                    {
                        attributeCode = "BKGCL",
                        attributeValue = attributeValue
                    }
                }
                    };

                    var json = JsonSerializer.Serialize(filter);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    _httpClient.DefaultRequestHeaders.Accept.Clear();
                    _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    _httpClient.DefaultRequestHeaders.Remove("x-auth-channel");
                    _httpClient.DefaultRequestHeaders.Remove("x-auth-token");
                    _httpClient.DefaultRequestHeaders.Add("x-auth-channel", "GFINTERNAL@GF");
                    _httpClient.DefaultRequestHeaders.Add("x-auth-token", "GFinternaL@111");

                    var response = await _httpClient.PostAsync(
                        "https://gulfair.ibsplc.aero/iflyloyalty/api/common-services/v50/rest/RetrieveRewardsWithPricingDetailService/retrieveRewardsWithPricingDetail",
                        content);

                    response.EnsureSuccessStatusCode();

                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };

                    var resultContent = await response.Content.ReadAsStringAsync();
                    var completeResponse = JsonSerializer.Deserialize<UseMilesResponse>(resultContent, options);

                    if (completeResponse?.Reward.Count > 0)
                    {
                        return new
                        {
                            Class = attributeValue == "E" ? "Economy" :
                                   attributeValue == "B" ? "Business" : "Add Class",
                            Rewards = (int)completeResponse.Reward[0].RewardPricingDetail.RewardPoints
                        };
                    }

                    throw new Exception("No reward information found for the segment");
                }

                // If it's a direct BAH flight, calculate single segment
                if (request.OriginAirport == "BAH" && request.DestinationAirport == "BAH")
                {
                    var singleSegmentResult = await CalculateSegmentMiles(request.OriginAirport, request.DestinationAirport, request.AttributeValue);
                    return Ok(singleSegmentResult);
                }

                // Calculate multi-segment miles
                var firstSegmentMiles = 0;
                var secondSegmentMiles = 0;

                // First segment: If origin is not BAH
                if (request.OriginAirport != "BAH")
                {
                    var firstSegmentResult = await CalculateSegmentMiles(request.OriginAirport, "BAH", request.AttributeValue);
                    firstSegmentMiles = firstSegmentResult.Rewards;
                }

                // Second segment: If destination is not BAH
                if (request.DestinationAirport != "BAH")
                {
                    var secondSegmentResult = await CalculateSegmentMiles("BAH", request.DestinationAirport, request.AttributeValue);
                    secondSegmentMiles = secondSegmentResult.Rewards;
                }

                // Calculate total miles and prepare response
                var totalMiles = firstSegmentMiles + secondSegmentMiles;
                var rewardClass = request.AttributeValue == "E" ? "Economy" :
                                 request.AttributeValue == "B" ? "Business" : "Add Class";

                var multiSegmentResult = new
                {
                    Class = rewardClass,
                    Rewards = totalMiles,
                    Segments = new[]
                    {
                new {
                    Origin = request.OriginAirport,
                    Destination = "BAH",
                    Miles = firstSegmentMiles
                },
                new {
                    Origin = "BAH",
                    Destination = request.DestinationAirport,
                    Miles = secondSegmentMiles
                }
            }
                };

                return Ok(multiSegmentResult);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPost]
        [Authorize(Roles = "Api.MilesCalculato.Read")]
        [Route("api/GetMultiSegmentAccrualMileage")]
        public async Task<ActionResult> GetMultiSegmentAccrualMileage(MilesCalculatorRequest request)
        {
            if (request == null || string.IsNullOrEmpty(request.CabinClass) ||
                string.IsNullOrEmpty(request.OriginAirport) || string.IsNullOrEmpty(request.DestinationAirport))
            {
                return BadRequest("Invalid input.");
            }

            try
            {
                async Task<List<PointCreditDetail>> GetSegmentAccrual(string origin, string destination)
                {
                    var filter = new
                    {
                        AccrualMileageCalculatorFilter = new
                        {
                            companyCode = "GF",
                            programCode = "FF",
                            partnerCode = "GF",
                            flightAttributes = new[]
                            {
                        new
                        {
                            carrierCode = "GF",
                            cabinClass = request.CabinClass,
                            originAirport = origin,
                            destinationAirport = destination,
                            partnerAccrualDynamicAttributes = new object[]
                            {
                                new
                                {
                                    attributeKey = "MLGCAL",
                                    attributeValue = "Y"
                                },
                                new
                                {
                                    attributeKey = "MCTIRCOD",
                                    //attributeName = "MCTIR", //commented becuse of update in the api
                                    attributeValue = request.AttributeValue,
                                    //fieldType = "TIRCOD", //commented becuse of update in the api
                                    //key = "tier_code", //commented becuse of update in the api
                                    //attributeMapping = "state.selectedTier.value" //commented becuse of update in the api
                                }
                            }
                        }
                    }
                        },
                        txnHeader = new
                        {
                            userName = "GFINTERNAL",
                            channelUserCode = "GFINTERNAL"
                        }
                    };

                    var jsonContent = JsonSerializer.Serialize(filter);
                    var httpRequest = new HttpRequestMessage(HttpMethod.Post,
                        "https://gulfair.ibsplc.aero/iflyloyalty/api/common-services/v50/rest/AccrualMileageCalculatorService/accrualMileageCalculator")
                    {
                        Headers =
                {
                    { "Accept", "application/json" },
                    { "x-auth-channel", "GFINTERNAL@GF" },
                    { "X-auth-token", "GFinternaL@111" }
                },
                        Content = new StringContent(jsonContent, Encoding.UTF8, "application/json")
                    };

                    var result = await _httpClient.SendAsync(httpRequest);
                    result.EnsureSuccessStatusCode();

                    var resultContent = await result.Content.ReadAsStringAsync();
                    var completeResponse = JsonSerializer.Deserialize<AccrualResponse>(resultContent);
                    var filterResponse = completeResponse.AccrualMileageCalculatorResult.FlightAttributePointDetails
                        .Select(a => a.PointCreditDetails).ToList();

                    return filterResponse.SelectMany(a => a)
                        .Select(x => new PointCreditDetail()
                        {
                            CreditLimit = Math.Ceiling(x.CreditLimit),
                            Points = Math.Ceiling(x.Points),
                            PointType = x.PointType
                        }).ToList();
                }

                // Direct flight case
                if (request.OriginAirport == "BAH" && request.DestinationAirport == "BAH")
                {
                    var directPoints = await GetSegmentAccrual(request.OriginAirport, request.DestinationAirport);
                    return Ok(new
                    {
                        TotalPoints = directPoints,
                        Segments = new[]
                        {
                    new { Origin = request.OriginAirport, Destination = request.DestinationAirport, Points = directPoints }
                }
                    });
                }

                // Multi-segment case
                var segments = new List<(string Origin, string Destination, List<PointCreditDetail> Points)>();

                // First segment if origin is not BAH
                if (request.OriginAirport != "BAH")
                {
                    var firstSegmentPoints = await GetSegmentAccrual(request.OriginAirport, "BAH");
                    segments.Add((request.OriginAirport, "BAH", firstSegmentPoints));
                }

                // Second segment if destination is not BAH
                if (request.DestinationAirport != "BAH")
                {
                    var secondSegmentPoints = await GetSegmentAccrual("BAH", request.DestinationAirport);
                    segments.Add(("BAH", request.DestinationAirport, secondSegmentPoints));
                }

                // Calculate total points by combining segments
                var totalPoints = new List<PointCreditDetail>();
                var allPointTypes = segments.SelectMany(s => s.Points).Select(p => p.PointType).Distinct();

                foreach (var pointType in allPointTypes)
                {
                    var totalPointsForType = segments.SelectMany(s => s.Points)
                        .Where(p => p.PointType == pointType)
                        .Sum(p => p.Points);

                    var totalCreditForType = segments.SelectMany(s => s.Points)
                        .Where(p => p.PointType == pointType)
                        .Sum(p => p.CreditLimit);

                    totalPoints.Add(new PointCreditDetail
                    {
                        PointType = pointType,
                        Points = Math.Ceiling(totalPointsForType),
                        CreditLimit = Math.Ceiling(totalCreditForType)
                    });
                }

                var response = new
                {
                    TotalPoints = totalPoints,
                    Segments = segments.Select(s => new
                    {
                        Origin = s.Origin,
                        Destination = s.Destination,
                        Points = s.Points
                    }).ToArray()
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPost]
        [Authorize(Roles = "Api.MilesCalculato.Read")]
        [Route("api/GetMultiSegmentRequiredMilesForUpgrade")]
        public async Task<ActionResult> GetMultiSegmentRequiredMilesForUpgrade(RequiredMilesCalculatorRequest request)
        {
            if (request == null || string.IsNullOrEmpty(request.OriginAirport) ||
                string.IsNullOrEmpty(request.DestinationAirport) || string.IsNullOrEmpty(request.BookingClass))
            {
                return BadRequest("Invalid input.");
            }

            try
            {
                async Task<(int Points, string Class)> CalculateSegmentUpgrade(string origin, string destination)
                {
                    var filter = new
                    {
                        companyCode = "GF",
                        programCode = "FF",
                        partnerCode = "GF",
                        originalBookingClass = request.BookingClass,
                        origin = origin,
                        destination = destination,
                        rewardGroup = "U",
                        pageNumber = 1,
                        absoluteIndex = 1,
                        cost = 0,
                        rewardDiscount = 0,
                        actualCostofRedemption = 0,
                        excessWeight = 0
                    };

                    var json = JsonSerializer.Serialize(filter);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    _httpClient.DefaultRequestHeaders.Accept.Clear();
                    _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    _httpClient.DefaultRequestHeaders.Remove("x-auth-channel");
                    _httpClient.DefaultRequestHeaders.Remove("x-auth-token");
                    _httpClient.DefaultRequestHeaders.Add("x-auth-channel", "GFINTERNAL@GF");
                    _httpClient.DefaultRequestHeaders.Add("x-auth-token", "GFinternaL@111");

                    var response = await _httpClient.PostAsync(
                        "https://gulfair.ibsplc.aero/iflyloyalty/api/common-services/v50/rest/RetrieveRewardsWithPricingDetailService/retrieveRewardsWithPricingDetail",
                        content);

                    response.EnsureSuccessStatusCode();

                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };

                    var resultContent = await response.Content.ReadAsStringAsync();
                    var completeResponse = JsonSerializer.Deserialize<UpgradeMilesResponse>(resultContent, options);

                    if (completeResponse?.Reward == null || completeResponse.Reward.Count == 0)
                    {
                        throw new Exception($"No upgrade information found for segment {origin} to {destination}");
                    }

                    return ((int)completeResponse.Reward[0].RewardPricingDetail.RewardPoints, "Falcon Gold");
                }

                // Direct flight case
                if (request.OriginAirport == "BAH" && request.DestinationAirport == "BAH")
                {
                    var (points, upgradeClass) = await CalculateSegmentUpgrade(
                        request.OriginAirport,
                        request.DestinationAirport);

                    return Ok(new
                    {
                        Class = upgradeClass,
                        Rewards = points
                    });
                }

                // Multi-segment case
                var segments = new List<(string Origin, string Destination, int Points)>();
                var totalPoints = 0;

                // First segment if origin is not BAH
                if (request.OriginAirport != "BAH")
                {
                    var (points, _) = await CalculateSegmentUpgrade(request.OriginAirport, "BAH");
                    segments.Add((request.OriginAirport, "BAH", points));
                    totalPoints += points;
                }

                // Second segment if destination is not BAH
                if (request.DestinationAirport != "BAH")
                {
                    var (points, _) = await CalculateSegmentUpgrade("BAH", request.DestinationAirport);
                    segments.Add(("BAH", request.DestinationAirport, points));
                    totalPoints += points;
                }

                var response = new
                {
                    Class = "Falcon Gold",
                    Rewards = totalPoints,
                    Segments = segments.Select(s => new
                    {
                        Origin = s.Origin,
                        Destination = s.Destination,
                        RequiredMiles = s.Points
                    }).ToArray()
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}
