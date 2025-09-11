using System;
using System.Collections.Generic;

namespace CoreWebAPIs.Models.CustomerDetailsModels;

public partial class SixFsrFlight
{
    public long? Dcsflightlegid { get; set; }

    public decimal? FltSeqNr { get; set; }

    public decimal? FlightFlightnumber { get; set; }

    public string? FlightOrigin { get; set; }

    public string? FlightDestination { get; set; }

    public DateTime? FlightDeparturedate { get; set; }

    public string? FlightlegOrigin { get; set; }

    public decimal? FCapacity { get; set; }

    public decimal? JCapacity { get; set; }

    public decimal? YCapacity { get; set; }

    public decimal? BookedpercabinF { get; set; }

    public decimal? BookedpercabinJ { get; set; }

    public decimal? BookedpercabinY { get; set; }

    public decimal? PadpercabinF { get; set; }

    public decimal? PadpercabinJ { get; set; }

    public decimal? PadpercabinY { get; set; }

    public string? FlightlegDestination { get; set; }

    public DateTime? Departuredatetimeutc { get; set; }

    public string FileId { get; set; } = null!;

    public DateTime? DateSent { get; set; }
}
