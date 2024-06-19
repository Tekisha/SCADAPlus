using System.ServiceModel;
using ReportManager.Commands;
using ServiceReference1;
using ServiceReference2;

namespace ReportManager.UI;

public class Menu
{
    private readonly Dictionary<int, ICommand> _authenticatedUserCommands;
    private readonly Dictionary<int, ICommand> _unauthenticatedUserCommands;
    private Dictionary<int, ICommand> _commands;
    private string _token;

    public Menu()
    {
        const string reportServiceBaseAddress = "http://localhost:8733/SCADA/ReportController/";
        const string userServiceBaseAddress = "http://localhost:8733/SCADA/UserController/";

        var binding = new BasicHttpBinding();
        binding.MaxReceivedMessageSize = 4 * 1024 * 1024;

        var reportServiceEndpoint = new EndpointAddress(reportServiceBaseAddress);
        var userServiceEndpoint = new EndpointAddress(userServiceBaseAddress);

        var reportServiceFactory = new ChannelFactory<IReportController>(binding, reportServiceEndpoint);
        var userServiceFactory = new ChannelFactory<IUserController>(binding, userServiceEndpoint);

        var userServiceProxy = userServiceFactory.CreateChannel();
        var reportServiceProxy = reportServiceFactory.CreateChannel();

        var loggedInAction = new Action<string>(newToken => Token = newToken);
        _authenticatedUserCommands = new Dictionary<int, ICommand>
        {
            { 1, new GetAlarmsDuringIntervalCommand(reportServiceProxy) },
            /*
            { 2, new GetAlarmsByPriorityCommand(reportServiceProxy) },
            { 3, new GetTagValuesDuringIntervalCommand(reportServiceProxy) },
            { 4, new GetLatestAnalogInputTagValuesCommand(reportServiceProxy) },
            { 5, new GetLatetstDigitalInputTagValuesCommand(reportServiceProxy) },
            { 6, new GetAllTagValuesCommand(reportServiceProxy) },
            */
            { 7, new ExitCommand(userServiceProxy, reportServiceProxy) }
        };

        _unauthenticatedUserCommands = new Dictionary<int, ICommand>
        {
            { 1, new RegisterUserCommand(userServiceProxy, loggedInAction) },
            { 2, new LogInCommand(userServiceProxy, loggedInAction) },
            { 3, new ExitCommand(userServiceProxy, reportServiceProxy) }
        };

        _commands = _unauthenticatedUserCommands;
    }

    private string Token
    {
        get => _token;
        set
        {
            _token = value;
            _commands = value is not null ? _authenticatedUserCommands : _unauthenticatedUserCommands;
        }
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
            command.Execute(Token);
        } while (command is not ExitCommand);
    }
}