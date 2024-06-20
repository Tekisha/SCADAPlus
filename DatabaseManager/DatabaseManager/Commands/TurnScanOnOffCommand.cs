using System;
using DatabaseManager.TagService;

namespace DatabaseManager.Commands;

internal class TurnScanOnOffCommand(ITagController tagController) : ICommand
{
    public string GetDescription()
    {
        return "Turn Scan On/Off.";
    }

    public void Execute(string token)
    {
        Console.WriteLine("Enter tag ID: ");
        var tagId = Console.ReadLine();
        Console.WriteLine("Should turn on or off? [on/off]");
        var onOff = Console.ReadLine();
        var shouldTurnOn = onOff != null && onOff.ToLower() == "on";
        tagController.TurnScanOnOff(tagId, shouldTurnOn, token);
    }
}