using System;
using System.Collections.Generic;

namespace CoreWebAPIs.Models.CustomerDetailsModels;

public partial class SixFsrfbFlight
{
    /// <summary>
    /// /flight
    /// </summary>
    public DateTime? Departuredate { get; set; }

    /// <summary>
    /// /flight
    /// </summary>
    public string? Destination { get; set; }

    /// <summary>
    /// /flight
    /// </summary>
    public string? Flightnumber { get; set; }

    /// <summary>
    /// /flight
    /// </summary>
    public string? Origin { get; set; }

    /// <summary>
    /// /flight
    /// </summary>
    public decimal? Status { get; set; }

    public string? FileId { get; set; }

    public DateTime? DateSent { get; set; }
}
