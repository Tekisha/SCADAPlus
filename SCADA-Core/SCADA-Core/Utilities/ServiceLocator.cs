using SCADA_Core.Repositories;
using SCADA_Core.Repositories.implementations;
using SCADA_Core.Services.implementations;

namespace SCADA_Core.Utilities;

public static class ServiceLocator
{
    private static readonly ScadaDbContext dbContext;
    private static readonly TagValueRepository tagValueRepository;
    private static readonly TagRepository tagRepository;
    private static readonly UserRepository userRepository;
    private static readonly TagService tagService;
    private static readonly UserService userService;

    static ServiceLocator()
    {
        dbContext = new ScadaDbContext();
        tagRepository = new TagRepository(dbContext);
        userRepository = new UserRepository(dbContext);
        tagValueRepository = new TagValueRepository(dbContext);
        tagService = new TagService(tagRepository, tagValueRepository);
        userService = new UserService(userRepository);
    }

    public static TagService GetTagService()
    {
        return tagService;
    }

    public static UserService GetUserService()
    {
        return userService;
    }

    public static ScadaDbContext GetDbContext()
    {
        return dbContext;
    }
}