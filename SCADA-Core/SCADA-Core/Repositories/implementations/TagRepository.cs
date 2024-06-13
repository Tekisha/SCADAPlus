using System.Collections.Generic;
using System.Linq;
using SCADA_Core.Models;
using SCADA_Core.Repositories.interfaces;

namespace SCADA_Core.Repositories.implementations;

public class TagRepository : ITagRepository
{
    private readonly ScadaDbContext dbContext;

    public TagRepository(ScadaDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public void AddTag(Tag tag)
    {
        if (!dbContext.Tags.Any(t => t.Id == tag.Id))
        {
            dbContext.Tags.Add(tag);
            dbContext.SaveChanges();
        }
    }

    public void RemoveTag(string tagId)
    {
        var tag = dbContext.Tags.FirstOrDefault(t => t.Id == tagId);
        if (tag != null)
        {
            dbContext.Tags.Remove(tag);
            dbContext.SaveChanges();
        }
    }

    public Tag GetTag(string tagId)
    {
        return dbContext.Tags.FirstOrDefault(t => t.Id == tagId);
    }

    public IEnumerable<Tag> GetAllTags()
    {
        return dbContext.Tags.ToList();
    }
}