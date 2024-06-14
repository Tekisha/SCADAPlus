using System.Collections.Generic;
using System.Linq;
using SCADA_Core.Controllers.interfaces;
using SCADA_Core.DTOs;
using SCADA_Core.Models;
using SCADA_Core.Services.interfaces;

namespace SCADA_Core.Controllers.implementations;

public class TagController(ITagService tagService) : ITagController
{
    public double GetTagValue(string address)
    {
        return tagService.GetTagValue(address);
    }

    public void AddTag(TagDto tagDto)
    {
        var newTag = new AnalogInputTag
        {
            Id = tagDto.Id,
            Description = tagDto.Description,
            IOAddress = tagDto.IoAddress,
            Driver = tagDto.Driver,
            ScanTime = tagDto.ScanTime,
            OnOffScan = tagDto.OnOffScan,
            LowLimit = tagDto.LowLimit,
            HighLimit = tagDto.HighLimit,
            Units = tagDto.Units,
            Alarms = tagDto.Alarms
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

    public List<BaseTagInfoDto> GetAllTags()
    {
        var tags = tagService.GetAllTags();
        return tags.Select(tag => new BaseTagInfoDto
        {
            Id = tag.Id,
            Description = tag.Description
        }).ToList();
    }
}