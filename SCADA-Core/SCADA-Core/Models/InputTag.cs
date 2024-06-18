using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCADA_Core.Models
{
    public abstract class InputTag: Tag
    {
        [Required] public double LowLimit { get; set; }

        [Required] public double HighLimit { get; set; }

        [Required] public string Driver { get; set; }

        [Required] public int ScanTime { get; set; }

        [Required] public bool OnOffScan { get; set; }
    }
}
