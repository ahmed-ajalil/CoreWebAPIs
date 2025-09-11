using System;
using System.Collections.Generic;

namespace CoreWebAPIs.Models.CustomerDetailsModels;

public partial class SixFsrfbLeg
{
    /// <summary>
    /// /flight/leg
    /// </summary>
    public DateTime? DepartureDate { get; set; }

    /// <summary>
    /// /flight/leg
    /// </summary>
    public string? Destination { get; set; }

    /// <summary>
    /// /flight/leg
    /// </summary>
    public string? Origin { get; set; }

    /// <summary>
    /// flight/leg/legCabin
    /// </summary>
    public string? CabinCode { get; set; }

    /// <summary>
    /// /flight/leg
    /// </summary>
    public decimal? LegStatus { get; set; }

    /// <summary>
    /// /flight/leg/legCabin
    /// </summary>
    public decimal? LegCabinStatus { get; set; }

    /// <summary>
    /// /flight/leg/legCabin
    /// </summary>
    public decimal? Capacity { get; set; }

    /// <summary>
    /// /flight
    /// </summary>
    public decimal? Flightnumber { get; set; }

    public string? FileId { get; set; }

    public DateTime? DateSent { get; set; }
}
