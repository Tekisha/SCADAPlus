﻿using System;
using System.Collections.Generic;
using System.Linq;
using SCADA_Core.Controllers.interfaces;
using SCADA_Core.DTOs;
using SCADA_Core.Enums;
using SCADA_Core.Models;
using SCADA_Core.Services.interfaces;

namespace SCADA_Core.Controllers.implementations;

public class ReportController(IUserService userService, IReportService reportService, ITagService tagService)
    : IReportController
{
    public List<TriggeredAlarmDto> GetAlarmsByPriority(AlarmPriority priority, string token)
    {
        if (!ValidateToken(token)) throw new UnauthorizedAccessException("Invalid token.");
        return reportService.GetAlarmsByPriority(priority)
            .Select(ToDto)
            .ToList();
    }

    public List<TriggeredAlarmDto> GetAlarmsDuringInterval(DateTime start, DateTime end, string token)
    {
        if (!ValidateToken(token)) throw new UnauthorizedAccessException("Invalid token.");
        return reportService.GetAlarmsDuringInterval(start, end)
            .Select(ToDto)
            .ToList();
    }

    public List<TagValueDto> GetAllTagValues(string id, string token)
    {
        if (!ValidateToken(token)) throw new UnauthorizedAccessException("Invalid token.");
        return reportService.GetAllTagValues(id).Select(ToDto).ToList();
    }

    public List<TagValueDto> GetLatestAnalogInputTagValues(string token)
    {
        if (!ValidateToken(token)) throw new UnauthorizedAccessException("Invalid token.");
        return reportService.GetLatestAnalogInputTagValues().Select(ToDto).ToList();
    }

    public List<TagValueDto> GetLatetstDigitalInputTagValues(string token)
    {
        if (!ValidateToken(token)) throw new UnauthorizedAccessException("Invalid token.");
        return reportService.GetLatetstDigitalInputTagValues().Select(ToDto).ToList();
    }

    public List<TagValueDto> GetTagValuesDuringInterval(DateTime start, DateTime end, string token)
    {
        if (!ValidateToken(token)) throw new UnauthorizedAccessException("Invalid token.");
        return reportService.GetTagValuesDuringInterval(start, end).Select(ToDto).ToList();
    }

    private bool ValidateToken(string token)
    {
        return userService.ValidateToken(token);
    }

    private TagValueDto ToDto(TagValue tagValue)
    {
        return new TagValueDto
        {
            TagId = tagValue.TagId,
            Name = tagService.GetTag(tagValue.TagId).Description,
            Time = tagValue.Time,
            Value = tagValue.Value
        };
    }

    private TriggeredAlarmDto ToDto(Alarm alarm)
    {
        return new TriggeredAlarmDto
        {
            Alarm = alarm,
            TagDescription = tagService.GetTag(alarm.TagId).Description
        };
    }
}