namespace ReportManager.Commands;

internal class LogOutCommand(Action<string> setToken) : ICommand
{
    public string GetDescription()
    {
        return "Log out.";
    }

    public void Execute(string _)
    {
        setToken(null!);
    }
}