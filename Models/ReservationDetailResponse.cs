namespace CoreWebAPIs.Models
{
    public class ReservationDetailResponse
    {
        public BookingInfo? BookingDetails { get; set; }
        public PointOfSale? PointOfSale { get; set; }
        public List<Passenger> Passengers { get; set; } = new();
        public List<ItinerarySegment> Itinerary { get; set; } = new();
        public List<Contact> Contacts { get; set; } = new();
        public FormOfPayment? PaymentDetails { get; set; }
        public List<Remark> Remarks { get; set; } = new();
        public List<string> TicketNumbers { get; set; } = new();
    }

    public class BookingInfo
    {
        public string Locator { get; set; } = string.Empty;
        public DateTime? CreationTimestamp { get; set; }
        public DateTime? UpdateTimestamp { get; set; }
        public string? CreationAgentId { get; set; }
        public string? PnrSequence { get; set; }
    }

    public class PointOfSale
    {
        public string? PseudoCityCode { get; set; }
        public string? AgentSine { get; set; }
        public string? IsoCountry { get; set; }
        public string? AirlineVendorId { get; set; }
    }

    public class Passenger
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public List<SpecialRequest> SpecialRequests { get; set; } = new();
    }

    public class SpecialRequest
    {
        public string? Code { get; set; } // e.g., TKNE, DOCA
        public string? FreeText { get; set; }
        public string? TicketNumber { get; set; }
    }

    public class ItinerarySegment
    {
        public int Sequence { get; set; }
        public string? MarketingAirline { get; set; }
        public string? MarketingFlightNumber { get; set; }
        public string? OperatingAirline { get; set; }
        public string? OperatingFlightNumber { get; set; }
        public string? ClassOfService { get; set; }
        public string? EquipmentType { get; set; }
        public string? Status { get; set; }
        public LocationInfo? Departure { get; set; }
        public LocationInfo? Arrival { get; set; }
        public CabinInfo? Cabin { get; set; }
    }

    public class LocationInfo
    {
        public string? AirportCode { get; set; }
        public DateTime? DateTime { get; set; }
        public string? Terminal { get; set; }
    }

    public class CabinInfo
    {
        public string? Code { get; set; } // e.g., Y for Economy
        public string? Name { get; set; }
    }

    public class Contact
    {
        public string? Type { get; set; } // "Phone", "Email"
        public string? Value { get; set; }
    }

    public class FormOfPayment
    {
        public string? Type { get; set; } // e.g., "CC" for Credit Card
        public string? CardCode { get; set; } // e.g., "VI" for Visa
        public string? MaskedCardNumber { get; set; }
        public string? ExpiryDate { get; set; }
    }

    public class Remark
    {
        public string? Type { get; set; } // e.g., HS, FOP
        public string? Text { get; set; }
    }
}
