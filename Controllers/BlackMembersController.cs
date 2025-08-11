using Azure.Core;
using CoreWebAPIs.Interfaces;
using CoreWebAPIs.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

[ApiController]
public class BlackMembersController : ControllerBase
{
    private readonly HttpClient _httpClient;
    private readonly IPhoneParserService _parserService;
    public BlackMembersController(HttpClient httpClient, IPhoneParserService parserService)
    {
        _httpClient = httpClient;
        _parserService = parserService;
    }

    [HttpPost]
    //[Authorize(Roles = "Api.MilesCalculator.Read")]
    [Route("api/RetrieveCustomerDetails")]
    public async Task<ActionResult<CustomerDetail>> RetrieveCustomerDetails(string mobileNumber)
    {
        try
        {
            if (string.IsNullOrEmpty(mobileNumber))
            {
                return BadRequest("Invalid input.");
            }
            var formattedNumber = _parserService.ParsePhoneNumber(mobileNumber);
            if (!formattedNumber.IsValid)
            {
                return BadRequest(formattedNumber);
            }


            var filter = new
            {
                companyCode = "GF",
                customerType = "I",
                mobileNumber = formattedNumber.NationalNumber,
                pageNumber = "1",
                absoluteIndex = "1",
                pageSize = 10,
                filter = new
                {
                    companyCode = "GF",
                    programCode = "FF",
                    partnerCode = "GF",
                    flightAttributes = new[]
                    {
                    new
                    {
                        carrierCode = "GF",
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
            var httpRequest = new HttpRequestMessage(HttpMethod.Post, "https://gulfair.ibsplc.aero/iflyloyalty/api/member-retrieval/v50/rest/RetrieveCustomerDetailsService/retrieveCustomerDetails")
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
            if (!result.IsSuccessStatusCode)
            {
                var errorContent = await result.Content.ReadAsStringAsync();
                return StatusCode((int)result.StatusCode, errorContent);
            }

            var resultContent = await result.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var responseDocument = JsonDocument.Parse(resultContent);
            var customerDetailsElement = responseDocument.RootElement.GetProperty("customerDetails")[0];
            var basicCustomerDetail = JsonSerializer.Deserialize<BasicCustomerDetail>(customerDetailsElement.GetRawText(), options);

            return Ok(basicCustomerDetail);
        }
        catch (HttpRequestException ex)
        {
            return StatusCode(StatusCodes.Status503ServiceUnavailable, $"Error connecting to service: {ex.Message}");
        }
        catch (JsonException ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Error processing response data: {ex.Message}");
        }
        catch (KeyNotFoundException ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Required data not found in response: {ex.Message}");
        }
        catch (IndexOutOfRangeException ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"No customer details found: {ex.Message}");
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"An unexpected error occurred: {ex.Message}");
        }
    }

    [HttpPost]
    //[Authorize(Roles = "Api.MilesCalculator.Read")]
    [Route("api/RetrieveMemberDetailsForAllPrograms")]
    public async Task<ActionResult<RetrieveMemberDetailsForAllProgramsResponse>> RetrieveMemberDetailsForAllPrograms(string customerNumber)
    {
        try
        {
            if (string.IsNullOrEmpty(customerNumber))
            {
                return BadRequest("Invalid input.");
            }
            var payload = new
            {
                companyCode = "GF",
                customerNumber = customerNumber,
                txnHeader = new
                {
                    userName = "GFINTERNAL",
                    channelUserCode = "GFINTERNAL"
                }
            };
            var jsonContent = JsonSerializer.Serialize(payload);

            var httpRequest = new HttpRequestMessage(HttpMethod.Post, "https://gulfair.ibsplc.aero/iflyloyalty/api/member-retrieval/v60/rest/RetrieveMemberDetailsForAllProgramsService/retrieveMemberDetailsForAllPrograms")
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
            if (!result.IsSuccessStatusCode)
            {
                var errorContent = await result.Content.ReadAsStringAsync();
                return StatusCode((int)result.StatusCode, errorContent);
            }
            var resultContent = await result.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var response = JsonSerializer.Deserialize<RetrieveMemberDetailsForAllProgramsResponse>(resultContent, options);
            if (response == null)
                return StatusCode(500, "Failed to parse response.");
            return Ok(response);
        }
        catch (HttpRequestException ex)
        {
            return StatusCode(StatusCodes.Status503ServiceUnavailable, $"Error connecting to service: {ex.Message}");
        }
        catch (JsonException ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Error processing response data: {ex.Message}");
        }
        catch (UriFormatException ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Invalid API URL: {ex.Message}");
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"An unexpected error occurred: {ex.Message}");
        }
    }

    [HttpPost]
    //[Authorize(Roles = "Api.MilesCalculator.Read")]
    [Route("api/RetrieveMemberDetailsForAllProgramsByMembershipNumber")]
    public async Task<ActionResult<MemberDetailsByMembershipNumberResponse>> RetrieveMemberDetailsForAllProgramsByMembershipNumber(string membershipNumber)
    {
        try
        {
            if (string.IsNullOrEmpty(membershipNumber))
            {
                return BadRequest("Invalid input.");
            }

            var payload = new
            {
                companyCode = "GF",
                membershipNumber = membershipNumber
            };

            var jsonContent = JsonSerializer.Serialize(payload);

            var stringContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var httpRequest = new HttpRequestMessage(HttpMethod.Post, "https://gulfair.ibsplc.aero/iflyloyalty/api/member-retrieval/v60/rest/RetrieveMemberDetailsForAllProgramsService/retrieveMemberDetailsForAllPrograms")
            {
                Headers =
            {
                { "Accept", "application/json" },
                { "x-auth-channel", "GFINTERNAL@GF" },
                { "X-auth-token", "GFinternaL@111" }
            },
                Content = stringContent
            };

            var result = await _httpClient.SendAsync(httpRequest);
            if (!result.IsSuccessStatusCode)
            {
                var errorContent = await result.Content.ReadAsStringAsync();
                return StatusCode((int)result.StatusCode, errorContent);
            }

            var resultContent = await result.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var response = JsonSerializer.Deserialize<MemberDetailsByMembershipNumberResponse>(resultContent, options);
            if (response == null)
                return StatusCode(500, "Failed to parse response.");

            return Ok(response);
        }
        catch (HttpRequestException ex)
        {
            return StatusCode(StatusCodes.Status503ServiceUnavailable, $"Error connecting to service: {ex.Message}");
        }
        catch (JsonException ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Error processing response data: {ex.Message}");
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"An unexpected error occurred: {ex.Message}");
        }
    }

