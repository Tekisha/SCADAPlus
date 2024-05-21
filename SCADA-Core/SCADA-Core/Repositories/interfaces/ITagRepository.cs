using SCADA_Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCADA_Core.Repositories.interfaces
{
    public interface ITagRepository
    {
        void AddTag(Tag tag);
        void RemoveTag(string tagId);
        Tag GetTag(string tagId);
        IEnumerable<Tag> GetAllTags();
    }
}
