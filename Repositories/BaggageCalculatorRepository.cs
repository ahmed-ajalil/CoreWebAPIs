using CoreWebAPIs.Context;
using CoreWebAPIs.Helpers;
using CoreWebAPIs.Interfaces;
using CoreWebAPIs.Models;
using CoreWebAPIs.Services;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Reflection.Metadata;

namespace CoreWebAPIs.Repositories
{
    public class DataRepository : IDataInterface
    {
        private readonly ApplicationDbContext _context;
        private readonly DataService _dataService;
        public DataRepository(ApplicationDbContext context, DataService dataService)
        {

            _context = context;
            _dataService = dataService;

        }

        public async Task<ApiResponseModel<ZoneModel>> AddNewZone(ZoneModel model)
        {
            ApiResponseModel<ZoneModel> apiResponseModel = new ApiResponseModel<ZoneModel>();

            if (model != null)
            {
                try
                {
                    _context.Add(model);
                    var save = await _dataService.SaveChangesAsync();
                    if (save.IsSaveSuccessfully)
                    {
                        apiResponseModel.StatusCode = HttpStatusCode.OK;
                        apiResponseModel.Data = model;
                        apiResponseModel.Message = "success";
                    }
                    else
                    {
                        apiResponseModel.StatusCode = HttpStatusCode.InternalServerError;
                        apiResponseModel.Data = model;
                        apiResponseModel.Message = "Failed";
                        apiResponseModel.Error = save.Error;

                    }
                }
                catch (Exception ex)
                {

                    apiResponseModel.StatusCode = HttpStatusCode.InternalServerError;
                    apiResponseModel.Data = model;
                    apiResponseModel.Message = "Failed";
                    apiResponseModel.Error = ex.Message.ToString();
                }
            }
            else
            {
                apiResponseModel.StatusCode = HttpStatusCode.InternalServerError;
                apiResponseModel.Data = model;
                apiResponseModel.Message = "Failed";
                apiResponseModel.Error = "Provide model data";
            }

            return apiResponseModel;
        }

        public async Task<ApiResponseModel<List<T>>> GetAll<T>() where T : class
        {
            ApiResponseModel<List<T>> apiResponseModel = new ApiResponseModel<List<T>>();

            try
            {

                var dbSet = _context.Set<T>();


                List<T> entities = await dbSet.ToListAsync();


                apiResponseModel.StatusCode = HttpStatusCode.OK;
                apiResponseModel.Data = entities;
                apiResponseModel.Message = "success";
            }
            catch (Exception ex)
            {

                apiResponseModel.StatusCode = HttpStatusCode.InternalServerError;
                apiResponseModel.Data = null;
                apiResponseModel.Message = "Failed";
                apiResponseModel.Error = ex.Message;
            }

            return apiResponseModel;
        }

        public async Task<ApiResponseModel<T>> AddNew<T>(T model) where T : class
        {
            ApiResponseModel<T> apiResponseModel = new ApiResponseModel<T>();

            if (model != null)
            {
                try
                {
                    await _context.AddAsync(model);
                    var save = await _dataService.SaveChangesAsync();
                    if (save.IsSaveSuccessfully)
                    {
                        apiResponseModel.StatusCode = HttpStatusCode.OK;
                        apiResponseModel.Data = model;
                        apiResponseModel.Message = "success";
                    }
                    else
                    {
                        apiResponseModel.StatusCode = HttpStatusCode.InternalServerError;
                        apiResponseModel.Data = model;
                        apiResponseModel.Message = "Failed";
                        apiResponseModel.Error = save.Error;

                    }
                }
                catch (Exception ex)
                {

                    apiResponseModel.StatusCode = HttpStatusCode.InternalServerError;
                    apiResponseModel.Data = null;
                    apiResponseModel.Message = "Failed";
                    apiResponseModel.Error = ex.Message;
                }

            }


            return apiResponseModel;
        }



