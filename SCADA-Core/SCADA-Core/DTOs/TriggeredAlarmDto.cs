using SCADA_Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SCADA_Core.DTOs
{
    [DataContract]
    public class TriggeredAlarmDto
    {
        [DataMember]
        public Alarm Alarm { get; set; }
        [DataMember]
        public string TagDescription { get; set; }
    }
}
