using System;
using System.Collections.Generic;

namespace CoreWebAPIs.Models.CustomerDetailsModels;

public partial class SsciSumm
{
    public int? FltSeqNr { get; set; }

    public DateTime? FltDate { get; set; }

    public string? FltNr { get; set; }

    public string? Origin { get; set; }

    public string? Destination { get; set; }

    public byte? Authj { get; set; }

    public byte? Authy { get; set; }

    public byte? Bookedj { get; set; }

    public byte? Bookedy { get; set; }

    public byte? Availj { get; set; }

    public byte? Availy { get; set; }

    public byte? Thrurvj { get; set; }

    public byte? Thrurvy { get; set; }

    public byte? Lclrvj { get; set; }

    public byte? Lclrvy { get; set; }

    public byte? Lclonj { get; set; }

    public byte? Lclony { get; set; }

    public byte? Tlobj { get; set; }

    public byte? Tloby { get; set; }

    public byte? Otldj { get; set; }

    public byte? Otldy { get; set; }

    public byte? Jumpj { get; set; }

    public byte? Jumpy { get; set; }
}
