using SCADA_Core.Models;
using SCADA_Core.Repositories.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCADA_Core.Repositories.implementations
{
    public class TagValueRepository(ScadaDbContext dbContext) : ITagValueRepository
    {
        public void Add(string tagId, double value)
        {
            TagValue tagValue = new TagValue
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

        public TagValue GetNewest(string tagId)
        {
            return dbContext.TagValues
                .Where(tagValue => tagValue.TagId == tagId)
                .OrderByDescending(tagValue => tagValue.Time)
                .FirstOrDefault();
        }
    }
}
