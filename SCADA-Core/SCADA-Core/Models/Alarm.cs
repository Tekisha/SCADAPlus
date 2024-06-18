using SCADA_Core.DTOs;
using SCADA_Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SCADA_Core.Models
{
    [DataContract]
    public class Alarm
    {
        [DataMember]
        [Key]
        [Required]
        public string Id { get; set; }
        [DataMember]
        [Required]
        [MaxLength(256)]
        public string AlarmName { get; set; }
        [DataMember]
        [Required]
        [MaxLength(256)]
        public string TagId { get; set; }
        [DataMember]
        [Required]
        public double Limit { get; set; }
        [DataMember]
        [Required]
        public AlarmType Type { get; set; }
        [DataMember]
        [Required]
        public AlarmPriority Priority { get; set; }
        [DataMember]
        [Required]
        public bool Acknowledged { get; set; }
        [DataMember]
        public DateTime Time { get; set; }

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
