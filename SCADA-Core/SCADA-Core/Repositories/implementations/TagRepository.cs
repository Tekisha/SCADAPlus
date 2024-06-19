using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SCADA_Core.Models;
using SCADA_Core.Repositories.interfaces;
using SCADA_Core.Utilities;
using static SCADA_Core.Utilities.ConfigManager;

namespace SCADA_Core.Repositories.implementations;

public class TagRepository : ITagRepository
{
    private ScadaDbContext dbContext;
    private readonly List<Tag> tags;

    public TagRepository(ScadaDbContext dbContext)
    {
        this.dbContext = dbContext;
        ConfigData config = ConfigManager.LoadConfig();
        tags = config.Tags;
    }

    public void AddTag(Tag tag)
    {
        if (dbContext.Tags.Any(t => t.Id == tag.Id)) return;
        dbContext.Tags.Add(tag);
        tags.Add(tag);
        dbContext.SaveChanges();
        SaveConfig();
    }

    public void RemoveTag(string tagId)
    {
        var tag = dbContext.Tags.FirstOrDefault(t => t.Id == tagId);
        if (tag == null) return;
        dbContext.Tags.Remove(tag);
        tags.RemoveAt(tags.FindIndex(tag => tag.Id == tagId));
        dbContext.SaveChanges();
        SaveConfig();
    }

    public Tag GetTag(string tagId)
    {
        return tags.FirstOrDefault(tag => tag.Id == tagId);
    }

    public IEnumerable<Tag> GetAllTags()
    {
        return tags;
    }

    public void UpdateTag(Tag tag)
    {
        var existingTag = dbContext.Tags.FirstOrDefault(t => t.Id == tag.Id);
        if (existingTag != null)
        {
            dbContext.Entry(existingTag).CurrentValues.SetValues(tag);
            tags[tags.FindIndex(t => t.Id == tag.Id)] = tag;
            dbContext.SaveChanges();
            SaveConfig();
        }
    }

    public void SaveConfig()
    {
        ConfigData config = ConfigManager.LoadConfig();
        config.Tags = GetAllTags().ToList();
        ConfigManager.SaveConfig(config);
    }
}