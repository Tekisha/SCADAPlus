using System;
using DatabaseManager.TagService;

namespace DatabaseManager.Commands;

internal class GetOutputValueCommand(ITagController tagController) : ICommand
{
    public string GetDescription()
    {
        return "Get Tag Output Value.";
    }

    public void Execute()
    {
        Console.WriteLine("Enter tag ID:");
        var tagId = Console.ReadLine();

        var tagValue = tagController.GetTagValue(tagId);
        Console.WriteLine($"Tag Value: {tagValue}");
    }
}