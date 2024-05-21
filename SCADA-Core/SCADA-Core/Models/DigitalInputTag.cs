using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCADA_Core.Models
{
    public class DigitalInputTag : Tag
    {
        public string Driver { get; set; }
        public int ScanTime { get; set; }
        public bool OnOffScan { get; set; }
    }
}
