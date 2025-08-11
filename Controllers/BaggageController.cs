using CoreWebAPIs.Interfaces;
using CoreWebAPIs.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CoreWebAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaggageController : ControllerBase
    {

        private readonly IDataInterface _dataInterface;
        public BaggageController(IDataInterface dataInterface)
        {
            _dataInterface = dataInterface; 
        }

        [HttpPost]
        [Route("addnewzone")]
        public async Task<ApiResponseModel<ZoneModel>> AddNewZone(ZoneModel model)
        {
            ApiResponseModel<ZoneModel> responseModel = new ApiResponseModel<ZoneModel>();

            try
            {
                responseModel = await _dataInterface.AddNew<ZoneModel>(model);
            }
            catch (Exception ex)
            {

                responseModel.StatusCode = HttpStatusCode.InternalServerError;
                responseModel.Data = null;
                responseModel.Message = "Failed";
                responseModel.Error = ex.Message;
            }

            return responseModel;
        }


        [HttpPost]
        [Route("addnewcountry")]
        public async Task<ApiResponseModel<CountryModel>> AddNewCountry(CountryModel model)
        {
            ApiResponseModel<CountryModel> responseModel = new ApiResponseModel<CountryModel>();

            try
            {
                responseModel = await _dataInterface.AddNewCountry(model);
            }
            catch (Exception ex)
            {

                responseModel.StatusCode = HttpStatusCode.InternalServerError;
                responseModel.Data = null;
                responseModel.Message = "Failed";
                responseModel.Error = ex.Message;
            }

            return responseModel;
        }

        [HttpPost]
        [Route("addnewclass")]
        public async Task<ApiResponseModel<ClassOfServiceModel>> AddNewClass(ClassOfServiceModel model)
        {
            ApiResponseModel<ClassOfServiceModel> responseModel = new ApiResponseModel<ClassOfServiceModel>();

            try
            {
                responseModel = await _dataInterface.AddNewClassOfService(model);
            }
            catch (Exception ex)
            {

                responseModel.StatusCode = HttpStatusCode.InternalServerError;
                responseModel.Data = null;
                responseModel.Message = "Failed";
                responseModel.Error = ex.Message;
            }

            return responseModel;
        }

        [HttpPost]
        [Route("addnewfaretype")]
        public async Task<ApiResponseModel<FareTypeModel>> AddNewFareType(FareTypeModel model)
        {
            ApiResponseModel<FareTypeModel> responseModel = new ApiResponseModel<FareTypeModel>();

            try
            {
                responseModel = await _dataInterface.AddNewFareType(model);
            }
            catch (Exception ex)
            {

                responseModel.StatusCode = HttpStatusCode.InternalServerError;
                responseModel.Data = null;
                responseModel.Message = "Failed";
                responseModel.Error = ex.Message;
            }

            return responseModel;
        }

        [HttpPost]
        [Route("addnewroute")]
        public async Task<ApiResponseModel<RouteModel>> AddNewRoute(RouteModel model)
        {
            ApiResponseModel<RouteModel> responseModel = new ApiResponseModel<RouteModel>();

            try
            {
                responseModel = await _dataInterface.AddNewRoute(model);
            }
            catch (Exception ex)
            {

                responseModel.StatusCode = HttpStatusCode.InternalServerError;
                responseModel.Data = null;
                responseModel.Message = "Failed";
                responseModel.Error = ex.Message;
            }

            return responseModel;
        }



        /// <summary>
        /// Get Baggage Information
        /// </summary>
        /// <param name="origin">Origin Airport Code.</param>
        /// <param name="destination">Destination Airport Code.</param>
        /// <param name="className">Class Name.</param>
        /// <param name="fairType">fair type.</param>
        /// <returns>A dictionary giving baggage count with weight</returns>
        /// <remarks>
        ///  <b>Request:</b><br></br>
        ///
        ///     GET /baggage/baggageinformation?origin=DOH&amp;destination=PEW&amp;className=Economy&amp;fairType=Smart<br></br>
        ///  <b>PARAMS:</b><br></br>
        ///  origin = BAH {required, Type:string => 3 digit airportcode}<br></br>
        ///  destination = BAH {required, Type:string => 3 digit airportcode}<br></br>
        ///  className = Economy {required, Type:string => Economy Or Business}<br></br>
        ///  fairType = Light {required, Type:string => Light, Smart, Flex}<br></br>
        ///  
        /// </remarks>
        [HttpGet]
        [Authorize(Roles = "Api.BaggageCalculator.Read")]
        [Route("baggageinformation")]
        public async Task<ApiResponseModel<Dictionary<string, object>>> BaggageInformation([FromQuery]string origin,string destination, string className, string fairType)
        {
            ApiResponseModel<Dictionary<string, object>> responseModel = new ApiResponseModel<Dictionary<string, object>>();

            try
            {
                responseModel = await _dataInterface.AlloweWeightBasedOnClassAndFareType(origin, destination, className, fairType);
            }
            catch (Exception ex)
            {

                responseModel.StatusCode = HttpStatusCode.InternalServerError;
                responseModel.Data = null;
                responseModel.Message = "Failed";
                responseModel.Error = ex.Message;
            }

            return responseModel;
        }


        /// <summary>
        /// Get Additional Baggage Allowence
        /// </summary>
        /// <param name="origin">Origin Airport Code.</param>
        /// <param name="destination">Destination Airport Code.</param>
        /// <param name="weight">Weight.</param>
        /// <returns>returns the price for the weight</returns>
        /// <remarks>
        ///  <b>Request:</b><br></br>
        ///
        ///     GET /baggage/getextraweighfee?origin=BAH&amp;destination=LHR&amp;weight=9<br></br>
        ///  <b>PARAMS:</b><br></br>
        ///  origin = BAH {required, Type:string => 3 digit airportcode}<br></br>
        ///  destination = BAH {required, Type:string => 3 digit airportcode}<br></br>
        ///  weight = 5 {required, Type:int => 5}<br></br>
        ///  
        /// </remarks>
        [HttpGet]
        [Authorize(Roles = "Api.BaggageCalculator.Read")]
        [Route("additionalbaggageallowence")]
        public async Task<ApiResponseModel<double>> AdditionalBaggageAllowence([FromQuery] string origin, string destination, int weight)
        {
            ApiResponseModel<double> responseModel = new ApiResponseModel<double>();

            try
            {
                responseModel = await _dataInterface.GetExtraBaggageFee(origin, destination, weight);
            }
            catch (Exception ex)
            {

                responseModel.StatusCode = HttpStatusCode.InternalServerError;
                responseModel.Data = 0;
                responseModel.Message = "Failed";
                responseModel.Error = ex.Message;
            }

            return responseModel;
        }

        /// <summary>
        /// Get Flaco nFlayer Baggage Allowence
        /// </summary>
        /// <returns>returns tier based extra baggage allownce</returns>
        /// <remarks>
        ///  <b>Request:</b><br></br>
        ///
        ///     GET /baggage/flaconflayerbaggageallowence<br></br>
        ///  
        /// </remarks>
        [HttpGet]
        [Authorize(Roles = "Api.BaggageCalculator.Read")]
        [Route("flaconflayerbaggageallowence")]
        public async Task<ApiResponseModel<Dictionary<string, string>>> FlaconFlayerBaggageAllowence()
        {
            ApiResponseModel<Dictionary<string, string>> responseModel = new ApiResponseModel<Dictionary<string, string>>();

            try
            {
                responseModel = await _dataInterface.FlaconFlayerBaggageAllowence();
            }
            catch (Exception ex)
            {

                responseModel.StatusCode = HttpStatusCode.InternalServerError;
                responseModel.Data = null;
                responseModel.Message = "Failed";
                responseModel.Error = ex.Message;
            }

            return responseModel;
        }




        /// <summary>
        /// Get All Stations
        /// </summary>
        /// <returns>returns list of available stations</returns>
        /// <remarks>
        ///  <b>Request:</b><br></br>
        ///
        ///     GET /baggage/allstattions<br></br>
        ///  
        /// </remarks>
        [HttpGet]
        [Authorize(Roles = "Api.BaggageCalculator.Read")]
        [Route("allstattions")]
        public async Task<ApiResponseModel<List<AirportsViewModel>>> AllStattions()
        {
            ApiResponseModel<List<AirportsViewModel>> responseModel = new ApiResponseModel<List<AirportsViewModel>>();

            try
            {
                responseModel = await _dataInterface.GetAllStations();
            }
            catch (Exception ex)
            {

                responseModel.StatusCode = HttpStatusCode.InternalServerError;
                responseModel.Data = null;
                responseModel.Message = "Failed";
                responseModel.Error = ex.Message;
            }

            return responseModel;
        }

        /// <summary>
        /// Get All Zones with stations
        /// </summary>
        /// <returns>returns list of available zones with station stations</returns>
        /// <remarks>
        ///  <b>Request:</b><br></br>
        ///
        ///     GET /baggage/allzones<br></br>
        ///  
        /// </remarks>
        [HttpGet]
        [Authorize(Roles = "Api.BaggageCalculator.Read")]
        [Route("allzones")]
        public async Task<ApiResponseModel<List<ZoneViewModel>>> AllZones()
        {
            ApiResponseModel<List<ZoneViewModel>> responseModel = new ApiResponseModel<List<ZoneViewModel>>();

            try
            {
                responseModel = await _dataInterface.GetZonesStations();
            }
            catch (Exception ex)
            {

                responseModel.StatusCode = HttpStatusCode.InternalServerError;
                responseModel.Data = null;
                responseModel.Message = "Failed";
                responseModel.Error = ex.Message;
            }

            return responseModel;
        }



        /// <summary>
        /// Get All Zones by airportcode
        /// </summary>
        /// <returns>returns zone that match the airportcode</returns>
        /// <remarks>
        ///  <b>Request:</b><br></br>
        ///
        ///     GET /baggage/getzonebyairportcode<br></br>
        ///  
        /// </remarks>
        [HttpGet]
        [Authorize(Roles = "Api.BaggageCalculator.Read")]
        [Route("getzonebyairportcode")]
        public async Task<ApiResponseModel<AirportsViewModel>> GetZoneByAirportCode([FromQuery] string airportcode)
        {
            ApiResponseModel<AirportsViewModel> responseModel = new ApiResponseModel<AirportsViewModel>();

            try
            {
                responseModel = await _dataInterface.GetZoneByAirportCode(airportcode);
            }
            catch (Exception ex)
            {

                responseModel.StatusCode = HttpStatusCode.InternalServerError;
                responseModel.Data = null;
                responseModel.Message = "Failed";
                responseModel.Error = ex.Message;
            }

            return responseModel;
        }



        /// <summary>
        /// Get Additional Information
        /// </summary>
        /// <returns>returns list of additional information</returns>
        /// <remarks>
        ///  <b>Request:</b><br></br>
        ///
        ///     GET /baggage/getadditionalinformation<br></br>
        ///  
        /// </remarks>
        [HttpGet]
        [Authorize(Roles = "Api.BaggageCalculator.Read")]
        [Route("getadditionalinformation")]
        public async Task<ApiResponseModel<List<AdditionalInformationModel>>> GetAdditionalInformation()
        {
            ApiResponseModel<List<AdditionalInformationModel>> responseModel = new ApiResponseModel<List<AdditionalInformationModel>>();

            try
            {
                responseModel = await _dataInterface.GetAdditionalInformation();    
            }
            catch (Exception ex)
            {

                responseModel.StatusCode = HttpStatusCode.InternalServerError;
                responseModel.Data = null;
                responseModel.Message = "Failed";
                responseModel.Error = ex.Message;
            }

            return responseModel;
        }
    }
}
