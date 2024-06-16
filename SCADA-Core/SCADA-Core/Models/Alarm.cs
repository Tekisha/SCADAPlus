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
        public string TagName { get; set; }
        public double Limit { get; set; }
        public AlarmType Type { get; set; } 
        public AlarmPriority Priority { get; set; }
        public bool Acknowledged { get; set; }
    }

    public enum AlarmType
    {
        Above,
        Below
    }

    public enum AlarmPriority
    {
        High = 1,
        Medium = 2,
        Low = 3
    }

}
