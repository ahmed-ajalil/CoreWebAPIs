using System;
using System.Collections.Generic;

namespace CoreWebAPIs.Models.CustomerDetailsModels;

public partial class ExportTable
{
    public string? SeatNo { get; set; }

    public string? Sex { get; set; }

    public string? Name { get; set; }

    public string? DocNumber { get; set; }

    public int? FltSeqNr { get; set; }

    public string? MCls { get; set; }

    public string? CCls { get; set; }

    public string? TktNr { get; set; }

    public string? Status { get; set; }

    public string? Destination { get; set; }

    public string? PaxType { get; set; }

    public string? Adddate { get; set; }
}
