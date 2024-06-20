using System;
using DatabaseManager.TagService;

namespace DatabaseManager.Commands;

internal class GetAllAlarmsCommand(ITagController tagController) : ICommand
{
    public string GetDescription()
    {
        return "View all enabled alarms";
    }

    public void Execute(string token)
    {
        Console.WriteLine("All alarms:");
        Console.WriteLine($"{"Alarm name",-20} | {"Tag ID",-36} | {"Alarm type",-10} | {"Alarm limit",-11} | ");
        foreach (var alarm in tagController.GetAllAlarms(token))
            Console.WriteLine($"{alarm.AlarmName,-20} | {alarm.TagId,-36} | {alarm.Type,-10} | {alarm.Limit,11} | ");
    }
}