    [HttpPost]
    //[Authorize(Roles = "Api.MilesCalculator.Read")]
    [Route("api/GetBlackMemberDetails")]
    public async Task<ActionResult<BlackMemberDetailsResponse>> GetBlackMemberDetails(string membershipNumber)
    {
        try
        {
            if (string.IsNullOrEmpty(membershipNumber))
            {
                return BadRequest("Invalid input.");
            }

            var payload = new
            {
                companyCode = "GF",
                membershipNumber = membershipNumber
            };

            var jsonContent = JsonSerializer.Serialize(payload);
            var stringContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var httpRequest = new HttpRequestMessage(HttpMethod.Post, "https://gulfair.ibsplc.aero/iflyloyalty/api/member-retrieval/v60/rest/RetrieveMemberDetailsForAllProgramsService/retrieveMemberDetailsForAllPrograms")
            {
                Headers =
            {
                { "Accept", "application/json" },
                { "x-auth-channel", "GFINTERNAL@GF" },
                { "X-auth-token", "GFinternaL@111" }
            },
                Content = stringContent
            };

            var result = await _httpClient.SendAsync(httpRequest);
            if (!result.IsSuccessStatusCode)
            {
                var errorContent = await result.Content.ReadAsStringAsync();
                return StatusCode((int)result.StatusCode, errorContent);
            }

            var resultContent = await result.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var response = JsonSerializer.Deserialize<MemberDetailsByMembershipNumberResponse>(resultContent, options);
            if (response == null)
                return StatusCode(500, "Failed to parse response.");

            var blackProgram = response.ProgramDetails?.FirstOrDefault(program =>
                program.TierCode == "440" &&
                program.TierName.Equals("Black", StringComparison.OrdinalIgnoreCase));

            if (blackProgram == null)
            {
                return NotFound("Member is not a Black tier member.");
            }

            // Create a BasicCustomerDetail from the response data
            var basicCustomerDetail = new BasicCustomerDetail
            {
                MembershipNumber = membershipNumber,
                CustomerNumber = response.ProfileDetails?.CustomerNumber,
                Title = response.ProfileDetails?.IndividualInfo?.Title,
                FirstName = response.ProfileDetails?.IndividualInfo?.GivenName,
                SurName = response.ProfileDetails?.IndividualInfo?.FamilyName,
                MiddleName = response.ProfileDetails?.IndividualInfo?.SecondName,
                SecondName = response.ProfileDetails?.IndividualInfo?.SecondName,
                SecondLastName = response.ProfileDetails?.IndividualInfo?.SecondLastName,
                MarriedName = response.ProfileDetails?.IndividualInfo?.MarriedName,
                EmailAddress = response.ProfileDetails?.IndividualInfo?.MemberContactInfos?.FirstOrDefault(c => c.EmailAddressStatus == "V")?.EmailAddress,
                DateOfBirth = response.ProfileDetails?.IndividualInfo?.DateOfBirth,
                MobileNumber = response.ProfileDetails?.IndividualInfo?.MemberContactInfos?.FirstOrDefault(c => c.MobileNumberStatus == "V")?.MobileNumber,
                AddressLine1 = response.ProfileDetails?.IndividualInfo?.MemberContactInfos?.FirstOrDefault(c => c.PostalAddressStatus == "V")?.AddressLine1,
                PostalCode = response.ProfileDetails?.IndividualInfo?.MemberContactInfos?.FirstOrDefault(c => c.PostalAddressStatus == "V")?.ZipCode,
                PhoneNumber = response.ProfileDetails?.IndividualInfo?.MemberContactInfos?.FirstOrDefault(c => c.PhoneNumberStatus == "V")?.PhoneISDCode + "-" +
                             response.ProfileDetails?.IndividualInfo?.MemberContactInfos?.FirstOrDefault(c => c.PhoneNumberStatus == "V")?.PhoneNumber,
                CountryOfResidence = response.ProfileDetails?.IndividualInfo?.CountryOfResidence,
                Nationality = response.ProfileDetails?.IndividualInfo?.Nationality,
                CustomerStatus = response.ProfileDetails?.Status,
                CustomerType = response.ProfileDetails?.CustomerType
            };

            var filteredProgramDetails = new FilteredProgramDetail
            {
                ProgramCode = blackProgram.ProgramCode,
                ProgramName = blackProgram.ProgramName,
                EnrollmentDate = blackProgram.EnrollmentDate,
                ExpiryDate = blackProgram.ExpiryDate,
                AccountStatus = blackProgram.AccountStatus,
                Suspended = blackProgram.Suspended,
                TierCode = blackProgram.TierCode,
                TierName = blackProgram.TierName,
                TierFromDate = blackProgram.TierFromDate,
                TierToDate = blackProgram.TierToDate,
                PreviousTierCode = blackProgram.PreviousTierCode,
                LastTierChangeReasonCode = blackProgram.LastTierChangeReasonCode,
                PointDetails = blackProgram.PointDetails
            };

            var blackMemberDetails = new BlackMemberDetailsResponse
            {
                IndividualInfo = response.ProfileDetails?.IndividualInfo,
                ProgramDetails = filteredProgramDetails,
                CustomerDetails = basicCustomerDetail
            };

            return Ok(blackMemberDetails);
        }
        catch (HttpRequestException ex)
        {
            return StatusCode(StatusCodes.Status503ServiceUnavailable, $"Error connecting to service: {ex.Message}");
        }
        catch (JsonException ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Error processing response data: {ex.Message}");
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"An unexpected error occurred: {ex.Message}");
        }
    }

    [HttpPost]
    //[Authorize(Roles = "Api.MilesCalculator.Read")]
    [Route("api/GetBlackMemberByMobile")]
    public async Task<ActionResult<BlackMemberDetailsResponse>> GetBlackMemberByMobile(string mobileNumber)
    {
        try
        {
            if (string.IsNullOrEmpty(mobileNumber))
            {
                return BadRequest("Invalid input: Mobile number is required.");
            }

            var formattedNumber = _parserService.ParsePhoneNumber(mobileNumber);
            if (!formattedNumber.IsValid)
            {
                return BadRequest(formattedNumber);
            }
            var filter = new
            {
                companyCode = "GF",
                customerType = "I",
                mobileNumber = formattedNumber.NationalNumber,
                pageNumber = "1",
                absoluteIndex = "1",
                pageSize = 10,
                filter = new
                {
                    companyCode = "GF",
                    programCode = "FF",
                    partnerCode = "GF",
                    flightAttributes = new[]
                    {
                            new
                            {
                                carrierCode = "GF",
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
            var httpRequest = new HttpRequestMessage(HttpMethod.Post, "https://gulfair.ibsplc.aero/iflyloyalty/api/member-retrieval/v50/rest/RetrieveCustomerDetailsService/retrieveCustomerDetails")
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
            if (!result.IsSuccessStatusCode)
            {
                var errorContent = await result.Content.ReadAsStringAsync();
                return StatusCode((int)result.StatusCode, $"Error retrieving customer details: {errorContent}");
            }

            var resultContent = await result.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var responseDocument = JsonDocument.Parse(resultContent);

            if (!responseDocument.RootElement.TryGetProperty("customerDetails", out var customerDetailsArray) ||
                customerDetailsArray.GetArrayLength() == 0)
            {
                return NotFound("No customer found with the provided mobile number.");
            }

            var customerDetailsElement = customerDetailsArray[0];
            var basicCustomerDetail = JsonSerializer.Deserialize<BasicCustomerDetail>(customerDetailsElement.GetRawText(), options);

            // Set the MembershipNumber property to be the same as CustomerNumber
            basicCustomerDetail.MembershipNumber = basicCustomerDetail.CustomerNumber;

            string membershipNumber = basicCustomerDetail.CustomerNumber;
            if (string.IsNullOrEmpty(membershipNumber))
            {
                return NotFound("Customer found but no membership number is associated.");
            }

            var memberPayload = new
            {
                companyCode = "GF",
                membershipNumber = membershipNumber
            };

            jsonContent = JsonSerializer.Serialize(memberPayload);
            var stringContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var memberRequest = new HttpRequestMessage(HttpMethod.Post, "https://gulfair.ibsplc.aero/iflyloyalty/api/member-retrieval/v60/rest/RetrieveMemberDetailsForAllProgramsService/retrieveMemberDetailsForAllPrograms")
            {
                Headers =
                    {
                        { "Accept", "application/json" },
                        { "x-auth-channel", "GFINTERNAL@GF" },
                        { "X-auth-token", "GFinternaL@111" }
                    },
                Content = stringContent
            };

            var memberResult = await _httpClient.SendAsync(memberRequest);
            if (!memberResult.IsSuccessStatusCode)
            {
                var errorContent = await memberResult.Content.ReadAsStringAsync();
                return StatusCode((int)memberResult.StatusCode, $"Error retrieving member details: {errorContent}");
            }

            var memberContent = await memberResult.Content.ReadAsStringAsync();
            var memberResponse = JsonSerializer.Deserialize<MemberDetailsByMembershipNumberResponse>(memberContent, options);

            if (memberResponse == null)
                return StatusCode(500, "Failed to parse member details response.");

            var blackProgram = memberResponse.ProgramDetails?.FirstOrDefault(program =>
                program.TierCode == "440" &&
                program.TierName.Equals("Black", StringComparison.OrdinalIgnoreCase));

            if (blackProgram == null)
            {
                return NotFound("Member is not a Black tier member.");
            }

            var filteredProgramDetails = new FilteredProgramDetail
            {
                ProgramCode = blackProgram.ProgramCode,
                ProgramName = blackProgram.ProgramName,
                EnrollmentDate = blackProgram.EnrollmentDate,
                ExpiryDate = blackProgram.ExpiryDate,
                AccountStatus = blackProgram.AccountStatus,
                Suspended = blackProgram.Suspended,
                TierCode = blackProgram.TierCode,
                TierName = blackProgram.TierName,
                TierFromDate = blackProgram.TierFromDate,
                TierToDate = blackProgram.TierToDate,
                PreviousTierCode = blackProgram.PreviousTierCode,
                LastTierChangeReasonCode = blackProgram.LastTierChangeReasonCode,
                PointDetails = blackProgram.PointDetails
            };

            var blackMemberDetails = new BlackMemberDetailsResponse
            {
                IndividualInfo = memberResponse.ProfileDetails?.IndividualInfo,
                ProgramDetails = filteredProgramDetails,
                CustomerDetails = basicCustomerDetail
            };

            return Ok(blackMemberDetails);
        }
        catch (HttpRequestException ex)
        {
            return StatusCode(StatusCodes.Status503ServiceUnavailable, $"Error connecting to service: {ex.Message}");
        }
        catch (JsonException ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Error processing response data: {ex.Message}");
        }
        catch (KeyNotFoundException ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Required data not found in response: {ex.Message}");
        }
        catch (IndexOutOfRangeException ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"No customer details found: {ex.Message}");
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"An unexpected error occurred: {ex.Message}");
        }
    }

    [HttpPost]
    [Route("api/GetMemberAllProgramDetailsByPhoneNumber")]
    public async Task<ActionResult<MemberDetailsByMembershipNumberResponse>> GetFullMemberDetailsByPhone([FromBody] MemberDetailsRequest request)
    {
        // ====================================================================
        //  STEP 1: Validate input and get Membership Number from Phone Number
        //  (This is the logic from your first function)
        // ====================================================================

        if (request == null || string.IsNullOrEmpty(request.MobileNumber))
        {
            return BadRequest("A JSON body with a 'MobileNumber' property is required.");
        }

        string membershipNumber = null;
        try
        {
            var formattedNumber = _parserService.ParsePhoneNumber(request.MobileNumber);
            if (!formattedNumber.IsValid)
            {
                return BadRequest("The provided phone number is invalid.");
            }

            var filter = new
            {
                companyCode = "GF",
                customerType = "I",
                mobileNumber = formattedNumber.NationalNumber,
                pageNumber = "1",
                absoluteIndex = "1",
                pageSize = 10,
                filter = new
                {
                    companyCode = "GF",
                    programCode = "FF",
                    partnerCode = "GF",
                    flightAttributes = new[]
                    {
                    new
                    {
                        carrierCode = "GF",
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
            var firstJsonContent = JsonSerializer.Serialize(filter);

            var firstHttpRequest = new HttpRequestMessage(HttpMethod.Post, "https://gulfair.ibsplc.aero/iflyloyalty/api/member-retrieval/v50/rest/RetrieveCustomerDetailsService/retrieveCustomerDetails")
            {
                Headers =
                {
                    { "Accept", "application/json" },
                    { "x-auth-channel", "GFINTERNAL@GF" },
                    { "X-auth-token", "GFinternaL@111" }
                },
                Content = new StringContent(firstJsonContent, System.Text.Encoding.UTF8, "application/json")
            };

            var firstResult = await _httpClient.SendAsync(firstHttpRequest);

            if (!firstResult.IsSuccessStatusCode)
            {
                var errorContent = await firstResult.Content.ReadAsStringAsync();
                return StatusCode((int)firstResult.StatusCode, $"Error from customer details service: {errorContent}");
            }

            // --- Extract the Membership Number from the response ---
            var firstResultContent = await firstResult.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var responseDocument = JsonDocument.Parse(firstResultContent);
            var customerDetailsElement = responseDocument.RootElement.GetProperty("customerDetails")[0];
            var basicCustomerDetail = JsonSerializer.Deserialize<BasicCustomerDetail>(customerDetailsElement.GetRawText(), options);

            
            if (basicCustomerDetail?.MembershipNumber == "" || basicCustomerDetail?.MembershipNumber == null)
            {
                if (basicCustomerDetail.CustomerNumber !=null && basicCustomerDetail.CustomerNumber!="")
                {
                    membershipNumber = basicCustomerDetail.CustomerNumber;
                }
                
            }else
            {
                membershipNumber = basicCustomerDetail.MembershipNumber;
            }
        }
        catch (Exception ex)
        {
            // Log the exception (using your preferred logging framework)
            return StatusCode(StatusCodes.Status500InternalServerError, $"An unexpected error occurred while retrieving the membership number: {ex.Message}");
        }


        // ====================================================================
        //  STEP 2: Get Full Member Details using the retrieved Membership Number
        //  (This is the logic from your second function)
        // ====================================================================

        try
        {
            if (string.IsNullOrEmpty(membershipNumber))
            {
                return NotFound("Could not find a membership number associated with the given phone number.");
            }

            var detailsPayload = new
            {
                companyCode = "GF",
                membershipNumber = membershipNumber
            };

            var detailsJsonContent = JsonSerializer.Serialize(detailsPayload);
            var detailsStringContent = new StringContent(detailsJsonContent, System.Text.Encoding.UTF8, "application/json");

            var secondHttpRequest = new HttpRequestMessage(HttpMethod.Post, "https://gulfair.ibsplc.aero/iflyloyalty/api/member-retrieval/v60/rest/RetrieveMemberDetailsForAllProgramsService/retrieveMemberDetailsForAllPrograms")
            {
                Headers =
                {
                    { "Accept", "application/json" },
                    { "x-auth-channel", "GFINTERNAL@GF" },
                    { "X-auth-token", "GFinternaL@111" }
                },
                Content = detailsStringContent
            };

            var secondResult = await _httpClient.SendAsync(secondHttpRequest);

            if (!secondResult.IsSuccessStatusCode)
            {
                var errorContent = await secondResult.Content.ReadAsStringAsync();
                return StatusCode((int)secondResult.StatusCode, $"Error from member details service: {errorContent}");
            }

            var finalContent = await secondResult.Content.ReadAsStringAsync();
            var finalResponse = JsonSerializer.Deserialize<MemberDetailsByMembershipNumberResponse>(finalContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return Ok(finalResponse); // Success! Return the final, complete details.
        }
        catch (Exception ex)
        {
            // Log the exception
            return StatusCode(StatusCodes.Status500InternalServerError, $"An unexpected error occurred while retrieving full member details: {ex.Message}");
        }
    }

    [HttpPost]
    [Route("api/GetMemberAllProgramDetailsByPhone")]
    public async Task<ActionResult<MemberDetailsByMembershipNumberResponse>> GetFullMemberDetailsForAllProgramsByPhone(string mobilenumber)
    {
        // ====================================================================
        //  STEP 1: Validate input and get Membership Number from Phone Number
        //  (This is the logic from your first function)
        // ====================================================================

        if (mobilenumber == null || string.IsNullOrEmpty(mobilenumber))
        {
            return BadRequest("A JSON body with a 'MobileNumber' property is required.");
        }

        string membershipNumber = null;
        try
        {
            var formattedNumber = _parserService.ParsePhoneNumber(mobilenumber);
            if (!formattedNumber.IsValid)
            {
                return BadRequest("The provided phone number is invalid.");
            }

            var filter = new
            {
                companyCode = "GF",
                customerType = "I",
                mobileNumber = formattedNumber.NationalNumber,
                pageNumber = "1",
                absoluteIndex = "1",
                pageSize = 10,
                filter = new
                {
                    companyCode = "GF",
                    programCode = "FF",
                    partnerCode = "GF",
                    flightAttributes = new[]
                    {
                    new
                    {
                        carrierCode = "GF",
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
            var firstJsonContent = JsonSerializer.Serialize(filter);

            var firstHttpRequest = new HttpRequestMessage(HttpMethod.Post, "https://gulfair.ibsplc.aero/iflyloyalty/api/member-retrieval/v50/rest/RetrieveCustomerDetailsService/retrieveCustomerDetails")
            {
                Headers =
                {
                    { "Accept", "application/json" },
                    { "x-auth-channel", "GFINTERNAL@GF" },
                    { "X-auth-token", "GFinternaL@111" }
                },
                Content = new StringContent(firstJsonContent, System.Text.Encoding.UTF8, "application/json")
            };

            var firstResult = await _httpClient.SendAsync(firstHttpRequest);

            if (!firstResult.IsSuccessStatusCode)
            {
                var errorContent = await firstResult.Content.ReadAsStringAsync();
                return StatusCode((int)firstResult.StatusCode, $"Error from customer details service: {errorContent}");
            }

            // --- Extract the Membership Number from the response ---
            var firstResultContent = await firstResult.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var responseDocument = JsonDocument.Parse(firstResultContent);
            var customerDetailsElement = responseDocument.RootElement.GetProperty("customerDetails")[0];
            var basicCustomerDetail = JsonSerializer.Deserialize<BasicCustomerDetail>(customerDetailsElement.GetRawText(), options);


            if (basicCustomerDetail?.MembershipNumber == "" || basicCustomerDetail?.MembershipNumber == null)
            {
                if (basicCustomerDetail.CustomerNumber != null && basicCustomerDetail.CustomerNumber != "")
                {
                    membershipNumber = basicCustomerDetail.CustomerNumber;
                }

            }
            else
            {
                membershipNumber = basicCustomerDetail.MembershipNumber;
            }
        }
        catch (Exception ex)
        {
            // Log the exception (using your preferred logging framework)
            return StatusCode(StatusCodes.Status500InternalServerError, $"An unexpected error occurred while retrieving the membership number: {ex.Message}");
        }


        // ====================================================================
        //  STEP 2: Get Full Member Details using the retrieved Membership Number
        //  (This is the logic from your second function)
        // ====================================================================

        try
        {
            if (string.IsNullOrEmpty(membershipNumber))
            {
                return NotFound("Could not find a membership number associated with the given phone number.");
            }

            var detailsPayload = new
            {
                companyCode = "GF",
                membershipNumber = membershipNumber
            };

            var detailsJsonContent = JsonSerializer.Serialize(detailsPayload);
            var detailsStringContent = new StringContent(detailsJsonContent, System.Text.Encoding.UTF8, "application/json");

            var secondHttpRequest = new HttpRequestMessage(HttpMethod.Post, "https://gulfair.ibsplc.aero/iflyloyalty/api/member-retrieval/v60/rest/RetrieveMemberDetailsForAllProgramsService/retrieveMemberDetailsForAllPrograms")
            {
                Headers =
                {
                    { "Accept", "application/json" },
                    { "x-auth-channel", "GFINTERNAL@GF" },
                    { "X-auth-token", "GFinternaL@111" }
                },
                Content = detailsStringContent
            };

            var secondResult = await _httpClient.SendAsync(secondHttpRequest);

            if (!secondResult.IsSuccessStatusCode)
            {
                var errorContent = await secondResult.Content.ReadAsStringAsync();
                return StatusCode((int)secondResult.StatusCode, $"Error from member details service: {errorContent}");
            }

            var finalContent = await secondResult.Content.ReadAsStringAsync();
            var finalResponse = JsonSerializer.Deserialize<MemberDetailsByMembershipNumberResponse>(finalContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return Ok(finalResponse); // Success! Return the final, complete details.
        }
        catch (Exception ex)
        {
            // Log the exception
            return StatusCode(StatusCodes.Status500InternalServerError, $"An unexpected error occurred while retrieving full member details: {ex.Message}");
        }
    }

}