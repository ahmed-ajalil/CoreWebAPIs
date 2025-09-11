using System;
using System.Collections.Generic;

namespace CoreWebAPIs.Models.CustomerDetailsModels;

public partial class TmpFlownDay
{
    public string? Docno { get; set; }

    public string? Cpnno { get; set; }

    public string? Paxtype { get; set; }

    public string? Pnrnbr { get; set; }

    public string? Fltno { get; set; }

    public string? Flownsector { get; set; }

    public DateTime? Flowndate { get; set; }

    public string? Orgdest { get; set; }

    public string? Itineary { get; set; }

    public string? Farebasis { get; set; }

    public string? Rbd { get; set; }

    public string? Tktdcabinclass { get; set; }

    public DateTime? Saledate { get; set; }

    public string? Poscity { get; set; }

    public string? Poscntry { get; set; }

    public decimal? Proratedyq { get; set; }

    public decimal? Proratedcpnamt { get; set; }

    public decimal? Fltseqnr { get; set; }

    public string? Rficcode { get; set; }
}
