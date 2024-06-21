using System;
using DatabaseManager.TagService;
using static DatabaseManager.Utilities;

namespace DatabaseManager.Commands;

internal class AddAlarmCommand(ITagController tagController) : ICommand
{
    public string GetDescription()
    {
        return "Add alarm.";
    }

    public void Execute(string token)
    {
        while (true)
            try
            {
                tagController.CreateAlarm(ReadAlarm(), token);
                break;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("Something went wrong. Would you like to try again?");
                if (Console.ReadLine() != "y") break;
            }
    }
}