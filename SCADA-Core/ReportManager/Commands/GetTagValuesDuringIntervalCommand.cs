using ServiceReference1;

namespace ReportManager.Commands
{
    internal class GetTagValuesDuringIntervalCommand(IReportController reportController) : ICommand
    {
        public void Execute(string token)
        {
            throw new NotImplementedException();
        }

        public string GetDescription()
        {
            return "Get tag values during interval";
        }
    }
}