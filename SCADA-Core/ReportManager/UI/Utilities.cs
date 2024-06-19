using ReportManager.Commands;
using ServiceReference1;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportManager.UI
{
    internal class Utilities
    {
        public static DateTime ReadDateTime(string prompt)
        {
            Console.WriteLine(prompt);
            string input = Console.ReadLine();

            DateTime date;
            while (!DateTime.TryParse(input, CultureInfo.InvariantCulture.DateTimeFormat, DateTimeStyles.RoundtripKind, out date))
            {
                Console.WriteLine("Invalid date");
                input = Console.ReadLine();
            }

            return date;
        }

        public static List<DateTime> ReadInterval(string beginPrompt, string endPrompt)
        {
            DateTime begin = ReadDateTime(beginPrompt);
            DateTime end = ReadDateTime(endPrompt);

            while(end < begin)
            {
                Console.WriteLine("The end of the interval must be after the begining of the interval.");
                begin = ReadDateTime(beginPrompt);
                end = ReadDateTime(beginPrompt);
            }

            return new List<DateTime> { begin, end };
        }

        public static string ReadNonBlankString(string prompt, string errorPrompt)
        {
            Console.WriteLine(prompt);
            string value = Console.ReadLine();
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
            Console.WriteLine($"{"Alarm name", -20} | {"Alarm type", -10} | {"Tag name", -20} | {"Time", -21} | {"Alarm limit", -12} | {"Priority", -8} |");
        }

        public static void PrintAlarm(TriggeredAlarmDto triggeredAlarmDto)
        {
            Alarm alarm = triggeredAlarmDto.Alarm;
            Console.WriteLine($"{alarm.AlarmName, -20} | {alarm.Type, -10} | {triggeredAlarmDto.TagDescription, -20} | {alarm.Time, -21} | {alarm.Limit, 12} | {alarm.Priority, -8} |");
        }

        public static void PrintTagHeader()
        {
            Console.WriteLine($"{"Tag id", -36} | {"Tag name", -20} | {"Time", -21} | {"Value", 20} |");
        }

        public static void PrintTagValue(TagValueDTO tagValueDto)
        {
            Console.WriteLine($"{tagValueDto.TagId, -36} | {tagValueDto.Name, -20} | {tagValueDto.Time, -21} | {tagValueDto.Value, 20} |");
        }

        public static void PrintTagValues(IEnumerable<TagValueDTO> tagValueDtos)
        {
            PrintTagHeader();
            foreach (TagValueDTO tagValueDto in tagValueDtos)
            {
                PrintTagValue(tagValueDto);
            }
        }
    }
}
