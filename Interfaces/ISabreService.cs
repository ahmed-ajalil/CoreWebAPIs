

using CoreWebAPIs.Models;

namespace CoreWebAPIs.Interfaces
{
    public interface ISabreService
    {
        Task<TripSearchResponse> SearchTripsAsync(TripSearchRequest request);
        Task<ReservationDetailResponse> GetReservationAsync(GetReservationRequest request);
        Task<TripSearchResponse> SearchByFfpAsync(FfpSearchRequest request);
        //Task<TripSearchResponse> SearchByPhoneAsync(PhoneSearchRequest request);
        Task<ReservationDetailResponse> CertReservationAsync(GetReservationRequest request);
    }
}
