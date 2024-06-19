using ServiceReference1;

namespace ReportManager.Commands
{
    internal class GetLatetstDigitalInputTagValuesCommand(IReportController reportController) : ICommand
    {
        public void Execute(string token)
        {
            throw new NotImplementedException();
        }

        public string GetDescription()
        {
            return "Get the latest values of all digital input tags";
        }
    }
}