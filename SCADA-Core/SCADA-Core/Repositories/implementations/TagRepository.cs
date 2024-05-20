using SCADA_Core.Repositories.interfaces;
using ScadaPlus.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCADA_Core.Repositories.implementations
{
    public class TagRepository : ITagRepository
    {
        private Dictionary<string, Tag> tags = new Dictionary<string, Tag>();

        public void AddTag(Tag tag)
        {
            if (!tags.ContainsKey(tag.Id))
            {
                tags[tag.Id] = tag;
            }
        }

        public void RemoveTag(string tagId)
        {
            if (tags.ContainsKey(tagId))
            {
                tags.Remove(tagId);
            }
        }

        public Tag GetTag(string tagId)
        {
            tags.TryGetValue(tagId, out Tag tag);
            return tag;
        }

        public IEnumerable<Tag> GetAllTags()
        {
            return tags.Values;
        }
    }
}
