using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScadaPlus.Models
{
    public abstract class Tag
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public string IOAddress { get; set; }
    }

}
