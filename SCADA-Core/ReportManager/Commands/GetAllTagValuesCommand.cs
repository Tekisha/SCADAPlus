using ServiceReference1;

namespace ReportManager.Commands
{
    internal class GetAllTagValuesCommand(IReportController reportController) : ICommand
    {
        public void Execute(string token)
        {
            throw new NotImplementedException();
        }

        public string GetDescription()
        {
            return "Get all values for a tag";
        }
    }
}