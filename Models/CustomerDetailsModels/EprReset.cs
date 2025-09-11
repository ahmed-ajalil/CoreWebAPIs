using System;
using System.Collections.Generic;

namespace CoreWebAPIs.Models.CustomerDetailsModels;

public partial class EprReset
{
    public string? Epr { get; set; }

    public string? Auth { get; set; }

    public string? Email { get; set; }
}
