using DatabaseManager.TagService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseManager.Commands;

internal class RemoveTagCommand(ITagController tagController) : ICommand
{
    public string GetDescription()
    {
        return "Remove tag";
    }

    public void Execute(string token)
    {
        var id = Utilities.GetNonBlankString("Enter the id of the tag to remove:", "Tag id cannot be blank");
        try
        {
            tagController.RemoveTag(id, token);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

}
