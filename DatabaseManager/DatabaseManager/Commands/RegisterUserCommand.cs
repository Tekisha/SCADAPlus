﻿using System;
using DatabaseManager.UserService;

namespace DatabaseManager.Commands;

internal class RegisterUserCommand(IUserController userController, Action<string> setToken) : ICommand
{
    public string GetDescription()
    {
        return "Register User.";
    }

    public void Execute(string _)
    {
        Console.Write("Enter username: ");
        var username = Console.ReadLine();
        Console.Write("Enter password: ");
        var password = Console.ReadLine();
        var isRegistered = userController.RegisterUser(username, password);
        Console.WriteLine(isRegistered ? "Successfully registered." : "ERROR: Username already exists.");
        if (!isRegistered) return;

        var token = userController.LogIn(username, password);
        setToken(token);
    }
}