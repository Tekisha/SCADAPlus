using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCADA_Core.Models
{
    public class DigitalInputTag : Tag
    {
        [Required]
        public string Driver { get; set; }

        [Required]
        public int ScanTime { get; set; }

        [Required]
        public bool OnOffScan { get; set; }
    }
}
