using System;
using System.Linq;
using DatabaseManager.TagService;

namespace DatabaseManager.Commands;

internal class GetAllTagsCommand(ITagController tagController) : ICommand
{
    public string GetDescription()
    {
        return "Get All Tags.";
    }

    public void Execute()
    {
        Console.WriteLine("All Tags:");
        var tags = tagController.GetAllTags();
        tags.ToList().ForEach(tag => Console.WriteLine($"\t{tag.Id} - {tag.Description}"));
    }
}