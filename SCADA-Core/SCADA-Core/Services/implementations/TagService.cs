using System;
using System.Collections.Generic;
using SCADA_Core.Models;
using SCADA_Core.Repositories.interfaces;
using SCADA_Core.Services.interfaces;

namespace SCADA_Core.Services.implementations;

public class TagService(ITagRepository tagRepository, IAlarmService alarmService) : ITagService
{
    public double GetTagValue(string address)
    {
        var tag = tagRepository.GetTag(address);
        return tag switch
        {
            DigitalInputTag { OnOffScan: true } => SimulationDriver.SimulationDriver.GetValue(tag.IOAddress),
            AnalogInputTag { OnOffScan: true } => SimulationDriver.SimulationDriver.GetValue(tag.IOAddress),
            _ => double.NaN
        };
    }

    public void AddTag(Tag tag)
    {
        tagRepository.AddTag(tag);
    }

    public void RemoveTag(string id)
    {
        tagRepository.RemoveTag(id);
    }

    public void ChangeOutputValue(string tagId, double newValue)
    {
        var tag = tagRepository.GetTag(tagId);
        if (tag == null) return;

        if (tag is DigitalOutputTag digitalOutput)
        {
            digitalOutput.InitialValue = newValue;
            tagRepository.UpdateTag(digitalOutput);
        }
        else if (tag is AnalogOutputTag analogOutput)
        {
            analogOutput.InitialValue = newValue;
            tagRepository.UpdateTag(analogOutput);

            var alarms = alarmService.GetInvoked(tag.Id, newValue);
            foreach (var alarm in alarms)
            {
                alarmService.HandleTriggeredAlarm(alarm.ToEntity());
                Console.WriteLine($"Alarm Triggered: {alarm.AlarmName} for Tag: {tag.Id}");
            }
        }
    }

    public double GetOutputValue(string tagId)
    {
        var tag = tagRepository.GetTag(tagId) as DigitalOutputTag;
        return tag?.InitialValue ?? double.NaN;
    }

    public void TurnScanOnOff(string tagId, bool onOff)
    {
        var tag = tagRepository.GetTag(tagId);
        switch (tag)
        {
            case DigitalInputTag diTag:
                diTag.OnOffScan = onOff;
                break;
            case AnalogInputTag aiTag:
                aiTag.OnOffScan = onOff;
                break;
        }
    }

    public IEnumerable<Tag> GetAllTags()
    {
        return tagRepository.GetAllTags();
    }

    public Tag GetTag(string id)
    {
        return tagRepository.GetTag(id);
    }
}