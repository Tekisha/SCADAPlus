using System;
using Trending.Models;

namespace Trending.Services;

public class TrendingService : ITrendingService
{
    public void SendTagValue(TagValue value)
    {
        Console.WriteLine($"Received Tag: {value.TagName}, Value: {value.Value}, Timestamp: {value.Timestamp}");
    }
}