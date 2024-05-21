using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCADA_Core.Models
{
    public class AnalogInputTag : Tag
    {
        public string Driver { get; set; }
        public int ScanTime { get; set; }
        public bool OnOffScan { get; set; }
        public double LowLimit { get; set; }
        public double HighLimit { get; set; }
        public string Units { get; set; }
        public bool Alarms { get; set; }
    }
}
