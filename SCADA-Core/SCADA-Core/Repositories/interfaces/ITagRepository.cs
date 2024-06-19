using System.Collections.Generic;
using System.Data.Entity;
using SCADA_Core.Models;

namespace SCADA_Core.Repositories.interfaces;

public interface ITagRepository
{
    void AddTag(Tag tag);
    void RemoveTag(string tagId);
    Tag GetTag(string tagId);
    IEnumerable<Tag> GetAllTags();
    void UpdateTag(Tag tag);
}