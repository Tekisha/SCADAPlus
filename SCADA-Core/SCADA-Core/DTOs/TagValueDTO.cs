using System;
using System.Runtime.Serialization;

namespace SCADA_Core.DTOs;

[DataContract]
public class TagValueDto
{
    [DataMember] public string TagId { get; set; }

    [DataMember] public string Name { get; set; }

    [DataMember] public double Value { get; set; }

    [DataMember] public DateTime Time { get; set; }
}