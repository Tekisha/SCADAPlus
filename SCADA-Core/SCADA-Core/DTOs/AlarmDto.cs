﻿using System;
using SCADA_Core.Enums;
using SCADA_Core.Models;

namespace SCADA_Core.DTOs;

public class AlarmDto
{
    public string AlarmName { get; set; }
    public string TagId { get; set; }
    public double Limit { get; set; }
    public string Type { get; set; } // "ABOVE" or "BELOW"
    public AlarmPriority Priority { get; set; }
    public bool Acknowledged { get; set; }

    public Alarm ToEntity()
    {
        return new Alarm
        {
            AlarmName = AlarmName,
            TagId = TagId,
            Limit = Limit,
            Type = Type == "ABOVE" ? AlarmType.Above : AlarmType.Below,
            Priority = Priority,
            Acknowledged = Acknowledged
        };
    }

    public Alarm ToTriggered()
    {
        return new Alarm
        {
            Id = Guid.NewGuid().ToString(),
            AlarmName = AlarmName,
            TagId = TagId,
            Limit = Limit,
            Type = Type == "ABOVE" ? AlarmType.Above : AlarmType.Below,
            Priority = Priority,
            Acknowledged = Acknowledged
        };
    }
}