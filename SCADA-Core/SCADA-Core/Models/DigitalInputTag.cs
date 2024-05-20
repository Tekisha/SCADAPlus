using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScadaPlus.Models
{
    public class DigitalInputTag : Tag
    {
        public string Driver { get; set; }
        public int ScanTime { get; set; }
        public bool OnOffScan { get; set; }
    }
}
