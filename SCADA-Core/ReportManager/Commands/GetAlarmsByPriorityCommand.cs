using ReportManager.UI;
using ServiceReference1;

namespace ReportManager.Commands;

internal class GetAlarmsByPriorityCommand(IReportController reportController) : ICommand
{
    public void Execute(string token)
    {
        AlarmPriority priority;
        while (true)
        {
            Console.WriteLine("Enter priority [High, Medium, Low]:");
            var input = Console.ReadLine();
            input = input?.ToLower().Trim();

            if (((string[]) ["high", "medium", "low"]).Contains(input))
            {
                priority = (AlarmPriority)Enum.Parse(typeof(AlarmPriority), input!, true);
                break;
            }

            Console.WriteLine("Priority must be either High, Medium or Low");
        }

        Utilities.PrintAlarmHeader();
        foreach (var triggeredAlarmDto in reportController.GetAlarmsByPriority(priority, token))
            Utilities.PrintAlarm(triggeredAlarmDto);
    }

    public string GetDescription()
    {
        return "Get alarms by priority";
    }
}