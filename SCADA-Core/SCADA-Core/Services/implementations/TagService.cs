using System;
using System.Collections.Generic;
using System.Threading;
using SCADA_Core.Models;
using SCADA_Core.Repositories.interfaces;
using SCADA_Core.Services.interfaces;

namespace SCADA_Core.Services.implementations;

public class TagService : ITagService
{
    public delegate void TagValueChanged(TagValueChange tagValue);

    private readonly object _dbLock;

    private readonly Dictionary<string, IDriver.IDriver> _drivers;

    private readonly ITagRepository _tagRepository;

    public TagService(ITagRepository tagRepository)
    {
        _tagRepository = tagRepository;
        _dbLock = new object();
        _drivers = new Dictionary<string, IDriver.IDriver>
        {
            { "SIM", new SimulationDriver.SimulationDriver() },
            { "RT", new RealTimeDriver.RealTimeDriver() }
        };

        foreach (var tag in GetAllTags())
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
        var tag = _tagRepository.GetTag(id);
        return GetTagValue(tag);
    }

    public void AddTag(Tag tag)
    {
        _tagRepository.AddTag(tag);
        ConnectDriver(tag);
        StartScanning(tag);
    }

    public void RemoveTag(string id)
    {
        _tagRepository.RemoveTag(id);
    }

    public void ChangeOutputValue(string tagId, double newValue)
    {
        var tag = _tagRepository.GetTag(tagId);
        switch (tag)
        {
            case null:
                return;
            case DigitalOutputTag digitalOutput:
                digitalOutput.InitialValue = newValue;
                _tagRepository.UpdateTag(digitalOutput);
                break;
            case AnalogOutputTag analogOutput:
                analogOutput.InitialValue = newValue;
                _tagRepository.UpdateTag(analogOutput);
                break;
        }
    }

    public double GetOutputValue(string tagId)
    {
        var tag = _tagRepository.GetTag(tagId) as DigitalOutputTag;
        return tag?.InitialValue ?? double.NaN;
    }

    public void TurnScanOnOff(string tagId, bool onOff)
    {
        var tag = _tagRepository.GetTag(tagId);
        if (tag is not InputTag inTag) return;
        inTag.OnOffScan = onOff;
        _tagRepository.UpdateTag(tag);
        StartScanning(inTag);
    }

    public IEnumerable<Tag> GetAllTags()
    {
        return _tagRepository.GetAllTags();
    }

    public Tag GetTag(string id)
    {
        return _tagRepository.GetTag(id);
    }

    public event TagValueChanged OnTagValueChanged;

    private double GetTagValue(Tag tag)
    {
        return tag switch
        {
            InputTag { OnOffScan: true } inputTag => _drivers[inputTag.Driver].GetValue(inputTag.IOAddress),
            _ => double.NaN
        };
    }

    private void ConnectDriver(Tag tag)
    {
        if (tag is InputTag inputTag) _drivers[inputTag.Driver].Connect(tag.IOAddress);
    }

    private void StartScanning(Tag tag)
    {
        if (tag is not InputTag { OnOffScan: true } inputTag) return;
        var scanThread = new Thread(() => ScanThreadStart(inputTag));
        scanThread.Start();
    }

    private void ScanThreadStart(InputTag tag)
    {
        while (tag.OnOffScan)
        {
            var newValue = GetTagValue(tag);

            var tagValueChange = new TagValueChange
            {
                Tag = tag,
                Value = tag is AnalogInputTag aiTag ? Clamp(newValue, aiTag.LowLimit, aiTag.HighLimit) : newValue,
                Time = DateTime.Now
            };

            if (!double.IsNaN(newValue))
            {
                if (tag is DigitalInputTag) tagValueChange.Value = tagValueChange.Value < 1 ? 0 : 1;

                OnTagValueChanged?.Invoke(tagValueChange);
            }

            lock (_dbLock)
            {
                tag = (InputTag)_tagRepository.GetTag(tag.Id);
            }

            if (!tag.OnOffScan) break;
            Thread.Sleep(tag.ScanTime);
        }
    }

    private static double Clamp(double value, double min, double max)
    {
        return !double.IsNaN(value) ? Math.Min(Math.Max(min, value), max) : double.NaN;
    }
}