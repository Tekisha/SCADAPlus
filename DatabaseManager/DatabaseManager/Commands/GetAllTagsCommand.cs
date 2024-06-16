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

    public void Execute(string token)
    {
        Console.WriteLine("All Tags:");
        var tags = tagController.GetAllTags(token);
        tags.ToList().ForEach(tag => Console.WriteLine($"\t{tag.Id} - {tag.Description}"));
    }
}