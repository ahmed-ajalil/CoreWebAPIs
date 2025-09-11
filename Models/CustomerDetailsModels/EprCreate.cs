using System;
using System.Collections.Generic;

namespace CoreWebAPIs.Models.CustomerDetailsModels;

public partial class EprCreate
{
    public string? Epr { get; set; }

    public string? Dty { get; set; }

    public string? Name { get; set; }

    public string? City { get; set; }

    public string? Sine { get; set; }
}
