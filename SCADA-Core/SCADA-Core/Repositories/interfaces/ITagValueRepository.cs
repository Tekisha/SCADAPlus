using System;
using System.Collections.Generic;
using SCADA_Core.Models;

namespace SCADA_Core.Repositories.interfaces;

public interface ITagValueRepository
{
    public void Add(string tagId, double value);
    public IEnumerable<TagValue> GetAll();
    public TagValue GetNewest(string tagId);
    List<TagValue> GetTagValuesDuringInterval(DateTime start, DateTime end);
    List<TagValue> GetLatestAnalogInputTagValues();
    List<TagValue> GetLatetstDigitalInputTagValues();
    List<TagValue> GetAllTagValues(string id);
}