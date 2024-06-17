using System;
using System.Collections.Generic;
using System.Linq;
using SCADA_Core.Controllers.interfaces;
using SCADA_Core.DTOs;
using SCADA_Core.Models;
using SCADA_Core.Services.interfaces;

namespace SCADA_Core.Controllers.implementations;

public class TagController(ITagService tagService, IUserService userService, IAlarmService alarmService) : ITagController
{
    public double GetTagValue(string address, string token)
    {
        if (!ValidateToken(token)) throw new UnauthorizedAccessException("Invalid token.");
        return tagService.GetTagValue(address);
    }

    public void AddTag(TagDto tagDto, string token)
    {
        if (!ValidateToken(token)) throw new UnauthorizedAccessException("Invalid token.");
        var newTag = new AnalogInputTag
        {
            Id = tagDto.Id,
            Description = tagDto.Description,
            IOAddress = tagDto.IoAddress,
            Driver = tagDto.Driver,
            ScanTime = tagDto.ScanTime,
            OnOffScan = tagDto.OnOffScan,
            LowLimit = tagDto.LowLimit,
            HighLimit = tagDto.HighLimit,
            Units = tagDto.Units,
            Alarms = tagDto.Alarms
        };

        tagService.AddTag(newTag);
    }

    public void RemoveTag(string id, string token)
    {
        if (!ValidateToken(token)) throw new UnauthorizedAccessException("Invalid token.");
        tagService.RemoveTag(id);
    }

    public void ChangeOutputValue(string tagId, double newValue, string token)
    {
        if (!ValidateToken(token)) throw new UnauthorizedAccessException("Invalid token.");
        tagService.ChangeOutputValue(tagId, newValue);
    }

    public double GetOutputValue(string tagId, string token)
    {
        if (!ValidateToken(token)) throw new UnauthorizedAccessException("Invalid token.");
        return tagService.GetOutputValue(tagId);
    }

    public void TurnScanOnOff(string tagId, bool onOff, string token)
    {
        if (!ValidateToken(token)) throw new UnauthorizedAccessException("Invalid token.");
        tagService.TurnScanOnOff(tagId, onOff);
    }

    public List<BaseTagInfoDto> GetAllTags(string token)
    {
        if (!ValidateToken(token)) throw new UnauthorizedAccessException("Invalid token.");
        var tags = tagService.GetAllTags();
        return tags.Select(tag => new BaseTagInfoDto
        {
            Id = tag.Id,
            Description = tag.Description
        }).ToList();
    }

    public AlarmDto GetAlarm(string alarmName, string token)
    {
        if (!ValidateToken(token)) throw new UnauthorizedAccessException("Invalid token.");
        return alarmService.Get(alarmName);
    }

    public IEnumerable<AlarmDto> GetInvokedAlarms(string tagId, double value, string token)
    {
        if (!ValidateToken(token)) throw new UnauthorizedAccessException("Invalid token.");
        return alarmService.GetInvoked(tagId, value);
    }

    public AlarmDto CreateAlarm(AlarmDto newAlarm, string token)
    {
        if (!ValidateToken(token)) throw new UnauthorizedAccessException("Invalid token.");
        return alarmService.Create(newAlarm);
    }

    public AlarmDto DeleteAlarm(string alarmName, string token)
    {
        if (!ValidateToken(token)) throw new UnauthorizedAccessException("Invalid token.");
        return alarmService.Delete(alarmName);
    }

    public AlarmDto UpdateAlarm(AlarmDto updatedAlarm, string token)
    {
        if (!ValidateToken(token)) throw new UnauthorizedAccessException("Invalid token.");
        return alarmService.Update(updatedAlarm);
    }

    public IEnumerable<AlarmDto> GetAlarmsByTag(string tagId, string token)
    {
        if (!ValidateToken(token)) throw new UnauthorizedAccessException("Invalid token.");
        return alarmService.GetByTag(tagId);
    }
    private bool ValidateToken(string token)
    {
        return userService.ValidateToken(token);
    }
}