        public async Task<ApiResponseModel<CountryModel>> AddNewCountry(CountryModel model)
        {
            ApiResponseModel<CountryModel> apiResponseModel = new ApiResponseModel<CountryModel>();

            if (model != null)
            {
                try
                {
                    ZoneModel zone = await _context.Zones.FirstOrDefaultAsync(x => x.Id == model.Zone.Id);
                    model.Zone = zone;
                    _context.Add(model);
                    var save = await _dataService.SaveChangesAsync();
                    if (save.IsSaveSuccessfully)
                    {
                        apiResponseModel.StatusCode = HttpStatusCode.OK;
                        apiResponseModel.Data = null;
                        apiResponseModel.Message = "success";
                    }
                    else
                    {
                        apiResponseModel.StatusCode = HttpStatusCode.InternalServerError;
                        apiResponseModel.Data = model;
                        apiResponseModel.Message = "Failed";
                        apiResponseModel.Error = save.Error;

                    }
                }
                catch (Exception ex)
                {

                    apiResponseModel.StatusCode = HttpStatusCode.InternalServerError;
                    apiResponseModel.Data = model;
                    apiResponseModel.Message = "Failed";
                    apiResponseModel.Error = ex.Message.ToString();
                }
            }
            else
            {
                apiResponseModel.StatusCode = HttpStatusCode.InternalServerError;
                apiResponseModel.Data = model;
                apiResponseModel.Message = "Failed";
                apiResponseModel.Error = "Provide model data";
            }

            return apiResponseModel;
        }


        public async Task<ApiResponseModel<ClassOfServiceModel>> AddNewClassOfService(ClassOfServiceModel model)
        {
            ApiResponseModel<ClassOfServiceModel> apiResponseModel = new ApiResponseModel<ClassOfServiceModel>();

            if (model != null)
            {
                try
                {
                    _context.Add(model);
                    var save = await _dataService.SaveChangesAsync();
                    if (save.IsSaveSuccessfully)
                    {
                        apiResponseModel.StatusCode = HttpStatusCode.OK;
                        apiResponseModel.Data = model;
                        apiResponseModel.Message = "success";
                    }
                    else
                    {
                        apiResponseModel.StatusCode = HttpStatusCode.InternalServerError;
                        apiResponseModel.Data = model;
                        apiResponseModel.Message = "Failed";
                        apiResponseModel.Error = save.Error;

                    }
                }
                catch (Exception ex)
                {

                    apiResponseModel.StatusCode = HttpStatusCode.InternalServerError;
                    apiResponseModel.Data = model;
                    apiResponseModel.Message = "Failed";
                    apiResponseModel.Error = ex.Message.ToString();
                }
            }
            else
            {
                apiResponseModel.StatusCode = HttpStatusCode.InternalServerError;
                apiResponseModel.Data = model;
                apiResponseModel.Message = "Failed";
                apiResponseModel.Error = "Provide model data";
            }

            return apiResponseModel;
        }

        public async Task<ApiResponseModel<FareTypeModel>> AddNewFareType(FareTypeModel model)
        {
            ApiResponseModel<FareTypeModel> apiResponseModel = new ApiResponseModel<FareTypeModel>();

            if (model != null)
            {
                try
                {
                    ClassOfServiceModel classofService = await _context.ClassOfServices.FirstOrDefaultAsync(x => x.Id == model.ClassOfService.Id);
                    model.ClassOfService = classofService;
                    _context.Add(model);
                    var save = await _dataService.SaveChangesAsync();
                    if (save.IsSaveSuccessfully)
                    {
                        apiResponseModel.StatusCode = HttpStatusCode.OK;
                        apiResponseModel.Data = model;
                        apiResponseModel.Message = "success";
                    }
                    else
                    {
                        apiResponseModel.StatusCode = HttpStatusCode.InternalServerError;
                        apiResponseModel.Data = model;
                        apiResponseModel.Message = "Failed";
                        apiResponseModel.Error = save.Error;

                    }
                }
                catch (Exception ex)
                {

                    apiResponseModel.StatusCode = HttpStatusCode.InternalServerError;
                    apiResponseModel.Data = model;
                    apiResponseModel.Message = "Failed";
                    apiResponseModel.Error = ex.Message.ToString();
                }
            }
            else
            {
                apiResponseModel.StatusCode = HttpStatusCode.InternalServerError;
                apiResponseModel.Data = model;
                apiResponseModel.Message = "Failed";
                apiResponseModel.Error = "Provide model data";
            }

            return apiResponseModel;
        }


