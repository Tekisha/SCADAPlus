using System.ComponentModel.DataAnnotations;

namespace SCADA_Core.Models;

public class AnalogInputTag : Tag
{
    [Required] public string Driver { get; set; }

    [Required] public int ScanTime { get; set; }

    [Required] public bool OnOffScan { get; set; }

    [Required] public double LowLimit { get; set; }

    [Required] public double HighLimit { get; set; }

    [Required] public string Units { get; set; }

    [Required] public bool Alarms { get; set; }
}