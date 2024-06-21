using ReportManager.UI;
using ServiceReference1;

namespace ReportManager.Commands;

internal class GetLatestDigitalInputTagValuesCommand(IReportController reportController) : ICommand
{
    public void Execute(string token)
    {
        Utilities.PrintTagValues(reportController.GetLatetstDigitalInputTagValues(token));
    }

    public string GetDescription()
    {
        return "Get the latest values of all digital input tags";
    }
}