using System;
using DatabaseManager.TagService;

namespace DatabaseManager;

internal class Utilities
{
    public static AlarmDto ReadAlarm()
    {
        var alarmName = GetNonBlankString("Enter alarm name:", "Alarm name cannot be blank");

        double limit;
        Console.WriteLine("Enter limit:");
        while (!double.TryParse(Console.ReadLine(), out limit))
        {
            Console.WriteLine("Limit must be a number");
            Console.WriteLine("Enter limit:");
        }

        var tagId = GetNonBlankString("Enter tagId:", "Tag id cannot be blank");


        int priorityCode;
        Console.WriteLine("Enter priority (1 - Low | 2 - Medium | 3- High:");
        while (!int.TryParse(Console.ReadLine(), out priorityCode) || priorityCode < 1 || priorityCode > 3)
        {
            Console.WriteLine("Priority must be a number between one and 3");
            Console.WriteLine("Enter priority:");
        }

        var priority = (AlarmPriority)priorityCode;

        Console.WriteLine("Enter type (ABOVE/BELOW)");
        var type = Console.ReadLine();
        while (type != "ABOVE" && type != "BELOW")
        {
            Console.WriteLine("Type must be either ABOVE or BELOW");
            Console.WriteLine("Enter type (ABOVE/BELOW)");
            type = Console.ReadLine();
        }

        var alarmDto = new AlarmDto
        {
            AlarmName = alarmName,
            Limit = limit,
            Acknowledged = false,
            TagId = tagId,
            Priority = priority,
            Type = type
        };

        return alarmDto;
    }

    public static string GetNonBlankString(string prompt, string errorPrompt)
    {
        Console.WriteLine(prompt);
        var value = Console.ReadLine();
        while (value == null || value.Trim() == "")
        {
            Console.WriteLine(errorPrompt);
            Console.WriteLine(prompt);
            value = Console.ReadLine();
        }

        return value;
    }
}