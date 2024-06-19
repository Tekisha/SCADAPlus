using System.Runtime.Serialization;
using SCADA_Core.Models;

namespace SCADA_Core.DTOs;

[DataContract]
public class TriggeredAlarmDto
{
    [DataMember] public Alarm Alarm { get; set; }

    [DataMember] public string TagDescription { get; set; }
}