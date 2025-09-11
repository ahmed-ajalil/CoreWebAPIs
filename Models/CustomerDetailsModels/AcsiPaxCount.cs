using System;
using System.Collections.Generic;

namespace CoreWebAPIs.Models.CustomerDetailsModels;

public partial class AcsiPaxCount
{
    public int? FltSeqNr { get; set; }

    public string? Class { get; set; }

    public byte? Male { get; set; }

    public byte? Female { get; set; }

    public byte? Child { get; set; }

    public byte? Infant { get; set; }

    public short? BagPcs { get; set; }

    public int? BagWt { get; set; }
}
