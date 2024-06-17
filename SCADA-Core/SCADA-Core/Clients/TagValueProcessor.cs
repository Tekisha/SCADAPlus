using System;
using System.ServiceModel;
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

    public static void ProcessTagValue(TagValue value)
    {
        try
        {
            TrendingServiceProxy.SendTagValue(value);
        }
        catch (Exception ex)
        {
            // Handle exceptions
            Console.WriteLine($@"Error sending tag value: {ex.Message}");
        }
    }
}