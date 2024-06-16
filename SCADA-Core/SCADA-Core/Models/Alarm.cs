using SCADA_Core.DTOs;
using SCADA_Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCADA_Core.Models
{
    public class Alarm
    {
        public string Id { get; set; }
        public string AlarmName { get; set; }
        public string TagId { get; set; }
        public double Limit { get; set; }
        public AlarmType Type { get; set; } 
        public AlarmPriority Priority { get; set; }
        public bool Acknowledged { get; set; }

        public AlarmDto ToDto()
        {
            return new AlarmDto
            {
                AlarmName = this.AlarmName,
                TagId = this.TagId,
                Limit = this.Limit,
                Type = this.Type == AlarmType.Above ? "ABOVE" : "BELOW",
                Priority = this.Priority,
                Acknowledged = this.Acknowledged
            };
        }
    }
}
