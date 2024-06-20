using System;
using DatabaseManager.UserService;

namespace DatabaseManager.Commands;

public class LogInCommand(IUserController userController, Action<string> setToken) : ICommand
{
    public string GetDescription()
    {
        return "Log in.";
    }

    public void Execute(string _)
    {
        Console.Write("Enter username: ");
        var username = Console.ReadLine();
        Console.Write("Enter password: ");
        var password = Console.ReadLine();
        var isLogged = userController.LogIn(username, password);
        Console.WriteLine(isLogged != null ? "Successfully logged in." : "ERROR: Invalid username or password.");
        setToken(isLogged);
    }
}