using ReportManager.UI;
using ServiceReference1;

namespace ReportManager.Commands;

internal class GetAllTagValuesCommand(IReportController reportController) : ICommand
{
    public void Execute(string token)
    {
        var id = Utilities.ReadNonBlankString("Enter tag id", "Tag id cannot be blank");
        IEnumerable<TagValueDto> tagValues = reportController.GetAllTagValues(id, token);
        Utilities.PrintTagValues(tagValues);
    }

    public string GetDescription()
    {
        return "Get all values for a tag";
    }
}