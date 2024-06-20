using System;
using System.ServiceModel;
using System.ServiceModel.Description;
using Microsoft.Extensions.DependencyInjection;
using RealTimeUnit;

namespace RealTimeUnitService
{
    internal class Program
    {
        private static void Main()
        {
            Console.Write("Enter port: ");
            var port = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());
            Console.Write("Enter min value: ");
            var minValue = double.Parse(Console.ReadLine() ?? throw new InvalidOperationException());
            Console.Write("Enter max value: ");
            var maxValue = double.Parse(Console.ReadLine() ?? throw new InvalidOperationException());

            while (maxValue <= minValue)
            {
                Console.WriteLine("ERROR: Max value must be greater than min value");
                Console.Write("Enter min value: ");
                minValue = double.Parse(Console.ReadLine() ?? throw new InvalidOperationException());
                Console.Write("Enter max value: ");
                maxValue = double.Parse(Console.ReadLine() ?? throw new InvalidOperationException());
            }

            var baseAddress = $"http://localhost:{port}/";
            // Create a service collection and configure dependencies
            var serviceCollection = new ServiceCollection();
            ConfigureService(serviceCollection, baseAddress, minValue, maxValue);

            // Build the service provider
            var serviceProvider = serviceCollection.BuildServiceProvider();


            // Use the service provider to create the WCF service host
            var realTimeUnitServiceHostFactory = new DIServiceHostFactory(serviceProvider);

            using (var rtuHost =
                   realTimeUnitServiceHostFactory.CreateServiceHost("RealTimeUnit.RealTimeUnitService",
                       new[] { new Uri(baseAddress) }))
            {
                try
                {
                    var binding = new WSDualHttpBinding
                    {
                        SendTimeout = TimeSpan.FromSeconds(10),
                        ReceiveTimeout = TimeSpan.FromSeconds(10),
                        OpenTimeout = TimeSpan.FromSeconds(10),
                        CloseTimeout = TimeSpan.FromSeconds(10)
                    };
                    rtuHost.AddServiceEndpoint("RealTimeUnit.IRealTimeUnitService", binding, "RTU");
                    rtuHost.Description.Behaviors.Remove(typeof(ServiceDebugBehavior));
                    rtuHost.Description.Behaviors.Add(
                        new ServiceDebugBehavior { IncludeExceptionDetailInFaults = true });
                    rtuHost.Open();
                    Console.WriteLine($"RTU Service is running at {baseAddress}RTU");
                    Console.WriteLine("Press [Enter] to stop the services.");
                    Console.ReadLine();
                    rtuHost.Close();
                    Environment.Exit(0);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                    rtuHost.Abort();
                    while (true)
                    {
                        Console.Write("Press [Enter] to close window.");
                        if (Console.ReadKey().Key == ConsoleKey.Enter) break;
                    }
                }
            }
        }

        private static void ConfigureService(IServiceCollection services, string baseAddress, double minValue,
            double maxValue)
        {
            services.AddSingleton(typeof(RealTimeUnit.RealTimeUnitService),
                new RealTimeUnit.RealTimeUnitService(baseAddress + "RTU", minValue, maxValue));
        }
    }
}