using System;
using System.ServiceModel;
using System.ServiceModel.Description;
using Trending.Services;

namespace Trending;

internal class Program
{
    private static void Main()
    {
        var baseAddress = new Uri("http://localhost:8733/TrendingService");

        using var host = new ServiceHost(typeof(TrendingService), baseAddress);
        // Check if the ServiceMetadataBehavior is already added
        var smb = host.Description.Behaviors.Find<ServiceMetadataBehavior>();
        if (smb == null)
        {
            smb = new ServiceMetadataBehavior
            {
                HttpGetEnabled = true
            };
            host.Description.Behaviors.Add(smb);
        }

        host.AddServiceEndpoint(typeof(ITrendingService), new BasicHttpBinding(), "");

        host.Open();

        Console.WriteLine("Service is running at http://localhost:8733/TrendingService");
        Console.WriteLine("Press [Enter] to close the service.");
        Console.ReadLine();

        host.Close();
    }
}