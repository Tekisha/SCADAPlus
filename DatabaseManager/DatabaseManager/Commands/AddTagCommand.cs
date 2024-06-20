using System;
using DatabaseManager.TagService;

namespace DatabaseManager.Commands;

internal class AddTagCommand(ITagController tagController) : ICommand
{
    public string GetDescription()
    {
        return "Add Tag.";
    }

    public void Execute(string token)
    {
        var tagId = Guid.NewGuid().ToString();

        Console.Write("Enter tag description: ");
        var tagDescription = Console.ReadLine();

        Console.Write("Enter tag IO address [R/C/S/IP_Address]: ");
        var tagIoAddress = Console.ReadLine();

        Console.Write("Enter tag driver [SIM/RT]: ");
        var tagDriver = Console.ReadLine();

        Console.Write("Enter tag scan time (ms): ");
        var tagScanTime = Console.ReadLine();

        Console.Write("Enter tag on/off scan [true/false]: ");
        var tagOnOffScan = Console.ReadLine();

        Console.Write("Enter tag low limit: ");
        var tagLowLimit = Console.ReadLine();

        Console.Write("Enter tag high limit: ");
        var tagHighLimit = Console.ReadLine();

        Console.Write("Enter tag units: ");
        var tagUnits = Console.ReadLine();

        Console.Write("Enter tag alarms [true/false]: ");
        var tagAlarms = Console.ReadLine();

        var tagDto = new TagDto
        {
            Id = tagId,
            Description = tagDescription?.Trim(),
            IoAddress = tagIoAddress?.ToUpper(),
            Driver = tagDriver?.ToUpper(),
            ScanTime = int.Parse(tagScanTime!),
            OnOffScan = bool.Parse(tagOnOffScan!),
            LowLimit = double.Parse(tagLowLimit!),
            HighLimit = double.Parse(tagHighLimit!),
            Units = tagUnits?.Trim(),
            Alarms = bool.Parse(tagAlarms!)
        };

        tagController.AddTag(tagDto, token);
    }
}