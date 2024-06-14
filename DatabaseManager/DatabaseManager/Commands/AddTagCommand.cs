using System;
using DatabaseManager.TagService;

namespace DatabaseManager.Commands;

internal class AddTagCommand(ITagController tagController) : ICommand
{
    public string GetDescription()
    {
        return "Add Tag.";
    }

    public void Execute()
    {
        Console.WriteLine("Enter tag ID:");
        var tagId = Console.ReadLine();

        Console.WriteLine("Enter tag description:");
        var tagDescription = Console.ReadLine();

        Console.WriteLine("Enter tag IO address:");
        var tagIoAddress = Console.ReadLine();

        Console.WriteLine("Enter tag driver:");
        var tagDriver = Console.ReadLine();

        Console.WriteLine("Enter tag scan time:");
        var tagScanTime = Console.ReadLine();

        Console.WriteLine("Enter tag on/off scan:");
        var tagOnOffScan = Console.ReadLine();

        Console.WriteLine("Enter tag low limit:");
        var tagLowLimit = Console.ReadLine();

        Console.WriteLine("Enter tag high limit:");
        var tagHighLimit = Console.ReadLine();

        Console.WriteLine("Enter tag units:");
        var tagUnits = Console.ReadLine();

        Console.WriteLine("Enter tag alarms:");
        var tagAlarms = Console.ReadLine();

        var tagDto = new TagDto
        {
            Id = tagId,
            Description = tagDescription,
            IoAddress = tagIoAddress,
            Driver = tagDriver,
            ScanTime = int.Parse(tagScanTime!),
            OnOffScan = bool.Parse(tagOnOffScan!),
            LowLimit = double.Parse(tagLowLimit!),
            HighLimit = double.Parse(tagHighLimit!),
            Units = tagUnits,
            Alarms = bool.Parse(tagAlarms!)
        };

        tagController.AddTag(tagDto);
    }
}