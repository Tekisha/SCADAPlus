using System.Collections.Generic;
using SCADA_Core.Models;
using static SCADA_Core.Services.implementations.TagService;

namespace SCADA_Core.Services.interfaces;

public interface ITagService
{
    double GetTagValue(string address);
    void AddTag(Tag tag);
    void RemoveTag(string id);
    void ChangeOutputValue(string tagId, double newValue);
    double GetOutputValue(string tagId);
    void TurnScanOnOff(string tagId, bool onOff);
    IEnumerable<Tag> GetAllTags();
    public void Subscribe(TagValueChanged tagValueChangedDelegate);
}