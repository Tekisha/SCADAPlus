using System;
using System.Runtime.Serialization;

namespace Trending.Models;

[DataContract]
public class TagValue
{
    [DataMember] public string TagName { get; set; }

    [DataMember] public double Value { get; set; }

    [DataMember] public DateTime Timestamp { get; set; }
}