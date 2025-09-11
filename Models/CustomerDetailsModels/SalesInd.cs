using System;
using System.Collections.Generic;

namespace CoreWebAPIs.Models.CustomerDetailsModels;

public partial class SalesInd
{
    public int? FltSeqNr { get; set; }

    public DateTime? FltDate { get; set; }

    public string? FltNr { get; set; }

    public string? Origin { get; set; }

    public string? Destination { get; set; }

    public string? Name { get; set; }

    public string? TktNr { get; set; }

    public string? PaxType { get; set; }

    public string? DocNumber { get; set; }

    public string? Sex { get; set; }

    public string? SeatNo { get; set; }

    public DateTime? Adddate { get; set; }

    public string? MCls { get; set; }

    public string? CCls { get; set; }

    public string? Pos { get; set; }

    public string? Itin { get; set; }

    public string? Farebasis { get; set; }

    public string? ActualDest { get; set; }

    public string? LocalSales { get; set; }
}
