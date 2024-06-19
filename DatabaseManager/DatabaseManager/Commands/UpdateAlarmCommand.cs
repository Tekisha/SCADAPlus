using DatabaseManager.TagService;
using System;

namespace DatabaseManager.Commands
{
    public class UpdateAlarmCommand(ITagController tagController) : ICommand
    {
        public string GetDescription()
        {
            return "Update alarm";
        }

        public void Execute(string token)
        {
            while(true)
            {
                try
                {
                    tagController.UpdateAlarm(AddAlarmCommand.ReadAlarm(), token);
                    break;
                } catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine("Something went wrong. Would you like to try again?");
                    if (Console.ReadLine() != "y") {
                        break;
                    }

                }
            }
        }
    }
}
