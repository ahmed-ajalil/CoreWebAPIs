
using CoreWebAPIs.Models;

namespace CoreWebAPIs.Helpers
{
    public static class EntityToModelDTO
    {
        public static FlightsModel VwotpFlightEntityToFlightModelDTO(VwotpFlight entity)
        {

            FlightsModel model = new FlightsModel();

            try
            {
                model = new FlightsModel
                {
                    FlightNumber = entity.FltNr,
                    FlightDate = entity.PubDepDt,
                    ActualDepartedAirport = entity.ActualDepArpCd,
                    ActualArrivedAirport = entity.ActualArvArpCd,
                    SchedualDepartureDate = entity.SchDepDt,
                    SchedualArrivedDate = entity.SchArvDt,
                    PublishedDepartureDate = entity.PubDepDt,
                    PublishedArrivedDate = entity.PubArvDt,
                    ActualArrivedDate = entity.ActualArvDt,
                    ActualDepartureDate = entity.ActualDepDt,
                    LatestTailNubmer = entity.LatestTailNr,
                    LatestEqpCd = entity.LatestEqpCd,
                    IsFlightCancled = entity.CnclCd != null && entity.CnclCd != ApplicationConstants.CanclledFlightsValue ? 1 : 0,
                    IsFlightArrived = entity.ActualArvDt != null ? 1 : 0,
                    IsFlightDelayed = entity.Dep15delaycount == 1 ? 1 : 0,
                    IsFlightDeparted = entity.ActualArvDt != null ? 1 : 0,
                };
            }
            catch (Exception ex)
            {
                //log to file
            }

            return model;

        }
    }
}
