using System;
using DatabaseManager.UserService;

namespace DatabaseManager.Commands;

internal class LogOutCommand(IUserController userController, Action<string> setToken) : ICommand
{
    public string GetDescription()
    {
        return "Log out.";
    }

    public void Execute()
    {
        setToken(null);
    }
}