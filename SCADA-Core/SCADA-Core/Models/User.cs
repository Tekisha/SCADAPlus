﻿using System.ComponentModel.DataAnnotations;

namespace SCADA_Core.Models;

public class User
{
    [Key] [Required] [MaxLength(30)] public string Username { get; set; }

    [Required] [MaxLength(30)] public string Password { get; set; }
}