        public async Task<ApiResponseModel<RouteModel>> AddNewRoute(RouteModel model)
        {
            ApiResponseModel<RouteModel> apiResponseModel = new ApiResponseModel<RouteModel>();

            if (model != null)
            {
                try
                {
                    FareTypeModel fareType = await _context.FareTypes.FirstOrDefaultAsync(x => x.Id == model.FareType.Id);
                    model.FareType = fareType;
                    _context.Add(model);
                    var save = await _dataService.SaveChangesAsync();
                    if (save.IsSaveSuccessfully)
                    {
                        apiResponseModel.StatusCode = HttpStatusCode.OK;
                        apiResponseModel.Data = model;
                        apiResponseModel.Message = "success";
                    }
                    else
                    {
                        apiResponseModel.StatusCode = HttpStatusCode.InternalServerError;
                        apiResponseModel.Data = model;
                        apiResponseModel.Message = "Failed";
                        apiResponseModel.Error = save.Error;

                    }
                }
                catch (Exception ex)
                {

                    apiResponseModel.StatusCode = HttpStatusCode.InternalServerError;
                    apiResponseModel.Data = model;
                    apiResponseModel.Message = "Failed";
                    apiResponseModel.Error = ex.Message.ToString();
                }
            }
            else
            {
                apiResponseModel.StatusCode = HttpStatusCode.InternalServerError;
                apiResponseModel.Data = model;
                apiResponseModel.Message = "Failed";
                apiResponseModel.Error = "Provide model data";
            }

            return apiResponseModel;
        }

        public async Task<ApiResponseModel<Dictionary<string, object>>> AlloweWeightBasedOnClassAndFareType(string originCountry, string destinationCountry, string className, string fareType)
        {
            ApiResponseModel<Dictionary<string, object>> apiResponseModel = new ApiResponseModel<Dictionary<string, object>>();
            double weight = 0;
            try
            {
                var isSensitive = await ISBaggageSensitiveDestinations(originCountry, destinationCountry);
                int fareTypeID = _context.FareTypes.Where(x => x.ClassOfService.ClassName == className && x.FareTypeName == fareType).Select(x => x.Id).FirstOrDefault();
                if (isSensitive.StatusCode == HttpStatusCode.OK == isSensitive.Data)
                {

                    weight = await _context.Routes.Where(x => x.FareType.Id == fareTypeID && x.IsSensitiveDestination).Select(x => x.Weight).FirstOrDefaultAsync();
                }
                else
                {
                    weight = await _context.Routes.Where(x => x.FareType.Id == fareTypeID && !x.IsSensitiveDestination).Select(x => x.Weight).FirstOrDefaultAsync();
                }
                List<AdditionalInformationModel> additionalInformation = await _context.AdditionalInformation.Where(x=>x.Title != "Number of Bags CaseSensitive Destination").ToListAsync();
                if (isSensitive.Data)
                {
                    foreach (var item in additionalInformation)
                    {
                        if (item.Title == "Number of Bags")
                        {
                            AdditionalInformationModel x = await _context.AdditionalInformation.FirstOrDefaultAsync(x => x.Title == "Number of Bags CaseSensitive Destination");
                             item.Description = x.Description;
                        }
                    }
                }
                

                Dictionary<string,object> baggageAllownce = new Dictionary<string, object>();
                baggageAllownce.Add(Constants.CheckedInBaggage, $"{(className == "Economy" && fareType == "Light" && !isSensitive.Data ? $"Up to 5 bags, with a combined total weight of {weight} kg." : $"Up to 5 bags, with a combined total weight of {weight} kg.")}");
                baggageAllownce.Add(Constants.AdditionalInformation, additionalInformation);
                
            

                apiResponseModel.StatusCode = HttpStatusCode.OK;
                apiResponseModel.Data = baggageAllownce;
                apiResponseModel.Message = "success";
            }
            catch (Exception ex)
            {

                apiResponseModel.StatusCode = HttpStatusCode.InternalServerError;
                apiResponseModel.Data = null;
                apiResponseModel.Message = "Failed";
                apiResponseModel.Error = ex.Message;
            }

            return apiResponseModel;
        }

