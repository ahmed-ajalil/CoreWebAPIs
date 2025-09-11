using System;
using System.Collections.Generic;

namespace CoreWebAPIs.Models.CustomerDetailsModels;

public partial class VwPaxManufest
{
    public decimal? Fltseqnr { get; set; }

    public DateTime? Fltdate { get; set; }

    public string? TktNr { get; set; }

    public string? Fltnr { get; set; }

    public string? Org { get; set; }

    public string? Dest { get; set; }

    public string? PaxOrigin { get; set; }

    public string? PaxDestination { get; set; }

    public string? Name { get; set; }

    public string? Sex { get; set; }

    public string? SeatNo { get; set; }

    public string? PaxClass { get; set; }

    public string? Status { get; set; }

    public string? TlxPart { get; set; }
}
