using System;
using System.Collections.Generic;
using System.Linq;
using SCADA_Core.Models;
using SCADA_Core.Repositories.interfaces;

namespace SCADA_Core.Repositories.implementations;

public class TagValueRepository(ScadaDbContext dbContext) : ITagValueRepository
{
    public void Add(string tagId, double value)
    {
        var tagValue = new TagValue
        {
            Id = Guid.NewGuid().ToString(),
            TagId = tagId,
            Value = value,
            Time = DateTime.Now
        };
        dbContext.TagValues.Add(tagValue);
        dbContext.SaveChanges();
    }

    public IEnumerable<TagValue> GetAll()
    {
        return dbContext.TagValues.ToList();
    }

    public List<TagValue> GetAllTagValues(string id)
    {
        return dbContext.TagValues
            .Where(t => t.TagId == id)
            .OrderByDescending(t => t.Value)
            .ToList();
    }

    public List<TagValue> GetLatestAnalogInputTagValues()
    {
        return GetLatestTagValues()
            .Where(t => t.Tag.GetType() == typeof(AnalogInputTag))
            .ToList();
    }

    public List<TagValue> GetLatetstDigitalInputTagValues()
    {
        return GetLatestTagValues()
            .Where(t => t.Tag.GetType() == typeof(DigitalInputTag))
            .ToList();
    }

    public TagValue GetNewest(string tagId)
    {
        return dbContext.TagValues
            .Where(tagValue => tagValue.TagId == tagId)
            .OrderByDescending(tagValue => tagValue.Time)
            .FirstOrDefault();
    }

    public List<TagValue> GetTagValuesDuringInterval(DateTime start, DateTime end)
    {
        return dbContext.TagValues
            .Where(tagValue => start < tagValue.Time && tagValue.Time < end)
            .OrderByDescending(tagValue => tagValue.Time)
            .ToList();
    }

    private IEnumerable<TagValue> GetLatestTagValues()
    {
        return dbContext.TagValues
            .GroupBy(tagValue => tagValue.TagId)
            .Select(grp => grp.FirstOrDefault(tagValue => tagValue.Time.Equals(grp.Max(tv => tv.Time))))
            .OrderByDescending(t => t.Time)
            .ToList();
    }
}