using System;
using DatabaseManager.TagService;
using static DatabaseManager.Utilities;

namespace DatabaseManager.Commands;

public class DeleteAlarmCommand(ITagController tagController) : ICommand
{
    public string GetDescription()
    {
        return "Remove alarm";
    }

    public void Execute(string token)
    {
        var alarmName = GetNonBlankString("Enter the name of the alarm to delete:",
            "The name of the alarm cannot be blank");
        try
        {
            tagController.DeleteAlarm(alarmName, token);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
}