using System;

namespace SCADA_Core.Models;

public class TagValueChange
{
    public Tag Tag { get; set; }
    public double Value { get; set; }
    public DateTime Time { get; set; }
}