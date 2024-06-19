using Microsoft.Extensions.DependencyInjection;
using System;
using System.ComponentModel;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Threading;


namespace RealTimeUnit
{
    internal class Program
    {
        private static void Main()
        {

            Console.WriteLine("Enter port");
            int port = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter min value");
            double minValue = double.Parse(Console.ReadLine());
            Console.WriteLine("Enter max value");
            double maxValue = double.Parse(Console.ReadLine());

            while(maxValue <= minValue)
            {
                Console.WriteLine("Max value must be greater than min value");
                Console.WriteLine("Enter min value");
                minValue = double.Parse(Console.ReadLine());
                Console.WriteLine("Enter max value");
                maxValue = double.Parse(Console.ReadLine());
            }

            string baseAddress = $"http://localhost:{port}/";
            // Create a service collection and configure dependencies
            var serviceCollection = new ServiceCollection();
            ConfigureService(serviceCollection, baseAddress, minValue, maxValue);

            // Build the service provider
            var serviceProvider = serviceCollection.BuildServiceProvider();



            // Use the service provider to create the WCF service host
            var realTimeUnitServiceHostFactory = new DIServiceHostFactory(serviceProvider);

            using (var rtuHost =
                   realTimeUnitServiceHostFactory.CreateServiceHost("RealTimeUnit.RealTimeUnitService",
                       new Uri[] { new Uri(baseAddress) }))
            {
                try
                {
                    WSDualHttpBinding binding = new WSDualHttpBinding();
                    binding.SendTimeout = TimeSpan.FromSeconds(10);
                    binding.ReceiveTimeout = TimeSpan.FromSeconds(10);
                    binding.OpenTimeout = TimeSpan.FromSeconds(10);
                    binding.CloseTimeout = TimeSpan.FromSeconds(10);
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
                        Console.WriteLine("Press [Enter] to close window.");
                        if (Console.ReadKey().Key == ConsoleKey.Enter) break;
                    }
                }
            }
        }

        private static void ConfigureService(IServiceCollection services, string baseAddress, double minValue, double maxValue)
        {
            services.AddSingleton(typeof(RealTimeUnitService), new RealTimeUnitService(baseAddress + "RTU", minValue, maxValue));
        }

    }
}
