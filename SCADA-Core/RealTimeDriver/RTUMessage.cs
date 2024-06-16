using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealTimeDriver
{
    public class RTUMessage
    {
        public double Value { get; set; }
        public string Address { get; set; }
        public string Message { get => Value.ToString() + ":" + Address; }
        public byte[] Signature { get; set; }

        public RTUMessage(double value, byte[] signature)
        {
            Value = value;
            Signature = signature;
        }

    }
}
