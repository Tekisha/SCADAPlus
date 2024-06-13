using System.Runtime.Serialization;

namespace SCADA_Core.DTOs;

[DataContract]
public class TagDto
{
    [DataMember] public string Id { get; set; }

    [DataMember] public string Description { get; set; }

    [DataMember] public string IoAddress { get; set; }

    [DataMember] public string Driver { get; set; }

    [DataMember] public int ScanTime { get; set; }

    [DataMember] public bool OnOffScan { get; set; }

    [DataMember] public double LowLimit { get; set; }

    [DataMember] public double HighLimit { get; set; }

    [DataMember] public string Units { get; set; }

    [DataMember] public bool Alarms { get; set; }
}