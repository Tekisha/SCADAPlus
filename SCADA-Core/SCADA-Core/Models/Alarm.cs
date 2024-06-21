using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using SCADA_Core.DTOs;
using SCADA_Core.Enums;

namespace SCADA_Core.Models;

[DataContract]
public class Alarm
{
    [DataMember]
    [Key]
    [Required]
    [MaxLength(256)]
    public string Id { get; set; }

    [DataMember]
    [Required]
    [MaxLength(256)]
    public string AlarmName { get; set; }

    [DataMember]
    [Required]
    [MaxLength(256)]
    public string TagId { get; set; }

    [DataMember] [Required] public double Limit { get; set; }

    [DataMember] [Required] public AlarmType Type { get; set; }

    [DataMember] [Required] public AlarmPriority Priority { get; set; }

    [DataMember] [Required] public bool Acknowledged { get; set; }

    [DataMember] public DateTime Time { get; set; }

    public AlarmDto ToDto()
    {
        return new AlarmDto
        {
            AlarmName = AlarmName,
            TagId = TagId,
            Limit = Limit,
            Type = Type == AlarmType.Above ? "ABOVE" : "BELOW",
            Priority = Priority,
            Acknowledged = Acknowledged
        };
    }
}