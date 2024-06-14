using System.ComponentModel.DataAnnotations;

namespace SCADA_Core.Models;

public class AnalogOutputTag : Tag
{
    [Required] public double InitialValue { get; set; }

    [Required] public double LowLimit { get; set; }

    [Required] public double HighLimit { get; set; }

    [Required] public string Units { get; set; }
}