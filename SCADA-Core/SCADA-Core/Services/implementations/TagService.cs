using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq.Expressions;
using System.Threading;
using IDriver;
using Org.BouncyCastle.Tls;
using RealTimeDriver;
using SCADA_Core.Models;
using SCADA_Core.Repositories.interfaces;
using SCADA_Core.Services.interfaces;

namespace SCADA_Core.Services.implementations;

public class TagService : ITagService
{

    private Dictionary<string, IDriver.IDriver> drivers = new Dictionary<string, IDriver.IDriver> {
        {"SIM", new SimulationDriver.SimulationDriver() },
        {"RT", new RealTimeDriver.RealTimeDriver() } 
    };
    private ITagRepository tagRepository;
    private ITagValueRepository tagValueRepository;
    private List<Thread> scanThreads;
    private readonly object dbLock = new object();

    public delegate void TagValueChanged(TagValueChange tagValue);
    public event TagValueChanged OnTagValueChanged;

    public TagService(ITagRepository tagRepository, ITagValueRepository tagValueRepository)
    {
        this.tagRepository = tagRepository;
        this.tagValueRepository = tagValueRepository;
        this.dbLock = new object();
        drivers = new Dictionary<string, IDriver.IDriver> {
            {"SIM", new SimulationDriver.SimulationDriver() },
            {"RT", new RealTimeDriver.RealTimeDriver() } 
        };
        scanThreads = new();

        foreach (Tag tag in GetAllTags())
        {
            ConnectDriver(tag);
            StartScanning(tag);
        }
    }

    public void Subscribe(TagValueChanged tagValueChangedDelegate)
    {
        OnTagValueChanged += tagValueChangedDelegate;
    }

    public double GetTagValue(string id)
    {
        var tag = tagRepository.GetTag(id);
        return GetTagValue(tag);
    }

    private double GetTagValue(Tag tag)
    {
        return tag switch
        {
            InputTag { OnOffScan: true } => drivers[((InputTag) tag).Driver].GetValue(tag.IOAddress),
            _ => double.NaN
        };
    }

    public void AddTag(Tag tag)
    {
        tagRepository.AddTag(tag);
        ConnectDriver(tag);
        StartScanning(tag);
    }

    private void ConnectDriver(Tag tag)
    {
        if (tag is InputTag inputTag)
        {
            drivers[inputTag.Driver].Connect(tag.IOAddress);
        }
    }
    private void StartScanning(Tag tag)
    {
        if (tag is InputTag inputTag && inputTag.OnOffScan)
        {
            Thread scanThread = new Thread(() => ScanThreadStart(inputTag));
            scanThread.Start();
            scanThreads.Add(scanThread);
        }
    }

    private void ScanThreadStart(InputTag tag)
    {
        while(tag.OnOffScan)
        {
            double newValue = GetTagValue(tag);

            TagValueChange tagValueChange = new TagValueChange
            {
                Tag = tag,
                Value = Clamp(newValue, tag.LowLimit, tag.HighLimit),
                Time = DateTime.Now
            };

            if (!double.IsNaN(newValue))
            {
                if (tag is DigitalInputTag)
                {
                    if (tagValueChange.Value < 1)
                    {
                        tagValueChange.Value = 0;
                    }
                    else
                    {
                        tagValueChange.Value = 1;
                    }
                }
                OnTagValueChanged?.Invoke(tagValueChange);
            }

            lock(dbLock)
            {
                tag = (InputTag) tagRepository.GetTag(tag.Id);
            }
            if (!tag.OnOffScan)
            {
                break;
            }
            Thread.Sleep(tag.ScanTime);
        } 
    }

    private double Clamp(double value, double min, double max)
    {
        return !double.IsNaN(value) ? Math.Min(Math.Max(min, value), max) : double.NaN;
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


            // TODO: Remove
            // Alarms should not be invoked for output tags
            /*
            var alarms = alarmService.GetInvoked(tag.Id, newValue);
            foreach (var alarm in alarms)
            {
                alarmService.HandleTriggeredAlarm(alarm.ToEntity());
                Console.WriteLine($"Alarm Triggered: {alarm.AlarmName} for Tag: {tag.Id}");
            }
            */
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
        if (tag is InputTag inTag)
        {
            inTag.OnOffScan = onOff;
            tagRepository.UpdateTag(tag);
            StartScanning(inTag);
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