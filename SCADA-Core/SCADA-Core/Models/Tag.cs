using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCADA_Core.Models
{
    public abstract class Tag
    {
        [Key]
        public string Id { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string IOAddress { get; set; }
    }

}
