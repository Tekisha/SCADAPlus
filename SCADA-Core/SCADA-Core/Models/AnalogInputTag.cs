using System.ComponentModel.DataAnnotations;

namespace SCADA_Core.Models;

public class AnalogInputTag : InputTag
{
    [Required] public string Units { get; set; }

    [Required] public bool Alarms { get; set; }
}