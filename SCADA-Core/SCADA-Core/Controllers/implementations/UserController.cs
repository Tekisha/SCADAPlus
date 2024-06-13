using SCADA_Core.Controllers.interfaces;
using SCADA_Core.Services.interfaces;

namespace SCADA_Core.Controllers.implementations;

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