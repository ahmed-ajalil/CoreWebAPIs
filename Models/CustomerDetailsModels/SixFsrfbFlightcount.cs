using System;
using System.Collections.Generic;

namespace CoreWebAPIs.Models.CustomerDetailsModels;

public partial class SixFsrfbFlightcount
{
    /// <summary>
    /// /flight/leg/flightCountFlightLink/flightCount
    /// </summary>
    public DateTime? Departuredatetimeutc { get; set; }

    /// <summary>
    /// /flight/leg/flightCountFlightLink/flightCount/flightCountBreakdown
    /// </summary>
    public string? Countattribute { get; set; }

    /// <summary>
    /// /flight/leg/flightCountFlightLink/flightCount/flightCountBreakdown
    /// </summary>
    public decimal? Boarded { get; set; }

    /// <summary>
    /// /flight/leg/flightCountFlightLink/flightCount/flightCountBreakdown
    /// </summary>
    public decimal? Booked { get; set; }

    /// <summary>
    /// /flight/leg/flightCountFlightLink/flightCount
    /// </summary>
    public decimal? FlightCountStatus { get; set; }

    /// <summary>
    /// /flight/leg/flightCountFlightLink/flightCount/flightCountBreakdown
    /// </summary>
    public decimal? BreakdownStatus { get; set; }

    /// <summary>
    /// /flight
    /// </summary>
    public decimal? Flightnumber { get; set; }

    public string? FileId { get; set; }

    public DateTime? DateSent { get; set; }
}
