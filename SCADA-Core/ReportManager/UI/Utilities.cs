using System.Globalization;
using ServiceReference1;

namespace ReportManager.UI;

internal class Utilities
{
    public static DateTime ReadDateTime(string prompt)
    {
        Console.WriteLine(prompt);
        var input = Console.ReadLine();

        DateTime date;
        while (!DateTime.TryParse(input, CultureInfo.InvariantCulture.DateTimeFormat, DateTimeStyles.RoundtripKind,
                   out date))
        {
            Console.WriteLine("Invalid date");
            input = Console.ReadLine();
        }

        return date;
    }

    public static List<DateTime> ReadInterval(string beginPrompt, string endPrompt)
    {
        var begin = ReadDateTime($"{beginPrompt} ");
        var end = ReadDateTime($"{endPrompt} ");

        while (end < begin)
        {
            Console.WriteLine("ERROR: The end of the interval must be after the beginning of the interval.");
            begin = ReadDateTime(beginPrompt);
            end = ReadDateTime(beginPrompt);
        }

        return [begin, end];
    }

    public static string ReadNonBlankString(string prompt, string errorPrompt)
    {
        Console.WriteLine($"{prompt} ");
        var value = Console.ReadLine();
        while (value == null || value.Trim() == "")
        {
            Console.WriteLine(errorPrompt);
            Console.WriteLine(prompt);
            value = Console.ReadLine();
        }

        return value;
    }

    public static void PrintAlarmHeader()
    {
        Console.WriteLine(
            $"{"Alarm name",-20} | {"Alarm type",-10} | {"Tag name",-20} | {"Time",-21} | {"Alarm limit",-12} | {"Priority",-8} |");
    }

    public static void PrintAlarm(TriggeredAlarmDto triggeredAlarmDto)
    {
        var alarm = triggeredAlarmDto.Alarm;
        Console.WriteLine(
            $"{alarm.AlarmName,-20} | {alarm.Type,-10} | {triggeredAlarmDto.TagDescription,-20} | {alarm.Time,-21} | {alarm.Limit,12} | {alarm.Priority,-8} |");
    }

    public static void PrintTagHeader()
    {
        Console.WriteLine($"{"Tag id",-36} | {"Tag name",-20} | {"Time",-21} | {"Value",20} |");
    }

    public static void PrintTagValue(TagValueDto tagValueDto)
    {
        Console.WriteLine(
            $"{tagValueDto.TagId,-36} | {tagValueDto.Name,-20} | {tagValueDto.Time,-21} | {tagValueDto.Value,20} |");
    }

    public static void PrintTagValues(IEnumerable<TagValueDto> tagValueDtos)
    {
        PrintTagHeader();
        foreach (var tagValueDto in tagValueDtos) PrintTagValue(tagValueDto);
    }
}