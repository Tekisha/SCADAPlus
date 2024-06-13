using System.ComponentModel.DataAnnotations;

namespace SCADA_Core.Models;

public abstract class Tag
{
    [Key] public string Id { get; set; }

    [Required] public string Description { get; set; }

    [Required] public string IOAddress { get; set; }
}