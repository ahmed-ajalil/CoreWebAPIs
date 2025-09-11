using System;
using System.Collections.Generic;

namespace CoreWebAPIs.Models.CustomerDetailsModels;

public partial class AcsiGwlsLdm
{
    public int? FltSeqNr { get; set; }

    public string? Destination { get; set; }

    public byte? Male { get; set; }

    public byte? Female { get; set; }

    public byte? Child { get; set; }

    public byte? Infant { get; set; }

    public string? Filler { get; set; }
}
