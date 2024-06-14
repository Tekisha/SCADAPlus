using System.ComponentModel.DataAnnotations;

namespace SCADA_Core.Models;

public abstract class Tag
{
    [Key] [MaxLength(256)] public string Id { get; set; }

    [Required] [MaxLength(256)] public string Description { get; set; }

    [Required] [MaxLength(256)] public string IOAddress { get; set; }
}