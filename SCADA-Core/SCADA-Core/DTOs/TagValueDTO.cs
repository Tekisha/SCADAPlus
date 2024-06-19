using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SCADA_Core.DTOs
{
    [DataContract]
    public class TagValueDTO
    {
        [DataMember]
        public string TagId { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public double Value { get; set; }
        [DataMember]
        public DateTime Time { get; set; }
    }
}
