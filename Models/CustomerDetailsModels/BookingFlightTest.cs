using System;
using System.Collections.Generic;

namespace CoreWebAPIs.Models.CustomerDetailsModels;

public partial class BookingFlightTest
{
    public DateTime? Departuredate { get; set; }

    public string? Destination { get; set; }

    public decimal? Flightnumber { get; set; }

    public string? Origin { get; set; }

    public decimal? Status { get; set; }
}
