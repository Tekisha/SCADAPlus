﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SCADA_Core.Models;

public class TagValue
{
    [Key] [MaxLength(256)] public string Id { get; set; }

    [Required]
    [ForeignKey("Tag")] public string TagId { get; set; }

    public virtual Tag Tag { get; set; }

    public double Value { get; set; }
    public DateTime Time { get; set; }
}