using System.Runtime.Serialization;

namespace RealTimeUnitService;

[DataContract]
public class RTUMessage
{
    public RTUMessage(double value, string address)
    {
        Value = value;
        Address = address;
        Signature = new byte[1];
        Message = Value + ":" + Address;
    }

    [DataMember] public double Value { get; set; }

    [DataMember] public string Address { get; set; }

    [DataMember] public byte[] Signature { get; set; }

    [DataMember] public string Message { get; private set; }
}