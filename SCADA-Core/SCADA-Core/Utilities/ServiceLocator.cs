using SCADA_Core.Repositories.implementations;
using SCADA_Core.Repositories;
using SCADA_Core.Services.implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCADA_Core.Utilities
{
    public static class ServiceLocator
    {
        private static ScadaDbContext dbContext;
        private static TagRepository tagRepository;
        private static UserRepository userRepository;
        private static TagService tagService;
        private static UserService userService;

        static ServiceLocator()
        {
            dbContext = new ScadaDbContext();
            tagRepository = new TagRepository(dbContext);
            userRepository = new UserRepository(dbContext);
            tagService = new TagService(tagRepository);
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
}
