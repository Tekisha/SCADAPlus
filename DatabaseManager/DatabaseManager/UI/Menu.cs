using System;
using System.Collections.Generic;
using System.ServiceModel;
using DatabaseManager.Commands;
using DatabaseManager.TagService;
using DatabaseManager.UserService;

namespace DatabaseManager.UI;

public class Menu
{
    private readonly Dictionary<int, ICommand> _commands;

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

        _commands = new Dictionary<int, ICommand>
        {
            //{1, new AddTagCommand(tagServiceProxy)},
            { 2, new GetOutputValueCommand(tagServiceProxy) },
            { 3, new GetAllTagsCommand(tagServiceProxy) },
            //{4, new RegisterUserCommand(userServiceProxy)},
            { 5, new LogInCommand(userServiceProxy) },
            //{6, new LogOutCommand(userServiceProxy)},
            { 7, new ExitCommand() }
        };
    }

    private void Show()
    {
        foreach (var command in _commands) Console.WriteLine($"{command.Key} - {command.Value.GetDescription()}");
    }

    private ICommand GetCommand()
    {
        int command;
        Console.Write("Enter command: ");
        while (!int.TryParse(Console.ReadLine(), out command) || !_commands.ContainsKey(command))
            Console.Write("Invalid command. Please try again. Enter command: ");

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