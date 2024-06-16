using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq.Expressions;
using IDriver;
using Org.BouncyCastle.Tls;
using SCADA_Core.Models;
using SCADA_Core.Repositories.interfaces;
using SCADA_Core.Services.interfaces;

namespace SCADA_Core.Services.implementations;

public class TagService(ITagRepository tagRepository) : ITagService
{

    private Dictionary<string, IDriver.IDriver> drivers = new Dictionary<string, IDriver.IDriver> {
        {"SIM", new SimulationDriver.SimulationDriver() },
        {"RT", new SimulationDriver.SimulationDriver() } 
    };

    public double GetTagValue(string address)
    {
        var tag = tagRepository.GetTag(address);
        return tag switch
        {
            DigitalInputTag { OnOffScan: true } => drivers[((DigitalInputTag)tag).Driver].GetValue(tag.IOAddress),
            AnalogInputTag { OnOffScan: true } => drivers[((AnalogInputTag)tag).Driver].GetValue(tag.IOAddress),
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