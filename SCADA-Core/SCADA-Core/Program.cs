using Microsoft.Extensions.DependencyInjection;
using SCADA_Core.Controllers.implementations;
using SCADA_Core.Models;
using SCADA_Core.Repositories.implementations;
using SCADA_Core.Repositories.interfaces;
using SCADA_Core.Repositories;
using SCADA_Core.Services.implementations;
using SCADA_Core.Services.interfaces;
using SCADA_Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace SCADA_Core
{
    class Program
    {
        static void Main()
        {

            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            var serviceProvider = serviceCollection.BuildServiceProvider();

            var configData = ConfigManager.LoadConfig();

            ConfigManager.ApplyConfigurationSettings(configData);

            var userService = serviceProvider.GetService<IUserService>();
            var tagService = serviceProvider.GetService<ITagService>();
            var tagControllerInstance = new TagController(tagService);
            var userControllerInstance = new UserController(userService);

            
            using (ServiceHost tagHost = new ServiceHost(tagControllerInstance))
            using (ServiceHost userHost = new ServiceHost(userControllerInstance))
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
                        if (Console.ReadKey().Key == ConsoleKey.Enter)
                        {
                            break;
                        }
                    }
                }
            }

            // Save configuration settings back to the XML file
            ConfigManager.SaveConfig(configData);
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<ScadaDbContext>();
            services.AddScoped<ITagRepository, TagRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ITagService, TagService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<TagController>();
            services.AddScoped<UserController>();
        }
    }
}
