using SCADA_Core.DTOs;
using SCADA_Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCADA_Core.Models
{
    public class Alarm
    {
        [Key]
        [Required]
        public string Id { get; set; }
        [Required]
        [MaxLength(256)]
        public string AlarmName { get; set; }
        [Required]
        [MaxLength(256)]
        public string TagId { get; set; }
        [Required]
        public double Limit { get; set; }
        [Required]
        public AlarmType Type { get; set; }
        [Required]
        public AlarmPriority Priority { get; set; }
        [Required]
        public bool Acknowledged { get; set; }

        public AlarmDto ToDto()
        {
            return new AlarmDto
            {
                AlarmName = this.AlarmName,
                TagId = this.TagId,
                Limit = this.Limit,
                Type = this.Type == AlarmType.Above ? "ABOVE" : "BELOW",
                Priority = this.Priority,
                Acknowledged = this.Acknowledged
            };
        }
    }
}
