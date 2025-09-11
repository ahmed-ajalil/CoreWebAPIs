using System;
using System.Collections.Generic;

namespace CoreWebAPIs.Models.CustomerDetailsModels;

public partial class SixFsrPaxSer
{
    public decimal? Dcspaxserviceid { get; set; }

    public string? Servicecode { get; set; }

    public string? Servicetype { get; set; }

    public decimal? Dcspaxid { get; set; }

    public string? FileId { get; set; }

    public DateTime? DateSent { get; set; }
}
