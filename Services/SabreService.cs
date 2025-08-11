using CoreWebAPIs.Interfaces;
using CoreWebAPIs.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace CoreWebAPIs.Services
{
    public class SabreService : ISabreService
    {
        private readonly HttpClient _httpClient;
        private readonly HttpClient _certhttpClient;
        private readonly SabreConfig _sabreConfig;
        private string? _cachedSessionToken;
        private DateTime? _tokenCreationTime;

        public SabreService(HttpClient httpClient, IOptions<SabreConfig> sabreConfig, HttpClient certhttpClient)
        {
            _httpClient = httpClient;
            _sabreConfig = sabreConfig.Value;
            _httpClient.BaseAddress = new Uri(_sabreConfig.ApiUrl);
            _certhttpClient = certhttpClient;
            _certhttpClient.BaseAddress = new Uri(_sabreConfig.CertApiUrl);
        }

        private async Task<string> GetSessionTokenAsync()
        {
            if (!string.IsNullOrEmpty(_cachedSessionToken) &&
                _tokenCreationTime.HasValue &&
                (DateTime.UtcNow - _tokenCreationTime.Value).TotalMinutes < 14)
            {
                return _cachedSessionToken;
            }

            var requestXml = $"""
    <soap-env:Envelope xmlns:soap-env="http://schemas.xmlsoap.org/soap/envelope/" xmlns:eb="http://www.ebxml.org/namespaces/messageHeader" xmlns:xlink="http://www.w3.org/1999/xlink" xmlns:xsd="http://www.w3.org/1999/XMLSchema">
        <soap-env:Header>
            <eb:MessageHeader soap-env:mustUnderstand="1" eb:version="2.0.0">
                <eb:From><eb:PartyId /></eb:From>
                <eb:To><eb:PartyId /></eb:To>
                <eb:CPAId>{_sabreConfig.PCC}</eb:CPAId>
                <eb:ConversationId>SabreApiWrapper-Session</eb:ConversationId>
                <eb:Service>SessionCreateRQ</eb:Service>
                <eb:Action>SessionCreateRQ</eb:Action>
                <eb:MessageData>
                    <eb:MessageId>mid:20001209-133003-2333@client.com</eb:MessageId>
                    <eb:Timestamp>{DateTime.UtcNow:O}</eb:Timestamp>
                </eb:MessageData>
            </eb:MessageHeader>
            <wsse:Security xmlns:wsse="http://schemas.xmlsoap.org/ws/2002/12/secext" xmlns:wsu="http://schemas.xmlsoap.org/ws/2002/12/utility">
                <wsse:UsernameToken>
                    <wsse:Username>{_sabreConfig.Username}</wsse:Username>
                    <wsse:Password>{_sabreConfig.Password}</wsse:Password>
                    <Organization>{_sabreConfig.PCC}</Organization>
                    <Domain>{_sabreConfig.PCC}</Domain>
                </wsse:UsernameToken>
            </wsse:Security>
        </soap-env:Header>
        <soap-env:Body>
            <sws:SessionCreateRQ xmlns:sws="http://webservices.sabre.com" Version="1.0.0">
                <POS>
                    <Source PseudoCityCode="{_sabreConfig.PCC}" />
                </POS>
            </sws:SessionCreateRQ>
        </soap-env:Body>
    </soap-env:Envelope>
    """;

            var content = new StringContent(requestXml, Encoding.UTF8, "text/xml");
            var response = await _httpClient.PostAsync("", content);
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            // Parse the XML to find the token
            var responseDoc = XDocument.Parse(responseString);
            XNamespace wsse = "http://schemas.xmlsoap.org/ws/2002/12/secext";
            var token = responseDoc.Descendants(wsse + "BinarySecurityToken").FirstOrDefault()?.Value;

            if (string.IsNullOrEmpty(token))
            {
                throw new ApplicationException("Failed to retrieve session token from Sabre.");
            }

            _cachedSessionToken = token;
            _tokenCreationTime = DateTime.UtcNow;

            return _cachedSessionToken;
        }

        private async Task CloseSessionAsync(string tokenToClose)
        {
            if (string.IsNullOrEmpty(tokenToClose)) return;

            var conversationId = $"SabreApiWrapper-SessionClose-{Guid.NewGuid()}";

            var requestXml = $"""
            <soapenv:Envelope xmlns:soapenv="http://schemas.xmlsoap.org/soap/envelope/">
                <soapenv:Header>
                    <eb:MessageHeader eb:version="1.0" xmlns:eb="http://www.ebxml.org/namespaces/messageHeader">
                        <eb:From><eb:PartyId>Client</eb:PartyId></eb:From>
                        <eb:To><eb:PartyId>Server</eb:PartyId></eb:To>
                        <eb:CPAId>{_sabreConfig.PCC}</eb:CPAId>
                        <eb:ConversationId>{conversationId}</eb:ConversationId>
                        <eb:Service>SessionCloseRQ</eb:Service>
                        <eb:Action>SessionCloseRQ</eb:Action>
                        <eb:MessageData>
                            <eb:MessageId>mid:1</eb:MessageId>
                            <eb:Timestamp>{DateTime.UtcNow:O}</eb:Timestamp>
                        </eb:MessageData>
                    </eb:MessageHeader>
                    <wsse:Security xmlns:wsse="http://schemas.xmlsoap.org/ws/2002/12/secext">
                        <wsse:BinarySecurityToken>{tokenToClose}</wsse:BinarySecurityToken>
                    </wsse:Security>
                </soapenv:Header>
                <soapenv:Body>
                    <SessionCloseRQ Version="1.0.0" xmlns="http://www.opentravel.org/OTA/2002/11"/>
                </soapenv:Body>
            </soapenv:Envelope>
            """;

            var content = new StringContent(requestXml, Encoding.UTF8, "text/xml");
            var response = await _httpClient.PostAsync("", content);
            if (!response.IsSuccessStatusCode)
            {
                System.Diagnostics.Debug.WriteLine($"Failed to close Sabre session. Status: {response.StatusCode}");
            }
        }

        public async Task<TripSearchResponse> SearchTripsAsync(TripSearchRequest request)
        {
            string? token = null;
            try
            {
                token = await GetSessionTokenAsync();
                var conversationId = $"SabreApiWrapper-{Guid.NewGuid()}";
                var requestXml = $"""
    <soapenv:Envelope xmlns:soapenv="http://schemas.xmlsoap.org/soap/envelope/">
        <soapenv:Header>
            <eb:MessageHeader eb:version="1.0" xmlns:eb="http://www.ebxml.org/namespaces/messageHeader">
                <eb:From><eb:PartyId>Client</eb:PartyId></eb:From>
                <eb:To><eb:PartyId>Server</eb:PartyId></eb:To>
                <eb:CPAId>{_sabreConfig.TripSearchCPAId}</eb:CPAId>
    <eb:ConversationId>{conversationId}</eb:ConversationId>
                <eb:Service>Trip_SearchRQ</eb:Service>
                <eb:Action>Trip_SearchRQ</eb:Action>
                <eb:MessageData>
                    <eb:MessageId>mid:1</eb:MessageId>
                    <eb:Timestamp>{DateTime.UtcNow:O}</eb:Timestamp>
                </eb:MessageData>
            </eb:MessageHeader>
            <wsse:Security xmlns:wsse="http://schemas.xmlsoap.org/ws/2002/12/secext">
                <wsse:BinarySecurityToken>{token}</wsse:BinarySecurityToken>
            </wsse:Security>
        </soapenv:Header>
        <soapenv:Body>
            <Trip_SearchRQ Version="4.5.0" xmlns="http://webservices.sabre.com/triprecord">
                <ReadRequests>
                    <ReservationReadRequest>
                        <NameCriteria>
                            <Name>
                                <FirstName MatchMode="EXACT_NO_COMPRESS">{request.FirstName}</FirstName>
                                <LastName MatchMode="EXACT_NO_COMPRESS">{request.LastName}</LastName>
                            </Name>
                        </NameCriteria>

                        <PosCriteria AirlineCode="GF"/>

                        <ReturnOptions ViewName="TripSearchBlob" SearchType="ACTIVE" MaxItemsReturned="500">
                            <SubjectAreas>
                                <SubjectArea>FULL</SubjectArea>
                            </SubjectAreas>
                        </ReturnOptions>
                    </ReservationReadRequest>
                </ReadRequests>
            </Trip_SearchRQ>
        </soapenv:Body>
    </soapenv:Envelope>
    """;

                var content = new StringContent(requestXml, Encoding.UTF8, "text/xml");
                var response = await _httpClient.PostAsync("", content);
                var responseString = await response.Content.ReadAsStringAsync();

                if (responseString.Contains("<soap-env:Fault>") || responseString.Contains("<Errors>"))
                {
                    System.Diagnostics.Debug.WriteLine("Sabre returned an error: " + responseString);

                    throw new HttpRequestException($"Sabre returned an error in the response body. Check the logs. Status Code: {response.StatusCode}");
                }

                response.EnsureSuccessStatusCode();

                return ParseTripSearchXmlResponse(responseString, request);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (token != null)
                {
                    await CloseSessionAsync(token);
                }
            }



        }

        private TripSearchResponse ParseTripSearchXmlResponse(string xmlResponse, object originalRequest)
        {
            var responseDoc = XDocument.Parse(xmlResponse);

            XNamespace tr = "http://webservices.sabre.com/triprecord";
            XNamespace ns4 = "http://webservices.sabre.com/sabreXML/2003/07";

            var reservationListElement = responseDoc.Descendants(tr + "ReservationsList").FirstOrDefault();
            if (reservationListElement == null)
            {
                return new TripSearchResponse { SearchQuery = originalRequest, Results = new List<ReservationDetailResponse>() };
            }

            // Navigate the path: ReservationsList -> Reservations -> Reservation
            var detailedResults = reservationListElement.Element(tr + "Reservations")?
                .Elements(tr + "Reservation")
                .Select(res =>
                {
                    // Each <Reservation> contains a full <TravelItinerary>
                    var travelItinerary = res.Descendants(ns4 + "TravelItinerary").FirstOrDefault();
                    if (travelItinerary == null) return null; // Skip if the PNR data is malformed

                    // Create a new detailed response object for this reservation
                    var detail = new ReservationDetailResponse
                    {
                        BookingDetails = new BookingInfo
                        {
                            Locator = res.Attribute("Locator")?.Value,
                            UpdateTimestamp = DateTime.TryParse(
                                travelItinerary.Descendants(ns4 + "UpdatedBy").FirstOrDefault()?.Attribute("UpdateDateTime")?.Value,
                                out var ut) ? ut : null
                        },
                        Passengers = travelItinerary.Descendants(ns4 + "PersonName").Select(p => new Passenger
                        {
                            LastName = p.Element(ns4 + "Surname")?.Value,
                            FirstName = p.Element(ns4 + "GivenName")?.Value
                        }).ToList(),
                        Itinerary = travelItinerary.Descendants(ns4 + "Air").Select(s => new ItinerarySegment
                        {
                            MarketingAirline = s.Element(ns4 + "MarketingAirline")?.Attribute("Code")?.Value,
                            MarketingFlightNumber = s.Element(ns4 + "MarketingAirline")?.Attribute("FlightNumber")?.Value,
                            ClassOfService = s.Attribute("ResBookDesigCode")?.Value,
                            Status = s.Attribute("ActionCode")?.Value,
                            Departure = new LocationInfo
                            {
                                AirportCode = s.Element(ns4 + "DepartureAirport")?.Attribute("LocationCode")?.Value,
                                DateTime = DateTime.TryParse(s.Attribute("DepartureDateTime")?.Value, out var departureDateTime) ? departureDateTime : null
                            },
                            Arrival = new LocationInfo
                            {
                                AirportCode = s.Element(ns4 + "ArrivalAirport")?.Attribute("LocationCode")?.Value,
                                DateTime = DateTime.TryParse(s.Attribute("ArrivalDateTime")?.Value, out var arrivalDateTime) ? arrivalDateTime : null
                            }
                        }).ToList(),
                        Contacts = new List<Contact>()
                            .Concat(travelItinerary.Descendants(ns4 + "Telephone").Select(p => new Contact { Type = "Phone", Value = p.Attribute("PhoneNumber")?.Value }))
                            .Concat(travelItinerary.Descendants(ns4 + "Email").Select(e => new Contact { Type = "Email", Value = e.Attribute("Address")?.Value }))
                            .ToList(),
                        TicketNumbers = travelItinerary.Descendants(ns4 + "Ticketing")
                            .Select(t => t.Attribute("eTicketNumber")?.Value)
                            .Where(s => !string.IsNullOrEmpty(s))
                            .ToList()
                    };
                    return detail;
                })
                .Where(d => d != null) 
                .ToList();

            return new TripSearchResponse
            {
                SearchQuery = originalRequest,
                Results = detailedResults ?? new List<ReservationDetailResponse>()
            };
        }

        public async Task<ReservationDetailResponse> GetReservationAsync(GetReservationRequest request)
        {
            string? token = null;
            try
            {
                token = await GetSessionTokenAsync();
                var locator = request.Locator;

                var requestXml = $"""
    <soapenv:Envelope xmlns:soapenv="http://schemas.xmlsoap.org/soap/envelope/">
        <soapenv:Header>
            <eb:MessageHeader eb:version="1.0" xmlns:eb="http://www.ebxml.org/namespaces/messageHeader">
                <eb:From><eb:PartyId>Client</eb:PartyId></eb:From>
                <eb:To><eb:PartyId>Server</eb:PartyId></eb:To>
                <eb:CPAId>{_sabreConfig.PCC}</eb:CPAId>
                <eb:ConversationId>SabreApiWrapper-GetReservation</eb:ConversationId>
                <eb:Service>GetReservationRQ</eb:Service>
                <eb:Action>GetReservationRQ</eb:Action>
                <eb:MessageData>
                    <eb:MessageId>mid:1</eb:MessageId>
                    <eb:Timestamp>{DateTime.UtcNow:O}</eb:Timestamp>
                </eb:MessageData>
            </eb:MessageHeader>
            <wsse:Security xmlns:wsse="http://schemas.xmlsoap.org/ws/2002/12/secext">
                <wsse:BinarySecurityToken>{token}</wsse:BinarySecurityToken>
            </wsse:Security>
        </soapenv:Header>
        <soapenv:Body>
            <GetReservationRQ Version="1.19.8" xmlns="http://webservices.sabre.com/pnrbuilder/v1_19">
                <Locator>{locator}</Locator>
                <RequestType>Trip</RequestType>
                <!-- This is the key: Ask for the FULL subject area to get all data -->
                <ReturnOptions>
                    <SubjectAreas>
                        <SubjectArea>FULL</SubjectArea>
                    </SubjectAreas>
                    <ViewName>Default</ViewName>
                </ReturnOptions>
            </GetReservationRQ>
        </soapenv:Body>
    </soapenv:Envelope>
    """;

                var content = new StringContent(requestXml, Encoding.UTF8, "text/xml");
                var response = await _httpClient.PostAsync("", content);
                response.EnsureSuccessStatusCode();
                var responseString = await response.Content.ReadAsStringAsync();

                return ParseFullReservationXmlResponse(responseString);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (token != null)
                {
                    await CloseSessionAsync(token);
                }
            }


        }

        private ReservationDetailResponse ParseFullReservationXmlResponse(string xmlResponse)
        {
            var responseDoc = XDocument.Parse(xmlResponse);
            XNamespace stl19 = "http://webservices.sabre.com/pnrbuilder/v1_19";
            XNamespace or114 = "http://services.sabre.com/res/or/v1_14";

            var reservationElement = responseDoc.Descendants(stl19 + "Reservation").FirstOrDefault();
            if (reservationElement == null)
            {
                return new ReservationDetailResponse(); 
            }

            var response = new ReservationDetailResponse();

            // 1. Booking Details
            var bookingDetailsElement = reservationElement.Element(stl19 + "BookingDetails");
            if (bookingDetailsElement != null)
            {
                response.BookingDetails = new BookingInfo
                {
                    Locator = bookingDetailsElement.Element(stl19 + "RecordLocator")?.Value,
                    CreationTimestamp = DateTime.Parse(bookingDetailsElement.Element(stl19 + "CreationTimestamp")?.Value ?? "0001-01-01"),
                    UpdateTimestamp = DateTime.Parse(bookingDetailsElement.Element(stl19 + "UpdateTimestamp")?.Value ?? "0001-01-01"),
                    CreationAgentId = bookingDetailsElement.Element(stl19 + "CreationAgentID")?.Value,
                    PnrSequence = bookingDetailsElement.Element(stl19 + "PNRSequence")?.Value,
                };
            }

            // 2. Point of Sale
            var posElement = reservationElement.Descendants(stl19 + "Source").FirstOrDefault();
            if (posElement != null)
            {
                response.PointOfSale = new PointOfSale
                {
                    PseudoCityCode = posElement.Attribute("PseudoCityCode")?.Value,
                    AgentSine = posElement.Attribute("AgentSine")?.Value,
                    IsoCountry = posElement.Attribute("ISOCountry")?.Value,
                    AirlineVendorId = posElement.Attribute("AirlineVendorID")?.Value,
                };
            }

            // 3. Passengers and their Special Requests
            response.Passengers = reservationElement.Descendants(stl19 + "Passenger").Select(p => new Passenger
            {
                LastName = p.Element(stl19 + "LastName")?.Value,
                FirstName = p.Element(stl19 + "FirstName")?.Value,
                SpecialRequests = p.Descendants(stl19 + "GenericSpecialRequest").Select(ssr => new SpecialRequest
                {
                    Code = ssr.Element(stl19 + "Code")?.Value,
                    FreeText = ssr.Element(stl19 + "FreeText")?.Value,
                    TicketNumber = ssr.Element(stl19 + "TicketNumber")?.Value
                }).ToList()
            }).ToList();

            // 4. Itinerary
            response.Itinerary = reservationElement.Descendants(stl19 + "Air").Select(s => new ItinerarySegment
            {
                Sequence = int.Parse(s.Parent?.Attribute("sequence")?.Value ?? "0"),
                MarketingAirline = s.Element(stl19 + "MarketingAirlineCode")?.Value,
                MarketingFlightNumber = s.Element(stl19 + "MarketingFlightNumber")?.Value,
                OperatingAirline = s.Element(stl19 + "OperatingAirlineCode")?.Value,
                OperatingFlightNumber = s.Element(stl19 + "OperatingFlightNumber")?.Value,
                ClassOfService = s.Element(stl19 + "ClassOfService")?.Value,
                EquipmentType = s.Element(stl19 + "EquipmentType")?.Value,
                Status = s.Element(stl19 + "ActionCode")?.Value,
                Departure = new LocationInfo
                {
                    AirportCode = s.Element(stl19 + "DepartureAirport")?.Value,
                    DateTime = DateTime.Parse(s.Element(stl19 + "DepartureDateTime")?.Value ?? "0001-01-01"),
                    Terminal = s.Element(stl19 + "DepartureTerminalCode")?.Value
                },
                Arrival = new LocationInfo
                {
                    AirportCode = s.Element(stl19 + "ArrivalAirport")?.Value,
                    DateTime = DateTime.Parse(s.Element(stl19 + "ArrivalDateTime")?.Value ?? "0001-01-01"),
                    Terminal = s.Element(stl19 + "ArrivalTerminalCode")?.Value
                },
                Cabin = new CabinInfo
                {
                    Code = s.Descendants(stl19 + "Cabin").FirstOrDefault()?.Attribute("Code")?.Value,
                    Name = s.Descendants(stl19 + "Cabin").FirstOrDefault()?.Attribute("Name")?.Value
                }
            }).ToList();

            // 5. Contact Info (Phone and Email)
            response.Contacts.AddRange(
                reservationElement.Descendants(stl19 + "PhoneNumber").Select(p => new Contact
                {
                    Type = "Phone",
                    Value = p.Element(stl19 + "Number")?.Value
                })
            );
            response.Contacts.AddRange(
                reservationElement.Descendants(stl19 + "EmailAddress").Select(e => new Contact
                {
                    Type = "Email",
                    Value = e.Element(stl19 + "Address")?.Value
                })
            );

            // 6. Payment Details (Form of Payment)
            var fopElement = reservationElement.Descendants(or114 + "PaymentCard").FirstOrDefault();
            if (fopElement != null)
            {
                response.PaymentDetails = new FormOfPayment
                {
                    Type = fopElement.Element(or114 + "PaymentType")?.Value,
                    CardCode = fopElement.Element(or114 + "CardCode")?.Value,
                    MaskedCardNumber = fopElement.Element(or114 + "CardNumber")?.Value,
                    ExpiryDate = $"{fopElement.Element(or114 + "ExpiryMonth")?.Value.Replace("--", "")}/{fopElement.Element(or114 + "ExpiryYear")?.Value}"
                };
            }

            // 7. Remarks
            response.Remarks = reservationElement.Descendants(stl19 + "Remark").Select(r => new Remark
            {
                Type = r.Attribute("type")?.Value,
                Text = r.Descendants(stl19 + "Text").FirstOrDefault()?.Value
            }).ToList();

            // 8. Ticket Numbers
            response.TicketNumbers = reservationElement.Descendants(stl19 + "ETicketNumber")
                .Select(t => t.Value.Split(' ')[1]) // Extracts just the number part
                .ToList();

            return response;
        }

        public async Task<TripSearchResponse> SearchByFfpAsync(FfpSearchRequest request)
        {
            string? token = null;
            try
            {
                token = await GetSessionTokenAsync();
                var conversationId = $"SabreApiWrapper-{Guid.NewGuid()}";

                var requestXml = $"""
    <soapenv:Envelope xmlns:soapenv="http://schemas.xmlsoap.org/soap/envelope/">
        <soapenv:Header>
            <eb:MessageHeader eb:version="1.0" xmlns:eb="http://www.ebxml.org/namespaces/messageHeader">
                <eb:From><eb:PartyId>Client</eb:PartyId></eb:From>
                <eb:To><eb:PartyId>Server</eb:PartyId></eb:To>
                <eb:CPAId>IPCC</eb:CPAId>
                <eb:ConversationId>{conversationId}</eb:ConversationId>
                <eb:Service>Trip_SearchRQ</eb:Service>
                <eb:Action>Trip_SearchRQ</eb:Action>
                <eb:MessageData>
                    <eb:MessageId>mid:1</eb:MessageId>
                    <eb:Timestamp>{DateTime.UtcNow:O}</eb:Timestamp>
                </eb:MessageData>
            </eb:MessageHeader>
            <wsse:Security xmlns:wsse="http://schemas.xmlsoap.org/ws/2002/12/secext">
                <wsse:BinarySecurityToken>{token}</wsse:BinarySecurityToken>
            </wsse:Security>
        </soapenv:Header>
        <soapenv:Body>
            <Trip_SearchRQ Version="4.5.0" xmlns="http://webservices.sabre.com/triprecord">
                <ReadRequests>
                    <ReservationReadRequest>
                        <LoyaltyCriteria>
                            <Loyalty ProgramID="GF" MembershipID="{request.MembershipId}"/>
                        </LoyaltyCriteria>
                        <PosCriteria AirlineCode="GF"/>
                        <ReturnOptions ViewName="TripSearchBlob" SearchType="ACTIVE" MaxItemsReturned="50">
                            <SubjectAreas>
                                <SubjectArea>FULL</SubjectArea>
                            </SubjectAreas>
                        </ReturnOptions>
                    </ReservationReadRequest>
                </ReadRequests>
            </Trip_SearchRQ>
        </soapenv:Body>
    </soapenv:Envelope>
    """;

                var content = new StringContent(requestXml, Encoding.UTF8, "text/xml");
                var response = await _httpClient.PostAsync("", content);
                var responseString = await response.Content.ReadAsStringAsync();

                if (responseString.Contains("<soap-env:Fault>") || responseString.Contains("<Errors>"))
                {
                    System.Diagnostics.Debug.WriteLine("Sabre returned an error: " + responseString);
                    throw new HttpRequestException($"Sabre returned an error in the response body. Check the logs. Status Code: {response.StatusCode}");
                }

                response.EnsureSuccessStatusCode();

                return ParseTripSearchXmlResponse(responseString, null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (token != null)
                {
                    await CloseSessionAsync(token);
                }
            }

        }








        //**************************************************************************************************
        // Methods for Cert enviornment - testing purposes
        //**************************************************************************************************





        public async Task<ReservationDetailResponse> CertReservationAsync(GetReservationRequest request)
        {
            string? token = null;
            try
            {
                token = await GetCertSessionTokenAsync();
                var locator = request.Locator;

                var requestXml = $"""
    <soapenv:Envelope xmlns:soapenv="http://schemas.xmlsoap.org/soap/envelope/">
        <soapenv:Header>
            <eb:MessageHeader eb:version="1.0" xmlns:eb="http://www.ebxml.org/namespaces/messageHeader">
                <eb:From><eb:PartyId>Client</eb:PartyId></eb:From>
                <eb:To><eb:PartyId>Server</eb:PartyId></eb:To>
                <eb:CPAId>{_sabreConfig.CertPCC}</eb:CPAId>
                <eb:ConversationId>SabreApiWrapper-GetReservation</eb:ConversationId>
                <eb:Service>GetReservationRQ</eb:Service>
                <eb:Action>GetReservationRQ</eb:Action>
                <eb:MessageData>
                    <eb:MessageId>mid:1</eb:MessageId>
                    <eb:Timestamp>{DateTime.UtcNow:O}</eb:Timestamp>
                </eb:MessageData>
            </eb:MessageHeader>
            <wsse:Security xmlns:wsse="http://schemas.xmlsoap.org/ws/2002/12/secext">
                <wsse:BinarySecurityToken>{token}</wsse:BinarySecurityToken>
            </wsse:Security>
        </soapenv:Header>
        <soapenv:Body>
            <GetReservationRQ Version="1.19.8" xmlns="http://webservices.sabre.com/pnrbuilder/v1_19">
                <Locator>{locator}</Locator>
                <RequestType>Trip</RequestType>
                <!-- This is the key: Ask for the FULL subject area to get all data -->
                <ReturnOptions>
                    <SubjectAreas>
                        <SubjectArea>FULL</SubjectArea>
                    </SubjectAreas>
                    <ViewName>Default</ViewName>
                </ReturnOptions>
            </GetReservationRQ>
        </soapenv:Body>
    </soapenv:Envelope>
    """;

                var content = new StringContent(requestXml, Encoding.UTF8, "text/xml");
                var response = await _certhttpClient.PostAsync("", content);
                response.EnsureSuccessStatusCode();
                var responseString = await response.Content.ReadAsStringAsync();

                return ParseFullReservationXmlResponse(responseString);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (token != null)
                {
                    await CloseCertSessionAsync(token);
                }
            }


        }

        private async Task<string> GetCertSessionTokenAsync()
        {
            if (!string.IsNullOrEmpty(_cachedSessionToken) &&
                _tokenCreationTime.HasValue &&
                (DateTime.UtcNow - _tokenCreationTime.Value).TotalMinutes < 14)
            {
                return _cachedSessionToken;
            }

            var requestXml = $"""
    <soap-env:Envelope xmlns:soap-env="http://schemas.xmlsoap.org/soap/envelope/" xmlns:eb="http://www.ebxml.org/namespaces/messageHeader" xmlns:xlink="http://www.w3.org/1999/xlink" xmlns:xsd="http://www.w3.org/1999/XMLSchema">
        <soap-env:Header>
            <eb:MessageHeader soap-env:mustUnderstand="1" eb:version="2.0.0">
                <eb:From><eb:PartyId /></eb:From>
                <eb:To><eb:PartyId /></eb:To>
                <eb:CPAId>{_sabreConfig.CertPCC}</eb:CPAId>
                <eb:ConversationId>SabreApiWrapper-Session</eb:ConversationId>
                <eb:Service>SessionCreateRQ</eb:Service>
                <eb:Action>SessionCreateRQ</eb:Action>
                <eb:MessageData>
                    <eb:MessageId>mid:20001209-133003-2333@client.com</eb:MessageId>
                    <eb:Timestamp>{DateTime.UtcNow:O}</eb:Timestamp>
                </eb:MessageData>
            </eb:MessageHeader>
            <wsse:Security xmlns:wsse="http://schemas.xmlsoap.org/ws/2002/12/secext" xmlns:wsu="http://schemas.xmlsoap.org/ws/2002/12/utility">
                <wsse:UsernameToken>
                    <wsse:Username>{_sabreConfig.CertUsername}</wsse:Username>
                    <wsse:Password>{_sabreConfig.CertPassword}</wsse:Password>
                    <Organization>{_sabreConfig.CertPCC}</Organization>
                    <Domain>{_sabreConfig.CertPCC}</Domain>
                </wsse:UsernameToken>
            </wsse:Security>
        </soap-env:Header>
        <soap-env:Body>
            <sws:SessionCreateRQ xmlns:sws="http://webservices.sabre.com" Version="1.0.0">
                <POS>
                    <Source PseudoCityCode="{_sabreConfig.CertPCC}" />
                </POS>
            </sws:SessionCreateRQ>
        </soap-env:Body>
    </soap-env:Envelope>
    """;

            var content = new StringContent(requestXml, Encoding.UTF8, "text/xml");
            var response = await _certhttpClient.PostAsync("", content);
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            // Parse the XML to find the token
            var responseDoc = XDocument.Parse(responseString);
            XNamespace wsse = "http://schemas.xmlsoap.org/ws/2002/12/secext";
            var token = responseDoc.Descendants(wsse + "BinarySecurityToken").FirstOrDefault()?.Value;

            if (string.IsNullOrEmpty(token))
            {
                throw new ApplicationException("Failed to retrieve session token from Sabre.");
            }

            _cachedSessionToken = token;
            _tokenCreationTime = DateTime.UtcNow;

            return _cachedSessionToken;
        }

        private async Task CloseCertSessionAsync(string tokenToClose)
        {
            if (string.IsNullOrEmpty(tokenToClose)) return;

            var conversationId = $"SabreApiWrapper-SessionClose-{Guid.NewGuid()}";

            var requestXml = $"""
            <soapenv:Envelope xmlns:soapenv="http://schemas.xmlsoap.org/soap/envelope/">
                <soapenv:Header>
                    <eb:MessageHeader eb:version="1.0" xmlns:eb="http://www.ebxml.org/namespaces/messageHeader">
                        <eb:From><eb:PartyId>Client</eb:PartyId></eb:From>
                        <eb:To><eb:PartyId>Server</eb:PartyId></eb:To>
                        <eb:CPAId>{_sabreConfig.PCC}</eb:CPAId>
                        <eb:ConversationId>{conversationId}</eb:ConversationId>
                        <eb:Service>SessionCloseRQ</eb:Service>
                        <eb:Action>SessionCloseRQ</eb:Action>
                        <eb:MessageData>
                            <eb:MessageId>mid:1</eb:MessageId>
                            <eb:Timestamp>{DateTime.UtcNow:O}</eb:Timestamp>
                        </eb:MessageData>
                    </eb:MessageHeader>
                    <wsse:Security xmlns:wsse="http://schemas.xmlsoap.org/ws/2002/12/secext">
                        <wsse:BinarySecurityToken>{tokenToClose}</wsse:BinarySecurityToken>
                    </wsse:Security>
                </soapenv:Header>
                <soapenv:Body>
                    <SessionCloseRQ Version="1.0.0" xmlns="http://www.opentravel.org/OTA/2002/11"/>
                </soapenv:Body>
            </soapenv:Envelope>
            """;

            var content = new StringContent(requestXml, Encoding.UTF8, "text/xml");
            var response = await _certhttpClient.PostAsync("", content);
            if (!response.IsSuccessStatusCode)
            {
                System.Diagnostics.Debug.WriteLine($"Failed to close Sabre session. Status: {response.StatusCode}");
            }
        }

        //    public async Task<TripSearchResponse> SearchByPhoneAsync(PhoneSearchRequest request)
        //    {
        //        var token = await GetSessionTokenAsync();
        //        var conversationId = $"SabreApiWrapper-{Guid.NewGuid()}";

        //        // --- Logic Block 1: Smart Phone Number Formatting ---
        //        // This correctly implements the logic from the Sabre documentation.
        //        string digitsOnlyPhoneNumber = Regex.Replace(request.PhoneNumber, "[^0-9]", "");

        //        // This validation correctly handles cases where the input has no digits.
        //        if (string.IsNullOrWhiteSpace(digitsOnlyPhoneNumber))
        //        {
        //            throw new ArgumentException("The provided phone number did not contain any digits.", nameof(request.PhoneNumber));
        //        }

        //        // --- Logic Block 2: The Complete and Correct XML Request ---
        //        var requestXml = $"""
        //<soapenv:Envelope xmlns:soapenv="http://schemas.xmlsoap.org/soap/envelope/">
        //    <!-- FIX: The full, correct header has been restored. -->
        //    <soapenv:Header>
        //        <eb:MessageHeader eb:version="1.0" xmlns:eb="http://www.ebxml.org/namespaces/messageHeader">
        //            <eb:From><eb:PartyId>Client</eb:PartyId></eb:From>
        //            <eb:To><eb:PartyId>Server</eb:PartyId></eb:To>
        //            <eb:CPAId>IPCC</eb:CPAId>
        //            <eb:ConversationId>{conversationId}</eb:ConversationId>
        //            <eb:Service>Trip_SearchRQ</eb:Service>
        //            <eb:Action>Trip_SearchRQ</eb:Action>
        //            <eb:MessageData>
        //                <eb:MessageId>mid:1</eb:MessageId>
        //                <eb:Timestamp>{DateTime.UtcNow:O}</eb:Timestamp>
        //            </eb:MessageData>
        //        </eb:MessageHeader>
        //        <wsse:Security xmlns:wsse="http://schemas.xmlsoap.org/ws/2002/12/secext">
        //            <wsse:BinarySecurityToken>{token}</wsse:BinarySecurityToken>
        //        </wsse:Security>
        //    </soapenv:Header>
        //    <soapenv:Body>
        //        <Trip_SearchRQ Version="4.5.0" xmlns="http://webservices.sabre.com/triprecord">
        //            <ReadRequests>
        //                <ReservationReadRequest>
        //                    <TelephoneCriteria>
        //                        <!-- This correctly uses the sanitized number and the best MatchMode -->
        //                        <PhoneNumber MatchMode="START">{digitsOnlyPhoneNumber}</PhoneNumber>
        //                    </TelephoneCriteria>

        //                    <PosCriteria AirlineCode="GF"/>
        //                    <ReturnOptions ViewName="TripSearchBlob" SearchType="ACTIVE" MaxItemsReturned="50">
        //                        <SubjectAreas>
        //                            <SubjectArea>ITINERARY</SubjectArea>
        //                        </SubjectAreas>
        //                    </ReturnOptions>
        //                </ReservationReadRequest>
        //            </ReadRequests>
        //        </Trip_SearchRQ>
        //    </soapenv:Body>
        //</soapenv:Envelope>
        //""";

        //        // The rest of your method is correct.
        //        var content = new StringContent(requestXml, Encoding.UTF8, "text/xml");
        //        var response = await _httpClient.PostAsync("", content);
        //        var responseString = await response.Content.ReadAsStringAsync();

        //        if (responseString.Contains("<soap-env:Fault>") || responseString.Contains("<Errors>"))
        //        {
        //            System.Diagnostics.Debug.WriteLine("Sabre returned an error: " + responseString);
        //            throw new HttpRequestException($"Sabre returned an error in the response body. Check the logs. Status Code: {response.StatusCode}");
        //        }

        //        response.EnsureSuccessStatusCode();

        //        return ParseTripSearchXmlResponse(responseString, null);
        //    }

    }
}
