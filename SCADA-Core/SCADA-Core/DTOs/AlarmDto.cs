using SCADA_Core.Enums;
using SCADA_Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCADA_Core.DTOs
{
    public class AlarmDto
    {
        public string AlarmName { get; set; }
        public string TagName { get; set; }
        public double Limit { get; set; }
        public string Type { get; set; } // "ABOVE" or "BELOW"
        public AlarmPriority Priority { get; set; }
        public bool Acknowledged { get; set; }

        public Alarm ToEntity()
        {
            return new Alarm
            {
                AlarmName = AlarmName,
                TagName = TagName,
                Limit = Limit,
                Type = Type == "ABOVE" ? AlarmType.Above : AlarmType.Below,
                Priority = Priority,
                Acknowledged = Acknowledged
            };
        }
    }
}
