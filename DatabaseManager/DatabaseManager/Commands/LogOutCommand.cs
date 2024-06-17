using System;

namespace DatabaseManager.Commands;

internal class LogOutCommand(Action<string> setToken) : ICommand
{
    public string GetDescription()
    {
        return "Log out.";
    }

    public void Execute(string _)
    {
        setToken(null);
    }
}