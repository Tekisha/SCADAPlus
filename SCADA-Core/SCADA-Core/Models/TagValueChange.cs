using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCADA_Core.Models
{
    public class TagValueChange
    {
        public Tag Tag { get; set; }
        public double Value { get; set; }
        public DateTime Time { get; set; }
    }
}
