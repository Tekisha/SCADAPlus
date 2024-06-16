using System;
using Microsoft.Extensions.DependencyInjection;
using SCADA_Core.Clients;
using SCADA_Core.Controllers.implementations;
using SCADA_Core.Repositories;
using SCADA_Core.Repositories.implementations;
using SCADA_Core.Repositories.interfaces;
using SCADA_Core.Services.implementations;
using SCADA_Core.Services.interfaces;
using SCADA_Core.TrendingService;
using SCADA_Core.Utilities;

namespace SCADA_Core;

internal class Program
{
    private static void Main()
    {
        SimulateTagInputs();
        // Create a service collection and configure dependencies
        var serviceCollection = new ServiceCollection();
        ConfigureServices(serviceCollection);

        // Build the service provider
        var serviceProvider = serviceCollection.BuildServiceProvider();

        // Load configuration from the XML file
        var configData = ConfigManager.LoadConfig();

        // Resolve the services and apply configuration
        var userService = serviceProvider.GetService<IUserService>();
        var tagService = serviceProvider.GetService<ITagService>();
        ConfigManager.ApplyConfigurationSettings(configData);

        // Use the service provider to create the WCF service host
        var tagServiceHostFactory = new DIServiceHostFactory(serviceProvider);
        var userServiceHostFactory = new DIServiceHostFactory(serviceProvider);

        using (var tagHost =
               tagServiceHostFactory.CreateServiceHost("SCADA_Core.Controllers.implementations.TagController",
                   Array.Empty<Uri>()))
        using (var userHost =
               userServiceHostFactory.CreateServiceHost("SCADA_Core.Controllers.implementations.UserController",
                   Array.Empty<Uri>()))
        {
            try
            {
                tagHost.Open();
                userHost.Open();
                Console.WriteLine("SCADA Tag Service is running...");
                Console.WriteLine("SCADA User Service is running...");
                Console.WriteLine("Press [Enter] to stop the services.");
                Console.ReadLine();
                tagHost.Close();
                userHost.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                tagHost.Abort();
                userHost.Abort();
                while (true)
                {
                    Console.WriteLine("Press [Enter] to close window.");
                    if (Console.ReadKey().Key == ConsoleKey.Enter) break;
                }
            }
        }

        // Save configuration settings back to the XML file
        ConfigManager.SaveConfig(configData);
    }

    private static void SimulateTagInputs()
    {
        for (var i = 0; i < 10; i++)
        {
            var tagValue = new TagValue
            {
                TagName = $"Tag{i}",
                Value = i * 10,
                Timestamp = DateTime.Now
            };
            TagValueProcessor.ProcessTagValue(tagValue);
        }
    }

    private static void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<ScadaDbContext>();
        services.AddScoped<ITagRepository, TagRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ITagValueRepository, TagValueRepository>();
        services.AddScoped<ITagService, TagService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<TagController>(); // Register the controller itself
        services.AddScoped<UserController>(); // Register the controller itself
    }
}