using System;
using System.Collections.Generic;

namespace CoreWebAPIs.Models.CustomerDetailsModels;

public partial class MyBidt
{
    public DateTime? TransDate { get; set; }

    public string? FltNo { get; set; }

    public string? Crs { get; set; }

    public string? PaxName { get; set; }

    public string? IataCode { get; set; }

    public string? BookClass { get; set; }

    public string? Pnr { get; set; }

    public string? Orig { get; set; }

    public string? Dest { get; set; }

    public DateTime? FltDate { get; set; }
}
