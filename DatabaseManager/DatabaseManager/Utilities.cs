using System;
using DatabaseManager.TagService;

namespace DatabaseManager;

internal class Utilities
{
    public static AlarmDto ReadAlarm()
    {
        var alarmName = GetNonBlankString("Enter alarm name:", "Alarm name cannot be blank");

        double limit;
        Console.Write("Enter limit: ");
        while (!double.TryParse(Console.ReadLine(), out limit))
        {
            Console.WriteLine("ERROR: Limit must be a number");
            Console.Write("Enter limit: ");
        }

        var tagId = GetNonBlankString("Enter tagId:", "Tag id cannot be blank");


        string priorityAsString;
        while (true)
        {
            Console.Write("Enter priority [Low/Medium/High]: ");
            priorityAsString = Console.ReadLine()?.ToUpper();
            if (priorityAsString is "LOW" or "MEDIUM" or "HIGH") break;
            Console.WriteLine("ERROR: Priority must be either Low, Medium or High");
        }

        var priority = (AlarmPriority)Enum.Parse(typeof(AlarmPriority), priorityAsString);

        string type;
        while (true)
        {
            Console.Write("Enter type [ABOVE/BELOW]: ");
            type = Console.ReadLine()?.ToUpper();
            if (type is "ABOVE" or "BELOW") break;
            Console.WriteLine("ERROR: Type must be either ABOVE or BELOW");
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
        while (true)
        {
            Console.Write($"{prompt} ");
            var value = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(value)) return value;
            Console.WriteLine($"ERROR: {errorPrompt}");
        }
    }
}