        public async Task<ApiResponseModel<bool>> ISBaggageSensitiveDestinations(string originCountry, string destinationCountry)
        {
            ApiResponseModel<bool> apiResponseModel = new ApiResponseModel<bool>();
            bool ISSensitiveDestinations = false;
            try
            {


                ISSensitiveDestinations = _context.BaggageSensitiveDestinationsConfigurations.Where(x => (x.Origin == originCountry && x.Destination == destinationCountry) || (x.Origin == originCountry && x.Destination == "All Networks")).Count() > 0;

                apiResponseModel.StatusCode = HttpStatusCode.OK;
                apiResponseModel.Data = ISSensitiveDestinations;
                apiResponseModel.Message = "success";
            }
            catch (Exception ex)
            {

                apiResponseModel.StatusCode = HttpStatusCode.InternalServerError;
                apiResponseModel.Data = ISSensitiveDestinations;
                apiResponseModel.Message = "Failed";
                apiResponseModel.Error = ex.Message;
            }

            return apiResponseModel;
        }

        public async Task<ApiResponseModel<double>> GetExtraBaggageFee(string originCountry, string destinationCountry, int weight)
        {
            ApiResponseModel<double> apiResponseModel = new ApiResponseModel<double>();
            double fair = 0;
            try
            {
                int originZone = _context.Airports.Include(x => x.Zone).Where(x => x.Code == originCountry).Select(x => x.Zone.Id).FirstOrDefault();
                int destinationZone = _context.Airports.Include(x => x.Zone).Where(x => x.Code == destinationCountry).Select(x => x.Zone.Id).FirstOrDefault();

                if (originZone != 0 && destinationZone != 0)
                {
                    double fee = _context.ZoneToZoneFees.Where(x => x.OriginZone == originZone && x.DestinationZone == destinationZone).Select(x => x.Fee).FirstOrDefault();
                    fair = fee * (int)Math.Ceiling(weight / 5.0) * 5;
                }


                //List<T> entities = await dbSet.ToListAsync();


                apiResponseModel.StatusCode = HttpStatusCode.OK;
                apiResponseModel.Data = fair;
                apiResponseModel.Message = "success";
            }
            catch (Exception ex)
            {

                apiResponseModel.StatusCode = HttpStatusCode.InternalServerError;
                apiResponseModel.Data = 0;
                apiResponseModel.Message = "Failed";
                apiResponseModel.Error = ex.Message;
            }

            return apiResponseModel;
        }

        public async Task<ApiResponseModel<Dictionary<string, string>>> FlaconFlayerBaggageAllowence()
        {
            ApiResponseModel<Dictionary<string, string>> apiResponseModel = new ApiResponseModel<Dictionary<string, string>>();
            int weight = 0;
            try
            {
                Dictionary<string, string> baggageAllownce = new Dictionary<string, string>();
                baggageAllownce.Add(Constants.SilverTier, "+10KG");
                baggageAllownce.Add(Constants.GoldTier, "+15KG");
                baggageAllownce.Add(Constants.BlackTier, "+25KG");
                //List<T> entities = await dbSet.ToListAsync();


                apiResponseModel.StatusCode = HttpStatusCode.OK;
                apiResponseModel.Data = baggageAllownce;
                apiResponseModel.Message = "success";
            }
            catch (Exception ex)
            {

                apiResponseModel.StatusCode = HttpStatusCode.InternalServerError;
                apiResponseModel.Data = null;
                apiResponseModel.Message = "Failed";
                apiResponseModel.Error = ex.Message;
            }

            return apiResponseModel;
        }

