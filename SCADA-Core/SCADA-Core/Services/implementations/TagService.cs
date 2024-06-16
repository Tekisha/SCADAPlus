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

    public delegate void TagValueChanged(TagValue tagValue);
    public event TagValueChanged OnTagValueChanged;

    public TagService(ITagRepository tagRepository, ITagValueRepository tagValueRepository)
    {
        this.tagRepository = tagRepository;
        this.tagValueRepository = tagValueRepository;
        drivers = new Dictionary<string, IDriver.IDriver> {
            {"SIM", new SimulationDriver.SimulationDriver() },
            {"RT", new RealTimeDriver.RealTimeDriver() } 
        };
        scanThreads = new();

        OnTagValueChanged += (tagValue) => { Console.WriteLine($"Tag {tagValue.TagId} changed at {tagValue.Time} to {tagValue.Value}"); };

        foreach (Tag tag in GetAllTags())
        {
            ConnectDriver(tag);
            StartScanning(tag);
        }


    }

    public double GetTagValue(string address)
    {
        var tag = tagRepository.GetTag(address);
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
        if (tag is InputTag inputTag && inputTag.OnOffScan)
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
            TagValue currentTagValue = tagValueRepository.GetNewest(tag.Id);
            double currentValue = currentTagValue != null ? currentTagValue.Value : double.NaN;
            double newValue = GetTagValue(tag.IOAddress);

            // TODO: Save value (in other class)
            TagValue newTagValue = new TagValue
            {
                Id = "",
                TagId = tag.Id,
                Tag = tag,
                Value = newValue,
                Time = DateTime.Now
            };
            OnTagValueChanged?.Invoke(newTagValue);

            tag = (InputTag) tagRepository.GetTag(tag.Id);
            if (!tag.OnOffScan)
            {
                break;
            }
            Thread.Sleep(tag.ScanTime);
        } 
    }

    public void RemoveTag(string id)
    {
        tagRepository.RemoveTag(id);
    }

    public void ChangeOutputValue(string tagId, double newValue)
    {
        if (tagRepository.GetTag(tagId) is DigitalOutputTag tag) tag.InitialValue = newValue;
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
}