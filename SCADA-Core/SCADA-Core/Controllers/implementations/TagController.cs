using SCADA_Core.Controllers.interfaces;
using SCADA_Core.Repositories.implementations;
using SCADA_Core.Services.implementations;
using SCADA_Core.Services.interfaces;
using SCADA_Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SCADA_Core.Utilities;

namespace SCADA_Core.Controllers.implementations
{
    public class TagController : ITagController
    {
        private readonly ITagService tagService;

        public TagController(ITagService tagService)
        {
            this.tagService = tagService;
        }

        public double GetTagValue(string address)
        {
            return tagService.GetTagValue(address);
        }

        public void AddTag(string id, string description, string ioAddress, string driver, int scanTime, bool onOffScan, double lowLimit, double highLimit, string units, bool alarms)
        {
            var newTag = new AnalogInputTag
            {
                Id = id,
                Description = description,
                IOAddress = ioAddress,
                Driver = driver,
                ScanTime = scanTime,
                OnOffScan = onOffScan,
                LowLimit = lowLimit,
                HighLimit = highLimit,
                Units = units,
                Alarms = alarms
            };
            tagService.AddTag(newTag);
        }

        public void RemoveTag(string id)
        {
            tagService.RemoveTag(id);
        }

        public void ChangeOutputValue(string tagId, double newValue)
        {
            tagService.ChangeOutputValue(tagId, newValue);
        }

        public double GetOutputValue(string tagId)
        {
            return tagService.GetOutputValue(tagId);
        }

        public void TurnScanOnOff(string tagId, bool onOff)
        {
            tagService.TurnScanOnOff(tagId, onOff);
        }
    }
}
