using System;
using System.ServiceModel;
using SCADA_Core.Services.interfaces;
using SCADA_Core.TrendingService;

namespace SCADA_Core.Clients;

public class TagValueProcessor
{
    private static readonly ITrendingService TrendingServiceProxy;

    static TagValueProcessor()
    {
        const string trendingServiceBaseAddress = "http://localhost:8733/TrendingService/";
        var binding = new BasicHttpBinding();
        var endpoint = new EndpointAddress(trendingServiceBaseAddress);

        var factory = new ChannelFactory<ITrendingService>(binding, endpoint);

        TrendingServiceProxy = factory.CreateChannel();
    }

    public TagValueProcessor(ITagService tagService)
    {
        tagService.Subscribe(tagValueChanged => ProcessTagValue(new TagValue
        {
            TagName = tagValueChanged.Tag.Description,
            Timestamp = tagValueChanged.Time,
            Value = tagValueChanged.Value
        }));
    }

    public static void ProcessTagValue(TagValue value)
    {
        try
        {
            TrendingServiceProxy.SendTagValue(value);
        }
        catch (Exception)
        {
            // Handle exceptions
            //Console.WriteLine($"Error sending tag value: {ex.Message}");
        }
    }
}