using DatabaseManager.TagService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseManager.Commands
{
    public class DeleteAlarmCommand(ITagController tagController) : ICommand
    {
        public string GetDescription()
        {
            return "Remove alarm";
        }

        public void Execute(string token)
        {
            string alarmName = GetNonBlankString("Enter the name of the alarm to delete:", "The name of the alarm cannot be blank");
            try
            {
                tagController.DeleteAlarm(alarmName, token);
            } catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private string GetNonBlankString(string prompt, string errorPrompt)
        {
            Console.WriteLine(prompt);
            string value = Console.ReadLine();
            while (value == null || value.Trim() == "")
            {
                Console.WriteLine(errorPrompt);
                Console.WriteLine(prompt);
                value = Console.ReadLine();
            }

            return value;
        }

    }
}
