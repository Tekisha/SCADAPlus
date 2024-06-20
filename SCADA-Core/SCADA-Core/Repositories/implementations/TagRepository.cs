using System.Collections.Generic;
using System.Linq;
using SCADA_Core.Models;
using SCADA_Core.Repositories.interfaces;
using SCADA_Core.Utilities;
using static SCADA_Core.Utilities.ConfigManager;

namespace SCADA_Core.Repositories.implementations;

public class TagRepository : ITagRepository
{
    private readonly ScadaDbContext _dbContext;
    private readonly List<Tag> _tags;

    public TagRepository(ScadaDbContext dbContext)
    {
        _dbContext = dbContext;
        var config = LoadConfig();
        _tags = config.Tags;
    }

    public void AddTag(Tag tag)
    {
        if (_dbContext.Tags.Any(t => t.Id == tag.Id)) return;
        _dbContext.Tags.Add(tag);
        _tags.Add(tag);
        _dbContext.SaveChanges();
        SaveConfig();
    }

    public void RemoveTag(string tagId)
    {
        var tag = _dbContext.Tags.FirstOrDefault(t => t.Id == tagId);
        if (tag == null) return;
        _dbContext.Tags.Remove(tag);
        _tags.RemoveAt(_tags.FindIndex(t => t.Id == tagId));
        _dbContext.SaveChanges();
        SaveConfig();
    }

    public Tag GetTag(string tagId)
    {
        return _tags.FirstOrDefault(tag => tag.Id == tagId);
    }

    public IEnumerable<Tag> GetAllTags()
    {
        return _tags;
    }

    public void UpdateTag(Tag tag)
    {
        var existingTag = _dbContext.Tags.FirstOrDefault(t => t.Id == tag.Id);
        if (existingTag == null) return;
        {
            _dbContext.Entry(existingTag).CurrentValues.SetValues(tag);
            _tags[_tags.FindIndex(t => t.Id == tag.Id)] = tag;
            _dbContext.SaveChanges();
            SaveConfig();
        }
    }

    public void SaveConfig()
    {
        var config = LoadConfig();
        config.Tags = GetAllTags().ToList();
        ConfigManager.SaveConfig(config);
    }
}