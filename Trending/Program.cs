using System;
using System.ServiceModel;
using Trending.Services;

namespace Trending;

internal class Program
{
    private static void Main()
    {
        var host = new ServiceHost(typeof(TrendingService), new Uri("http://localhost:8733/TrendingService"));
        host.AddServiceEndpoint(typeof(ITrendingService), new BasicHttpBinding(), "");
        host.Open();

        Console.WriteLine("Service is running at http://localhost:8733/TrendingService");
        Console.WriteLine("Press [Enter] to close the service.");
        Console.ReadLine();

        host.Close();
    }
}