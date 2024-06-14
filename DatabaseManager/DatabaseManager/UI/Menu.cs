using System;
using System.Collections.Generic;
using System.ServiceModel;
using DatabaseManager.Commands;
using DatabaseManager.TagService;
using DatabaseManager.UserService;

namespace DatabaseManager.UI;

public class Menu
{
    private readonly Dictionary<int, ICommand> _authenticatedUserCommands;
    private readonly Dictionary<int, ICommand> _commands;
    private readonly Dictionary<int, ICommand> _unauthenticatedUserCommands;
    private bool _loggedIn;

    public Menu()
    {
        const string tagServiceBaseAddress = "http://localhost:8733/SCADA/TagController/";
        const string userServiceBaseAddress = "http://localhost:8733/SCADA/UserController/";

        var binding = new BasicHttpBinding();

        var tagServiceEndpoint = new EndpointAddress(tagServiceBaseAddress);
        var userServiceEndpoint = new EndpointAddress(userServiceBaseAddress);

        var tagServiceFactory = new ChannelFactory<ITagController>(binding, tagServiceEndpoint);
        var userServiceFactory = new ChannelFactory<IUserController>(binding, userServiceEndpoint);

        var tagServiceProxy = tagServiceFactory.CreateChannel();
        var userServiceProxy = userServiceFactory.CreateChannel();

        _authenticatedUserCommands = new Dictionary<int, ICommand>
        {
            //{1, new AddTagCommand(tagServiceProxy)},
            { 2, new GetOutputValueCommand(tagServiceProxy) },
            { 3, new GetAllTagsCommand(tagServiceProxy) },
            //{4, new RegisterUserCommand(userServiceProxy)},
            { 5, new LogInCommand(userServiceProxy, isLogged => _loggedIn = isLogged) },
            //{6, new LogOutCommand(userServiceProxy)},
            { 7, new ExitCommand() }
        };

        _unauthenticatedUserCommands = new Dictionary<int, ICommand>
        {
            //{ 1, new RegisterUserCommand(userServiceProxy)},
            { 5, new LogInCommand(userServiceProxy, isLogged => _loggedIn = isLogged) },
            { 7, new ExitCommand() }
        };

        _commands = _unauthenticatedUserCommands;
    }

    private void Show()
    {
        Console.WriteLine();
        Console.WriteLine("------------Commands------------");
        foreach (var command in _commands) Console.WriteLine($"{command.Key} - {command.Value.GetDescription()}");
    }

    private ICommand GetCommand()
    {
        int command;
        Console.Write("Enter command: ");
        while (!int.TryParse(Console.ReadLine(), out command) || !_commands.ContainsKey(command))
            Console.Write("Invalid command. Please try again. Enter command: ");

        Console.WriteLine();
        return _commands[command];
    }

    public void Run()
    {
        ICommand command;
        do
        {
            Show();
            command = GetCommand();
            command.Execute();
        } while (command is not ExitCommand);
    }
}