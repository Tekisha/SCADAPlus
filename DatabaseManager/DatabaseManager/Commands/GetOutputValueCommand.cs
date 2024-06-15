using System;
using DatabaseManager.TagService;

namespace DatabaseManager.Commands;

internal class GetOutputValueCommand(ITagController tagController, string token) : ICommand
{
    public string GetDescription()
    {
        return "Get Tag Output Value.";
    }

    public void Execute()
    {
        Console.WriteLine("Enter tag ID:");
        var tagId = Console.ReadLine();

        var tagValue = tagController.GetTagValue(tagId, token);
        Console.WriteLine($"Tag Value: {tagValue}");
    }
}