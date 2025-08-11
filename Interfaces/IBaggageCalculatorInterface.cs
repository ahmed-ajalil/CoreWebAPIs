
using CoreWebAPIs.Models;

namespace CoreWebAPIs.Interfaces
{
    public interface IDataInterface
    {


        Task<ApiResponseModel<List<T>>> GetAll<T>() where T : class;

        Task<ApiResponseModel<T>> AddNew<T>(T model) where T : class;


        Task<ApiResponseModel<CountryModel>> AddNewCountry(CountryModel model);
        Task<ApiResponseModel<ZoneModel>> AddNewZone(ZoneModel model);
        Task<ApiResponseModel<ClassOfServiceModel>> AddNewClassOfService(ClassOfServiceModel model);
        Task<ApiResponseModel<FareTypeModel>> AddNewFareType(FareTypeModel model);
        Task<ApiResponseModel<RouteModel>> AddNewRoute(RouteModel model);

        Task<ApiResponseModel<Dictionary<string, object>>> AlloweWeightBasedOnClassAndFareType(string originCountry, string destinationCountry, string className, string fareType);

        Task<ApiResponseModel<double>> GetExtraBaggageFee(string originCountry, string destinationCountry, int weight);
        Task<ApiResponseModel<Dictionary<string, string>>> FlaconFlayerBaggageAllowence();
        Task<ApiResponseModel<List<AirportsViewModel>>> GetAllStations();
        Task<ApiResponseModel<List<ZoneViewModel>>> GetZonesStations();
        Task<ApiResponseModel<AirportsViewModel>> GetZoneByAirportCode(string airportcode);

        Task<ApiResponseModel<List<AdditionalInformationModel>>> GetAdditionalInformation();
    }
}
