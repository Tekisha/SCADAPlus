using System;
using DatabaseManager.TagService;

namespace DatabaseManager.Commands;

internal class GetOutputValueCommand(ITagController tagController) : ICommand
{
    public string GetDescription()
    {
        return "Get Tag Output Value.";
    }

    public void Execute(string token)
    {
        Console.Write("Enter tag ID: ");
        var tagId = Console.ReadLine()?.Trim();

        var tagValue = tagController.GetTagValue(tagId, token);
        Console.WriteLine($"Tag Value: {tagValue}");
    }
}