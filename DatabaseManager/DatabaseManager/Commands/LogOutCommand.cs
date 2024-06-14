using System;
using DatabaseManager.UserService;

namespace DatabaseManager.Commands;

internal class LogOutCommand(IUserController userController, Action<bool> setLoggedIn) : ICommand
{
    public string GetDescription()
    {
        return "Log out.";
    }

    public void Execute()
    {
        setLoggedIn(false);
    }
}