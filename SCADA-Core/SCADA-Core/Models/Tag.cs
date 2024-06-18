using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace SCADA_Core.Models;

[XmlInclude(typeof(AnalogInputTag))]
[XmlInclude(typeof(AnalogOutputTag))]
[XmlInclude(typeof(DigitalInputTag))]
[XmlInclude(typeof(DigitalOutputTag))]
public abstract class Tag
{
    [Key] [MaxLength(256)] public string Id { get; set; }

    [Required] [MaxLength(256)] public string Description { get; set; }

    [Required] [MaxLength(256)] public string IOAddress { get; set; }
}