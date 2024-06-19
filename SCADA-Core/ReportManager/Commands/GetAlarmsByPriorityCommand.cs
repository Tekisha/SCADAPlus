using ReportManager.UI;
using ServiceReference1;

namespace ReportManager.Commands
{
    internal class GetAlarmsByPriorityCommand(IReportController reportController) : ICommand
    {
        public void Execute(string token)
        {
            AlarmPriority priority = AlarmPriority.Low;
            bool inputValid = false; 
            while (!inputValid) {
                Console.WriteLine("Enter priority");
                string input = Console.ReadLine();
                input = input.ToLower();
                switch(input)
                {
                    case "high":
                        priority = AlarmPriority.High;
                        inputValid = true;
                        break;
                    case "medium":
                        priority = AlarmPriority.Medium;
                        inputValid = true;
                        break;
                    case "low":
                        priority = AlarmPriority.Low;
                        inputValid = true;
                        break;
                    default:
                        Console.WriteLine("Priority must be either High, Medium or Low");
                        break;
                }
            }

            Utilities.PrintAlarmHeader();
            foreach (TriggeredAlarmDto triggeredAlarmDto in reportController.GetAlarmsByPriority(priority, token))
            {
                Utilities.PrintAlarm(triggeredAlarmDto);
            }
        }

        public string GetDescription()
        {
            return "Get alarms by priority";
        }
    }
}