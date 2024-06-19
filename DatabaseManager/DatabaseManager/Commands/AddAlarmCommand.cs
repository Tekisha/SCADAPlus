using DatabaseManager.TagService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseManager.Commands
{
    internal class AddAlarmCommand(ITagController tagController) : ICommand
    {
        public string GetDescription()
        {
            return "Add alarm.";
        }

        public void Execute(string token)
        {
            while(true)
            {
                try
                {
                    tagController.CreateAlarm(ReadAlarm(), token);
                    break;
                } catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine("Something went wrong. Would you like to try again?");
                    if (Console.ReadLine() != "y") {
                        break;
                    }

                }
            }
        }

        public static AlarmDto ReadAlarm()
        {
            string alarmName = GetNonBlankString("Enter alarm name:", "Alarm name cannot be blank");

            double limit;
            Console.WriteLine("Enter limit:");
            while(!double.TryParse(Console.ReadLine(), out limit))
            {
                Console.WriteLine("Limit must be a number");
                Console.WriteLine("Enter limit:");
            }

            string tagId = GetNonBlankString("Enter tagId:", "Tag id cannot be blank");
            

            int priorityCode;
            Console.WriteLine("Enter priority (1 - Low | 2 - Medium | 3- High:");
            while(!int.TryParse(Console.ReadLine(), out priorityCode) || priorityCode < 1 || priorityCode > 3)
            {
                Console.WriteLine("Priority must be a number between one and 3");
                Console.WriteLine("Enter priority:");
            }

            AlarmPriority priority;
            switch (priorityCode)
            {
                case 1:
                    priority = AlarmPriority.Low;
                    break;
                case 2:
                    priority = AlarmPriority.Medium;
                    break;
                case 3:
                    priority = AlarmPriority.High;
                    break;
                default:
                    priority = AlarmPriority.Low;
                    break;
            }

            Console.WriteLine("Enter type (ABOVE/BELOW)");
            string type = Console.ReadLine();
            while (type != "ABOVE" && type != "BELOW")
            {
                Console.WriteLine("Type must be either ABOVE or BELOW");
                Console.WriteLine("Enter type (ABOVE/BELOW)");
                type = Console.ReadLine();
            }

            AlarmDto alarmDto = new AlarmDto
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

        private static string GetNonBlankString(string prompt, string errorPrompt)
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
    }
}
