using System.Runtime.Serialization;

namespace SCADA_Core.DTOs;

[DataContract]
public class BaseTagInfoDto
{
    [DataMember] public string Id { get; set; }
    [DataMember] public string Description { get; set; }

    public override string ToString()
    {
        return $"Id: {Id}, Description: {Description}";
    }
}