using ReportManager.UI;
using ServiceReference1;

namespace ReportManager.Commands;

internal class GetTagValuesDuringIntervalCommand(IReportController reportController) : ICommand
{
    public void Execute(string token)
    {
        var interval = Utilities.ReadInterval("Enter the beginning of the time interval:",
            "Enter the end of the time interval:");
        IEnumerable<TagValueDto> tagValues =
            reportController.GetTagValuesDuringInterval(interval[0], interval[1], token);
        Utilities.PrintTagValues(tagValues);
    }

    public string GetDescription()
    {
        return "Get tag values during interval";
    }
}