        public async Task<ApiResponseModel<List<AirportsViewModel>>> GetAllStations()
        {
            ApiResponseModel<List<AirportsViewModel>> apiResponseModel = new ApiResponseModel<List<AirportsViewModel>>();
            try
            {
                //List<AirportCodeModel> data = await _context.Airports.ToListAsync();

                List<AirportsViewModel> data = await _context.Airports.Include(x => x.Zone).Include(x => x.Country)
                    .Select(x => new AirportsViewModel()
                    {
                        City = x.City,
                        Country = x.Country.Country,
                        AirportName = x.AirportName,
                        Code = x.Code,
                        ZoneName = x.Zone.ZoneName
                    }).ToListAsync();
                //List<T> entities = await dbSet.ToListAsync();


                apiResponseModel.StatusCode = HttpStatusCode.OK;
                apiResponseModel.Data = data;
                apiResponseModel.Message = "success";
            }
            catch (Exception ex)
            {

                apiResponseModel.StatusCode = HttpStatusCode.InternalServerError;
                apiResponseModel.Data = null;
                apiResponseModel.Message = "Failed";
                apiResponseModel.Error = ex.Message;
            }

            return apiResponseModel;
        }

        public async Task<ApiResponseModel<List<ZoneViewModel>>> GetZonesStations()
        {
            ApiResponseModel<List<ZoneViewModel>> apiResponseModel = new ApiResponseModel<List<ZoneViewModel>>();
            try
            {
                var zones = await _context.Zones
            .Include(x => x.Airports)
            .Select(z => new ZoneViewModel
            {
                ZoneId = z.Id,
                ZoneName = z.ZoneName,
                Airports = z.Airports.Select(a => new AirportsViewModel
                {
                    City = a.City,
                    Code = a.Code,
                    AirportName = a.AirportName,
                    Country = a.Country.Country,
                    ZoneName = a.Zone.ZoneName
                }).ToList()
            }).ToListAsync();

                apiResponseModel.StatusCode = HttpStatusCode.OK;
                apiResponseModel.Data = zones;
                apiResponseModel.Message = "success";
            }
            catch (Exception ex)
            {

                apiResponseModel.StatusCode = HttpStatusCode.InternalServerError;
                apiResponseModel.Data = null;
                apiResponseModel.Message = "Failed";
                apiResponseModel.Error = ex.Message;
            }

            return apiResponseModel;
        }

        public async Task<ApiResponseModel<AirportsViewModel>> GetZoneByAirportCode(string airportcode)
        {
            ApiResponseModel<AirportsViewModel> apiResponseModel = new ApiResponseModel<AirportsViewModel>();
            try
            {
                AirportsViewModel data = await _context.Airports.Where(x => x.Code == airportcode).Select(x => new AirportsViewModel() { AirportName = x.AirportName, City = x.City, Code = x.Code, Country = x.Country.Country, ZoneName = x.Zone.ZoneName }).FirstOrDefaultAsync();

                apiResponseModel.StatusCode = HttpStatusCode.OK;
                apiResponseModel.Data = data;
                apiResponseModel.Message = "success";
            }
            catch (Exception ex)
            {

                apiResponseModel.StatusCode = HttpStatusCode.InternalServerError;
                apiResponseModel.Data = null;
                apiResponseModel.Message = "Failed";
                apiResponseModel.Error = ex.Message;
            }

            return apiResponseModel;
        }

        public async Task<ApiResponseModel<List<AdditionalInformationModel>>> GetAdditionalInformation()
        {
            ApiResponseModel<List<AdditionalInformationModel>> apiResponseModel = new ApiResponseModel<List<AdditionalInformationModel>>();
            try
            {
                List<AdditionalInformationModel> data = await _context.AdditionalInformation.ToListAsync();

                apiResponseModel.StatusCode = HttpStatusCode.OK;
                apiResponseModel.Data = data;
                apiResponseModel.Message = "success";
            }
            catch (Exception ex)
            {

                apiResponseModel.StatusCode = HttpStatusCode.InternalServerError;
                apiResponseModel.Data = null;
                apiResponseModel.Message = "Failed";
                apiResponseModel.Error = ex.Message;
            }

            return apiResponseModel;
        }

    }
}
