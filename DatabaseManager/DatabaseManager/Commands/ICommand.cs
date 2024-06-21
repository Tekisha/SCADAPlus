namespace DatabaseManager.Commands;

public interface ICommand
{
    string GetDescription();
    void Execute(string token);
}