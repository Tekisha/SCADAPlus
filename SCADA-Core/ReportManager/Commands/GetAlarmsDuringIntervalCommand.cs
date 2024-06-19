using ReportManager.UI;
using ServiceReference1;

namespace ReportManager.Commands
{
    internal class GetAlarmsDuringIntervalCommand(IReportController reportController) : ICommand
    {
        public void Execute(string token)
        {
            List<DateTime> interval = Utilities.ReadInterval("Enter the beginning of the time interval", "Enter the end of the time interval");
            Utilities.PrintAlarmHeader();
            foreach (TriggeredAlarmDto triggeredAlarmDto in reportController.GetAlarmsDuringInterval(interval[0], interval[1], token))
            {
                Utilities.PrintAlarm(triggeredAlarmDto);
            }
        }

        public string GetDescription()
        {
            return "Get all alarms during interval";
        }
    }
}