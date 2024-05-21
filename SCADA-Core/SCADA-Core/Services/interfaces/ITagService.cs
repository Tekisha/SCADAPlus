using SCADA_Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace SCADA_Core.Services.interfaces
{
    public interface ITagService
    {
        double GetTagValue(string address);
        void AddTag(Tag tag);
        void RemoveTag(string id);
        void ChangeOutputValue(string tagId, double newValue);
        double GetOutputValue(string tagId);
        void TurnScanOnOff(string tagId, bool onOff);
        IEnumerable<Tag> GetAllTags();
    }
}
