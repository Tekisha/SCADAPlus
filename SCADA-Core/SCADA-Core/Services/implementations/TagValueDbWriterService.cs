using SCADA_Core.Models;
using SCADA_Core.Repositories.interfaces;
using SCADA_Core.Services.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCADA_Core.Services.implementations
{
    public class TagValueDbWriterService
    {
        private ITagValueRepository tagValueRepository;
        private readonly object dbLock = new object();
        public TagValueDbWriterService(ITagValueRepository tagValueRepository, ITagService tagService) {
            dbLock = new object();
            this.tagValueRepository = tagValueRepository;
            tagService.Subscribe(WriteToDb);
        }

        public void WriteToDb(TagValueChange tagValueChange)
        {
            if (tagValueChange.Value == double.NaN)
            {
                return;
            }
            lock(dbLock)
            {
                tagValueRepository.Add(tagValueChange.Tag.Id, tagValueChange.Value);
            }
        }
    }
}
