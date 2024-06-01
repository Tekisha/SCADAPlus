using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCADA_Core.Models
{
    public class DigitalOutputTag : Tag
    {
        [Required]
        public double InitialValue { get; set; }
    }
}
