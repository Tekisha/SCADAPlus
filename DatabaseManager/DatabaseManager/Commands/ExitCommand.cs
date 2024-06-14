using System;

namespace DatabaseManager.Commands;

internal class ExitCommand : ICommand
{
    public string GetDescription()
    {
        return "Exit application.";
    }

    public void Execute()
    {
        Environment.Exit(0);
    }
}