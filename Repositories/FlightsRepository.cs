using CoreWebAPIs.Context;
using CoreWebAPIs.Helpers;
using CoreWebAPIs.Interfaces;
using CoreWebAPIs.Models;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CoreWebAPIs.Repositories
{
    public class FlightsRepository : IFlightsInterface
    {
        private readonly ProfitabilityOTPDbContext _context;
        public FlightsRepository(ProfitabilityOTPDbContext context)
        {
            _context = context;
        }

        public async Task<ApiResponseModel> GetFlightByFlightNumber(string flightNumber, DateTime? fromFlightDate = null, DateTime? toFlightDate = null)
        {
            ApiResponseModel responseModel = new ApiResponseModel();
            if (string.IsNullOrEmpty(flightNumber))
            {
                responseModel.StatusCode = HttpStatusCode.InternalServerError;
                responseModel.Message = $"Flight Number is required";
            }
            else
            {
                try
                {
                    FilteredFlightsDataModel model = new FilteredFlightsDataModel();
                    var query = _context.VwotpFlights.AsQueryable().Where(x => x.FltNr == flightNumber);

                    query = HelperMethods.GetFlightsQueryBasedOnDates(query, fromFlightDate, toFlightDate);

                    using (_context)
                    {
                        List<FlightsModel> flights = await query.Select(x => EntityToModelDTO.VwotpFlightEntityToFlightModelDTO(x)).ToListAsync();
                        model.Flights = flights;
                        model.TotalFlights = flights.Count;
                        if (flights != null)
                        {
                            responseModel.StatusCode = HttpStatusCode.OK;
                            responseModel.Message = "Flights fetched successfully";
                            responseModel.Data = model;
                        }
                    }


                }
                catch (Exception ex)
                {

                    responseModel.StatusCode = HttpStatusCode.InternalServerError;
                    responseModel.Message = $"Error:{ex.Message.ToString()}";
                }
            }



            return responseModel;
        }

        public async Task<ApiResponseModel> GetFlightsByDate(DateTime? fromFlightDate = null, DateTime? toFlightDate = null)
        {
            ApiResponseModel responseModel = new ApiResponseModel();

            try
            {
                using (_context)
                {
                    FilteredFlightsDataModel model = new FilteredFlightsDataModel();
                    var query = _context.VwotpFlights.AsQueryable();
                    query = HelperMethods.GetFlightsQueryBasedOnDates(query, fromFlightDate, toFlightDate);
                    List<FlightsModel> flights = await query.Select(x => EntityToModelDTO.VwotpFlightEntityToFlightModelDTO(x)).ToListAsync();
                    model.TotalFlights = flights.Count;
                    model.Flights = flights;
                    if (flights != null)
                    {
                        responseModel.StatusCode = HttpStatusCode.OK;
                        responseModel.Message = "Flights fetched successfully";
                        responseModel.Data = model;
                    }
                }


            }
            catch (Exception ex)
            {

                responseModel.StatusCode = HttpStatusCode.InternalServerError;
                responseModel.Message = $"Error:{ex.Message.ToString()}";
            }

            return responseModel;
        }

        public async Task<ApiResponseModel> GetCanclledFlights(DateTime? FlightDate = null)
        {
            ApiResponseModel responseModel = new ApiResponseModel();

            try
            {
                using (_context)
                {
                    FilteredFlightsDataModel model = new FilteredFlightsDataModel();
                    var query = _context.VwotpFlights.AsQueryable().Where(x => x.CnclCd != null && x.CnclCd != ApplicationConstants.CanclledFlightsValue);
                    query = HelperMethods.GetFlightsQueryBasedOnDates(query, FlightDate);
                    List<FlightsModel> flights = await query.Select(x => EntityToModelDTO.VwotpFlightEntityToFlightModelDTO(x)).ToListAsync();
                    model.TotalFlights = flights.Count;
                    model.Flights = flights;
                    if (flights != null)
                    {
                        responseModel.StatusCode = HttpStatusCode.OK;
                        responseModel.Message = "Flights fetched successfully";
                        responseModel.Data = model;
                    }
                }


            }
            catch (Exception ex)
            {

                responseModel.StatusCode = HttpStatusCode.InternalServerError;
                responseModel.Message = $"Error:{ex.Message.ToString()}";
            }

            return responseModel;
        }

        public async Task<ApiResponseModel> GetFlightsByDestination(string SCHDep, DateTime? fromFlightDate = null, DateTime? toFlightDate = null)
        {
            ApiResponseModel responseModel = new ApiResponseModel();
            if (string.IsNullOrEmpty(SCHDep))
            {
                responseModel.StatusCode = HttpStatusCode.InternalServerError;
                responseModel.Message = $"Arrival is required";
            }
            else
            {
                try
                {
                    using (_context)
                    {
                        FilteredFlightsDataModel model = new FilteredFlightsDataModel();
                        var query = _context.VwotpFlights.AsQueryable().Where(x => x.ActualArvArpCd == SCHDep);
                        query = HelperMethods.GetFlightsQueryBasedOnDates(query, fromFlightDate, toFlightDate);
                        List<FlightsModel> flights = await query.Select(x => EntityToModelDTO.VwotpFlightEntityToFlightModelDTO(x)).ToListAsync();
                        model.TotalFlights = flights.Count;
                        model.Flights = flights;
                        if (flights != null)
                        {
                            responseModel.StatusCode = HttpStatusCode.OK;
                            responseModel.Message = "Flights fetched successfully";
                            responseModel.Data = model;
                        }
                    }


                }
                catch (Exception ex)
                {

                    responseModel.StatusCode = HttpStatusCode.InternalServerError;
                    responseModel.Message = $"Error:{ex.Message.ToString()}";
                }
            }
            return responseModel;
        }

        public async Task<ApiResponseModel> GetFlightsByDestinationCount(string destination, DateTime? fromFlightDate = null, DateTime? toFlightDate = null)
        {
            ApiResponseModel responseModel = new ApiResponseModel();
            if (string.IsNullOrEmpty(destination))
            {
                responseModel.StatusCode = HttpStatusCode.InternalServerError;
                responseModel.Message = $"Arrival is required";
            }
            else
            {
                try
                {
                    using (_context)
                    {
                        var query = _context.VwotpFlights.AsQueryable().Where(x => x.ActualArvArpCd == destination);
                        query = HelperMethods.GetFlightsQueryBasedOnDates(query, fromFlightDate, toFlightDate);
                        var flights = query.Count();
                        if (flights != null)
                        {
                            responseModel.StatusCode = HttpStatusCode.OK;
                            responseModel.Message = "Flights fetched successfully";
                            responseModel.Data = flights;
                        }
                    }


                }
                catch (Exception ex)
                {

                    responseModel.StatusCode = HttpStatusCode.InternalServerError;
                    responseModel.Message = $"Error:{ex.Message.ToString()}";
                }
            }
            return responseModel;
        }


        public async Task<ApiResponseModel> GetFlightsByDeparture(string departure, DateTime? fromFlightDate = null, DateTime? toFlightDate = null)
        {
            ApiResponseModel responseModel = new ApiResponseModel();
            if (string.IsNullOrEmpty(departure))
            {
                responseModel.StatusCode = HttpStatusCode.InternalServerError;
                responseModel.Message = $"Arrival is required";
            }
            else
            {
                try
                {
                    using (_context)
                    {
                        FilteredFlightsDataModel model = new FilteredFlightsDataModel();
                        var query = _context.VwotpFlights.AsQueryable().Where(x => x.ActualDepArpCd == departure);
                        query = HelperMethods.GetFlightsQueryBasedOnDates(query, fromFlightDate, toFlightDate);
                        List<FlightsModel> flights = await query.Select(x => EntityToModelDTO.VwotpFlightEntityToFlightModelDTO(x)).ToListAsync();
                        model.TotalFlights = flights.Count;
                        model.Flights = flights;
                        if (flights != null)
                        {
                            responseModel.StatusCode = HttpStatusCode.OK;
                            responseModel.Message = "Flights fetched successfully";
                            responseModel.Data = model;
                        }
                    }


                }
                catch (Exception ex)
                {

                    responseModel.StatusCode = HttpStatusCode.InternalServerError;
                    responseModel.Message = $"Error:{ex.Message.ToString()}";
                }
            }
            return responseModel;
        }


        public async Task<ApiResponseModel> GetDelayedFlights(DateTime? fromFlightDate = null, DateTime? toFlightDate = null)
        {
            ApiResponseModel responseModel = new ApiResponseModel();
            try
            {
                using (_context)
                {
                    FilteredFlightsDataModel model = new FilteredFlightsDataModel();
                    var query = _context.VwotpFlights.AsQueryable().Where(x => x.Dep15delaycount == 1);
                    query = HelperMethods.GetFlightsQueryBasedOnDates(query, fromFlightDate, toFlightDate);
                    List<FlightsModel> flights = await query.Select(x => EntityToModelDTO.VwotpFlightEntityToFlightModelDTO(x)).ToListAsync();
                    model.TotalFlights = flights.Count;
                    model.Flights = flights;
                    if (flights != null)
                    {
                        responseModel.StatusCode = HttpStatusCode.OK;
                        responseModel.Message = "Flights fetched successfully";
                        responseModel.Data = model;
                    }
                }


            }
            catch (Exception ex)
            {

                responseModel.StatusCode = HttpStatusCode.InternalServerError;
                responseModel.Message = $"Error:{ex.Message.ToString()}";
            }
            return responseModel;
        }

        public async Task<ApiResponseModel> GetArrivedFlights(string? ArrivedTo, DateTime? fromFlightDate = null, DateTime? toFlightDate = null)
        {
            ApiResponseModel responseModel = new ApiResponseModel();
            try
            {
                using (_context)
                {
                    FilteredFlightsDataModel model = new FilteredFlightsDataModel();
                    var query = _context.VwotpFlights.AsQueryable().Where(x => x.ActualOnblocks != null);
                    query = !string.IsNullOrEmpty(ArrivedTo) ? query.Where(x => x.ActualArvArpCd == ArrivedTo) : query;
                    query = HelperMethods.GetArrivedFlightsQueryBasedOnDates(query, fromFlightDate, toFlightDate);
                    List<FlightsModel> flights = await query.Select(x => EntityToModelDTO.VwotpFlightEntityToFlightModelDTO(x)).ToListAsync();
                    model.TotalFlights = flights.Count;
                    model.Flights = flights;
                    if (flights != null)
                    {
                        responseModel.StatusCode = HttpStatusCode.OK;
                        responseModel.Message = "Flights fetched successfully";
                        responseModel.Data = model;
                    }
                }


            }
            catch (Exception ex)
            {

                responseModel.StatusCode = HttpStatusCode.InternalServerError;
                responseModel.Message = $"Error:{ex.Message.ToString()}";
            }
            return responseModel;
        }

        public async Task<ApiResponseModel> GetDepartedFlights(string? DepartedFrom, DateTime flightDate)
        {
            ApiResponseModel responseModel = new ApiResponseModel();
            try
            {
                using (_context)
                {
                    FilteredFlightsDataModel model = new FilteredFlightsDataModel();
                    var query = _context.VwotpFlights.AsQueryable();
                    query = !string.IsNullOrEmpty(DepartedFrom) ? query.Where(x => x.ActualDepArpCd == DepartedFrom) : query;
                    query = HelperMethods.GetDepartedFlightsQueryBasedOnDates(query, flightDate);
                    List<FlightsModel> flights = await query.Select(x => EntityToModelDTO.VwotpFlightEntityToFlightModelDTO(x)).ToListAsync();
                    model.TotalFlights = flights.Count;
                    model.Flights = flights;
                    if (flights != null)
                    {
                        responseModel.StatusCode = HttpStatusCode.OK;
                        responseModel.Message = "Flights fetched successfully";
                        responseModel.Data = model;
                    }
                }


            }
            catch (Exception ex)
            {

                responseModel.StatusCode = HttpStatusCode.InternalServerError;
                responseModel.Message = $"Error:{ex.Message.ToString()}";
            }
            return responseModel;
        }

        public async Task<ApiResponseModel> GetFlightsStatusBetweenDates(string? departure, string? arrival, DateTime fromFlightDate, DateTime toFlightDate)
        {
            ApiResponseModel responseModel = new ApiResponseModel();
            try
            {
                using (_context)
                {

                    FilteredFlightsDataModel model = new FilteredFlightsDataModel();
                    var query = _context.VwotpFlights.AsQueryable();
                    query = !string.IsNullOrEmpty(departure) ? query.Where(x => x.ActualDepArpCd == departure) : query;
                    query = !string.IsNullOrEmpty(arrival) ? query.Where(x => x.ActualArvArpCd == arrival) : query;
                    query = HelperMethods.GetFlightsQueryBasedOnDates(query, fromFlightDate, toFlightDate);
                    List<FlightsModel> flights = await query.Select(x => EntityToModelDTO.VwotpFlightEntityToFlightModelDTO(x)).ToListAsync();
                    model.TotalFlights = flights.Count;
                    model.Flights = flights;
                    if (flights != null)
                    {
                        responseModel.StatusCode = HttpStatusCode.OK;
                        responseModel.Message = "Flights fetched successfully";
                        responseModel.Data = model;
                    }
                }


            }
            catch (Exception ex)
            {

                responseModel.StatusCode = HttpStatusCode.InternalServerError;
                responseModel.Message = $"Error:{ex.Message.ToString()}";
            }
            return responseModel;
        }

        public async Task<ApiResponseModel> GetNextFlights(DateTime? fromFlightDate = null, DateTime? toFlightDate = null)
        {
            ApiResponseModel responseModel = new ApiResponseModel();
            try
            {
                using (_context)
                {
                    var query = _context.VwotpFlights.AsQueryable();
                    query = HelperMethods.GetFlightsQueryBasedOnDates(query, fromFlightDate, toFlightDate);
                    var flights = await query.ToListAsync();
                    if (flights != null)
                    {
                        responseModel.StatusCode = HttpStatusCode.OK;
                        responseModel.Message = "Flights fetched successfully";
                        responseModel.Data = flights;
                    }
                }


            }
            catch (Exception ex)
            {

                responseModel.StatusCode = HttpStatusCode.InternalServerError;
                responseModel.Message = $"Error:{ex.Message.ToString()}";
            }
            return responseModel;
        }

        public async Task<ApiResponseModel> GetDiversionFlights(DateTime? fromFlightDate = null, DateTime? toFlightDate = null)
        {
            ApiResponseModel responseModel = new ApiResponseModel();
            try
            {
                using (_context)
                {
                    FilteredFlightsDataModel model = new FilteredFlightsDataModel();
                    var query = _context.VwotpFlights.AsQueryable().Where(x => x.ActualArvArpCd != x.ArvArpCd);
                    query = HelperMethods.GetFlightsQueryBasedOnDates(query, fromFlightDate, toFlightDate);
                    List<FlightsModel> flights = await query.Select(x => EntityToModelDTO.VwotpFlightEntityToFlightModelDTO(x)).ToListAsync();
                    model.TotalFlights = flights.Count;
                    model.Flights = flights;
                    if (flights != null)
                    {
                        responseModel.StatusCode = HttpStatusCode.OK;
                        responseModel.Message = "Flights fetched successfully";
                        responseModel.Data = flights;
                    }
                }


            }
            catch (Exception ex)
            {

                responseModel.StatusCode = HttpStatusCode.InternalServerError;
                responseModel.Message = $"Error:{ex.Message.ToString()}";
            }
            return responseModel;
        }

        public async Task<ApiResponseModel> GetAirReturnFlights(DateTime? fromFlightDate = null, DateTime? toFlightDate = null)
        {
            ApiResponseModel responseModel = new ApiResponseModel();
            try
            {
                using (_context)
                {
                    FilteredFlightsDataModel model = new FilteredFlightsDataModel();
                    var query = _context.VwotpFlights.AsQueryable().Where(x => x.ActualDepArpCd == x.ActualArvArpCd);
                    query = HelperMethods.GetFlightsQueryBasedOnDates(query, fromFlightDate, toFlightDate);
                    List<FlightsModel> flights = await query.Select(x => EntityToModelDTO.VwotpFlightEntityToFlightModelDTO(x)).ToListAsync();
                    model.TotalFlights = flights.Count;
                    model.Flights = flights.Where(x => int.TryParse(x.FlightNumber, out int fltNr) && !(fltNr >= 9000 && fltNr <= 9999)).ToList();
                    if (flights != null)
                    {
                        responseModel.StatusCode = HttpStatusCode.OK;
                        responseModel.Message = "Flights fetched successfully";
                        responseModel.Data = flights;
                    }
                }


            }
            catch (Exception ex)
            {

                responseModel.StatusCode = HttpStatusCode.InternalServerError;
                responseModel.Message = $"Error:{ex.Message.ToString()}";
            }
            return responseModel;
        }

        //Methods To workOn latter

        public Task<ApiResponseModel> GetFlightsBySchedulFleet(string SCHFleet, DateTime fromFlightDate, DateTime toFlightDate)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponseModel> GetFlightsByScheduleArrival(string SCHArr, DateTime fromFlightDate, DateTime toFlightDate)
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResponseModel> GetFlightsByActualArrival(string ACTUArr, DateTime fromFlightDate, DateTime toFlightDate)
        {
            ApiResponseModel responseModel = new ApiResponseModel();
            if (string.IsNullOrEmpty(ACTUArr))
            {
                responseModel.StatusCode = HttpStatusCode.InternalServerError;
                responseModel.Message = $"Arrival is required";
            }
            else
            {
                try
                {
                    using (_context)
                    {
                        var flights = await _context.VwotpFlights.Where(x => x.ActualDepDt.Value.Date >= HelperMethods.ConvertDateTimeToUTC(fromFlightDate).Date
                        && x.ActualArvDt.Value.Date <= (toFlightDate)
                        && x.ActualArvArpCd == ACTUArr).ToListAsync();
                        if (flights != null)
                        {
                            responseModel.StatusCode = HttpStatusCode.OK;
                            responseModel.Message = "Flights fetched successfully";
                            responseModel.Data = flights;
                        }
                    }


                }
                catch (Exception ex)
                {

                    responseModel.StatusCode = HttpStatusCode.InternalServerError;
                    responseModel.Message = $"Error:{ex.Message.ToString()}";
                }
            }
            return responseModel;
        }

        public async Task<ApiResponseModel> GetFlightsByActualDeparture(string ACTUDep, DateTime fromFlightDate, DateTime toFlightDate)
        {
            ApiResponseModel responseModel = new ApiResponseModel();
            if (string.IsNullOrEmpty(ACTUDep))
            {
                responseModel.StatusCode = HttpStatusCode.InternalServerError;
                responseModel.Message = $"Arrival is required";
            }
            else
            {
                try
                {
                    using (_context)
                    {
                        var flights = await _context.VwotpFlights.Where(x => x.ActualDepDt.Value.Date >= HelperMethods.ConvertDateTimeToUTC(fromFlightDate).Date
                        && x.ActualArvDt.Value.Date <= HelperMethods.ConvertDateTimeToUTC(toFlightDate).Date
                        && x.ActualDepArpCd == ACTUDep).ToListAsync();
                        if (flights != null)
                        {
                            responseModel.StatusCode = HttpStatusCode.OK;
                            responseModel.Message = "Flights fetched successfully";
                            responseModel.Data = flights;
                        }
                    }


                }
                catch (Exception ex)
                {

                    responseModel.StatusCode = HttpStatusCode.InternalServerError;
                    responseModel.Message = $"Error:{ex.Message.ToString()}";
                }
            }
            return responseModel;
        }

        public async Task<ApiResponseModel> GetFlightsByActualFleet(string ACTUFleet, DateTime fromFlightDate, DateTime toFlightDate)
        {
            ApiResponseModel responseModel = new ApiResponseModel();
            if (string.IsNullOrEmpty(ACTUFleet))
            {
                responseModel.StatusCode = HttpStatusCode.InternalServerError;
                responseModel.Message = $"Arrival is required";
            }
            else
            {
                try
                {
                    using (_context)
                    {
                        var flights = await _context.VwotpFlights.Where(x => x.ActualDepDt.Value.Date == HelperMethods.ConvertDateTimeToUTC(fromFlightDate).Date
                        && x.FltNr == ACTUFleet).ToListAsync();
                        if (flights != null)
                        {
                            responseModel.StatusCode = HttpStatusCode.OK;
                            responseModel.Message = "Flights fetched successfully";
                            responseModel.Data = flights;
                        }
                    }


                }
                catch (Exception ex)
                {

                    responseModel.StatusCode = HttpStatusCode.InternalServerError;
                    responseModel.Message = $"Error:{ex.Message.ToString()}";
                }
            }
            return responseModel;
        }
    }
}
