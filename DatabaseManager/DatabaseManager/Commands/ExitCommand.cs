using System;
using System.ServiceModel;
using DatabaseManager.TagService;
using DatabaseManager.UserService;

namespace DatabaseManager.Commands;

internal class ExitCommand(IUserController userController, ITagController tagController) : ICommand
{
    public string GetDescription()
    {
        return "Exit application.";
    }

    public void Execute(string _)
    {
        ((IClientChannel)tagController).Close();
        ((IClientChannel)userController).Close();
        Environment.Exit(0);
    }
}