using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CoreWebAPIs.Models;

public partial class AirportCode
{
    [Key]
    public int Id { get; set; }

    public string? Country { get; set; }

    public string? City { get; set; }

    public string? Code { get; set; }

    public string? AirportName { get; set; }
}
