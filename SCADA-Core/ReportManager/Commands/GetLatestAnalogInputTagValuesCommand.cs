using ReportManager.UI;
using ServiceReference1;

namespace ReportManager.Commands
{
    internal class GetLatestAnalogInputTagValuesCommand(IReportController reportController) : ICommand
    {
        public void Execute(string token)
        {
            Utilities.PrintTagValues(reportController.GetLatestAnalogInputTagValues(token));
        }

        public string GetDescription()
        {
            return "Get the latest values of all analog input tags";
        }
    }
}