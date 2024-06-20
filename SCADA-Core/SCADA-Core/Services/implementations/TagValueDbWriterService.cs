using SCADA_Core.Models;
using SCADA_Core.Repositories.interfaces;
using SCADA_Core.Services.interfaces;

namespace SCADA_Core.Services.implementations;

public class TagValueDbWriterService
{
    private readonly object _dbLock;
    private readonly ITagValueRepository _tagValueRepository;
    private readonly ITagService _tagService;

    public TagValueDbWriterService(ITagValueRepository tagValueRepository, ITagService tagService)
    {
        _dbLock = new object();
        _tagValueRepository = tagValueRepository;
        _tagService = tagService;
        tagService.Subscribe(WriteToDb);
    }

    public void WriteToDb(TagValueChange tagValueChange)
    {
        if (_tagService.GetTag(tagValueChange.Tag.Id) == null) return;
        lock (_dbLock)
        {
            _tagValueRepository.Add(tagValueChange.Tag.Id, tagValueChange.Value);
        }
    }
}