using SCADA_Core.Controllers.interfaces;
using SCADA_Core.Repositories.implementations;
using SCADA_Core.Services.implementations;
using SCADA_Core.Services.interfaces;
using ScadaPlus.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCADA_Core.Controllers.implementations
{
    public class TagController : ITagController
    {
        private readonly ITagService tagService;

        public TagController()
        {
            this.tagService = new TagService(new TagRepository());
        }

        public double GetTagValue(string address)
        {
            return tagService.GetTagValue(address);
        }

        public void AddTag(Tag tag)
        {
            tagService.AddTag(tag);
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
