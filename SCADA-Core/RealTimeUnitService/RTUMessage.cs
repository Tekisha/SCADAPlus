using System.Runtime.Serialization;

namespace RealTimeDriver
{
    [DataContract]
    public class RTUMessage
    {
        [DataMember]
        public double Value { get; set; }
        [DataMember]
        public string Address { get; set; }
        [DataMember]
        public byte[] Signature { get; set; }
        public string Message { get => Value + ":" + Address; }

        public RTUMessage(double value, string address)
        {
            Value = value;
            Address = address;
            Signature = new byte[1];
        }
    }
}