using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using SCADA_Core.DTOs;
using SCADA_Core.Models;
using SCADA_Core.Repositories.interfaces;
using SCADA_Core.Services.interfaces;
using SCADA_Core.Utilities;

namespace SCADA_Core.Services.implementations;

public class AlarmService : IAlarmService
{
    public delegate void AlarmTriggered(Alarm alarm);

    private const string ConfigFilePath = "alarmConfig.xml";
    private const string LogFilePath = "alarmsLog.txt";

    private readonly IAlarmRepository _repository;
    private readonly ITagService _tagService;

    public AlarmService(IAlarmRepository repository, ITagService tagService)
    {
        _repository = repository;
        _tagService = tagService;
        LoadAlarmsFromConfig();
        tagService.Subscribe(CheckForAlarms);
    }

    public IEnumerable<AlarmDto> GetAll()
    {
        var alarms = _repository.GetAll().Result;
        return alarms.Select(a => a.ToDto());
    }

    public AlarmDto Get(string alarmName)
    {
        ValidationService.ValidateEmptyString("alarmName", alarmName);
        var alarm = _repository.Get(alarmName).Result;
        if (alarm == null) throw new ObjectNotFoundException($"Alarm with name '{alarmName}' not found.");
        return alarm.ToDto();
    }

    public IEnumerable<AlarmDto> GetInvoked(string tagName, double value)
    {
        var alarms = _repository.GetInvoked(tagName, value).Result;
        return alarms.Select(a => a.ToDto());
    }

    public AlarmDto Create(AlarmDto newAlarm)
    {
        ValidationService.ValidateAlarm(newAlarm);
        var existingAlarm = _repository.Get(newAlarm.AlarmName).Result;
        if (existingAlarm != null)
            throw new InvalidOperationException($"Alarm with name '{newAlarm.AlarmName}' already exists.");
        var tag = _tagService.GetTag(newAlarm.TagId);
        if (!(tag is AnalogInputTag analogTag))
            throw new InvalidOperationException("Alarm can be added only to analog tags.");
        if (!(newAlarm.Limit < analogTag.HighLimit && newAlarm.Limit > analogTag.LowLimit))
            throw new ArgumentException("Limit is not between high and low limit.");
        var alarm = newAlarm.ToEntity();
        var createdAlarm = _repository.Create(alarm).Result;
        SaveAlarmsToConfig();
        return createdAlarm.ToDto();
    }

    public AlarmDto Delete(string alarmName)
    {
        ValidationService.ValidateEmptyString("alarmName", alarmName);
        var alarm = _repository.Delete(alarmName).Result;
        if (alarm == null) throw new ObjectNotFoundException($"Alarm with name '{alarmName}' not found.");
        SaveAlarmsToConfig();
        return alarm.ToDto();
    }

    public AlarmDto Update(AlarmDto updatedAlarm)
    {
        ValidationService.ValidateAlarm(updatedAlarm);
        var existingAlarm = _repository.Get(updatedAlarm.AlarmName).Result;
        if (existingAlarm == null)
            throw new ObjectNotFoundException($"Alarm with name '{updatedAlarm.AlarmName}' not found.");
        var updated = _repository.Update(updatedAlarm.ToEntity()).Result;
        SaveAlarmsToConfig();
        return updated.ToDto();
    }

    public IEnumerable<AlarmDto> GetByTag(string tagId)
    {
        var alarms = _repository.GetByTagId(tagId).Result;
        return alarms.Select(a => a.ToDto());
    }

    public void HandleTriggeredAlarm(Alarm alarm)
    {
        using (var writer = new StreamWriter(LogFilePath, true))
        {
            writer.WriteLine(
                $"{alarm.Time}: {alarm.AlarmName} (Priority: {alarm.Priority}, Type: {alarm.Type}, Limit: {alarm.Limit})");
        }

        _repository.SaveTriggeredAlarm(alarm);
        OnAlarmTriggered?.Invoke(alarm);
    }

    public void Subscribe(AlarmTriggered alarmTriggeredDelegate)
    {
        OnAlarmTriggered += alarmTriggeredDelegate;
    }

    public event AlarmTriggered OnAlarmTriggered;

    private void SaveAlarmsToConfig()
    {
        var alarms = _repository.GetAll().Result;
        var xmlSerializer = new XmlSerializer(typeof(List<Alarm>));
        using var writer = new StreamWriter(ConfigFilePath);
        xmlSerializer.Serialize(writer, alarms.ToList());
    }

    private void LoadAlarmsFromConfig()
    {
        if (!File.Exists(ConfigFilePath)) return;

        var xmlSerializer = new XmlSerializer(typeof(List<Alarm>));
        using var reader = new StreamReader(ConfigFilePath);
        var alarms = (List<Alarm>)xmlSerializer.Deserialize(reader);
        foreach (var alarm in alarms) _repository.Create(alarm).Wait();
    }


    public void CheckForAlarms(TagValueChange tagValueChange)
    {
        var invoked = GetInvoked(tagValueChange.Tag.Id, tagValueChange.Value);
        foreach (var alarm in invoked.Select(dto => dto.ToTriggered()))
        {
            alarm.Time = DateTime.Now;
            HandleTriggeredAlarm(alarm);
        }
    }
}