using SCADA_Core.Repositories.interfaces;
using SCADA_Core.Services.interfaces;
using SCADA_Core.Models;
using System.Collections.Generic;
using SimulationDriver;

namespace SCADA_Core.Services.implementations
{
    public class TagService : ITagService
    {
        private readonly ITagRepository tagRepository;

        public TagService(ITagRepository tagRepository)
        {
            this.tagRepository = tagRepository;
        }

        public double GetTagValue(string address)
        {
            var tag = tagRepository.GetTag(address);
            if (tag is DigitalInputTag diTag && diTag.OnOffScan)
            {
                return SimulationDriver.SimulationDriver.GetValue(tag.IOAddress);
            }
            else if (tag is AnalogInputTag aiTag && aiTag.OnOffScan)
            {
                return SimulationDriver.SimulationDriver.GetValue(tag.IOAddress);
            }
            return double.NaN;
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
            var tag = tagRepository.GetTag(tagId) as DigitalOutputTag;
            if (tag != null)
            {
                tag.InitialValue = newValue;
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
            if (tag is DigitalInputTag diTag)
            {
                diTag.OnOffScan = onOff;
            }
            else if (tag is AnalogInputTag aiTag)
            {
                aiTag.OnOffScan = onOff;
            }
        }

        public IEnumerable<Tag> GetAllTags()
        {
            return tagRepository.GetAllTags();
        }
    }
}
