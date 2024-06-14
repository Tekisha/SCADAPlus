using SCADA_Core.Controllers.interfaces;
using SCADA_Core.Services.interfaces;

namespace SCADA_Core.Controllers.implementations;

public class UserController(IUserService userService) : IUserController
{
    public bool RegisterUser(string username, string password)
    {
        return userService.RegisterUser(username, password);
    }

    public string LogIn(string username, string password)
    {
        return userService.LogIn(username, password);
    }
}