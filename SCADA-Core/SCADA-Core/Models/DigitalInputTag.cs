using System.ComponentModel.DataAnnotations;

namespace SCADA_Core.Models;

public class DigitalInputTag : InputTag
{
    [Required] public string Driver { get; set; }

    [Required] public int ScanTime { get; set; }

    [Required] public bool OnOffScan { get; set; }
}