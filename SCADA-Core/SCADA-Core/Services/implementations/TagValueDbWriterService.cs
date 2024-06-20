using SCADA_Core.Models;
using SCADA_Core.Repositories.interfaces;
using SCADA_Core.Services.interfaces;

namespace SCADA_Core.Services.implementations;

public class TagValueDbWriterService
{
    private readonly object _dbLock;
    private readonly ITagValueRepository _tagValueRepository;

    public TagValueDbWriterService(ITagValueRepository tagValueRepository, ITagService tagService)
    {
        _dbLock = new object();
        _tagValueRepository = tagValueRepository;
        tagService.Subscribe(WriteToDb);
    }

    public void WriteToDb(TagValueChange tagValueChange)
    {
        lock (_dbLock)
        {
            _tagValueRepository.Add(tagValueChange.Tag.Id, tagValueChange.Value);
        }
    }
}