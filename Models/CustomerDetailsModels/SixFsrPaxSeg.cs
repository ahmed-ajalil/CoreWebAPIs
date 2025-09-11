using System;
using System.Collections.Generic;

namespace CoreWebAPIs.Models.CustomerDetailsModels;

public partial class SixFsrPaxSeg
{
    public decimal? Dcspaxsegmentid { get; set; }

    public string? Bookingclass { get; set; }

    public string? Origin { get; set; }

    public decimal? Dcspaxid { get; set; }

    public string? FileId { get; set; }

    public DateTime? DateSent { get; set; }
}
