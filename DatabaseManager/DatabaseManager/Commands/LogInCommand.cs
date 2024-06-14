using System;
using DatabaseManager.UserService;

namespace DatabaseManager.Commands;

public class LogInCommand(IUserController userController) : ICommand
{
    public string GetDescription()
    {
        return "Log in.";
    }

    public void Execute()
    {
        Console.WriteLine("Enter username:");
        var username = Console.ReadLine();
        Console.WriteLine("Enter password:");
        var password = Console.ReadLine();
        var isLogged = userController.LogIn(username, password);
        Console.WriteLine(isLogged ? "Successfully logged in." : "Invalid username or password.");
    }
}