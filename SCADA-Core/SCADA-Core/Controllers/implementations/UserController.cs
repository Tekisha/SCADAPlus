using SCADA_Core.Controllers.interfaces;
using SCADA_Core.Repositories.implementations;
using SCADA_Core.Services.implementations;
using SCADA_Core.Services.interfaces;
using SCADA_Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCADA_Core.Controllers.implementations
{
    public class UserController : IUserController
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        public bool RegisterUser(string username, string password)
        {
            return userService.RegisterUser(username, password);
        }

        public bool LogIn(string username, string password)
        {
            return userService.LogIn(username, password);
        }
    }
}
