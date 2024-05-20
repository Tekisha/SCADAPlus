using SCADA_Core.Controllers.implementations;
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
            var (userRepository, tagRepository) = ConfigManager.LoadConfig();

            using (ServiceHost tagHost = new ServiceHost(typeof(TagController)))
            using (ServiceHost userHost = new ServiceHost(typeof(UserController)))
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

            ConfigManager.SaveConfig(userRepository, tagRepository);
        }
    }
}
