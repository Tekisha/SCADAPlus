using System.ComponentModel.DataAnnotations;

namespace SCADA_Core.Models;

public class DigitalOutputTag : Tag
{
    [Required] public double InitialValue { get; set; }
}