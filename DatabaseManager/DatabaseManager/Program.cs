using DatabaseManager.TagService;
using DatabaseManager.UserService;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseManager
{
    class Program
    {
        static void Main(string[] args)
        {
            string tagServiceBaseAddress = "http://localhost:8733/SCADA/TagController/";
            string userServiceBaseAddress = "http://localhost:8733/SCADA/UserController/";

            var binding = new BasicHttpBinding();

            var tagServiceEndpoint = new EndpointAddress(tagServiceBaseAddress);
            var userServiceEndpoint = new EndpointAddress(userServiceBaseAddress);

            var tagServiceFactory = new ChannelFactory<ITagController>(binding, tagServiceEndpoint);
            var userServiceFactory = new ChannelFactory<IUserController>(binding, userServiceEndpoint);

            ITagController tagServiceProxy = tagServiceFactory.CreateChannel();
            IUserController userServiceProxy = userServiceFactory.CreateChannel();

            tagServiceProxy.AddTag("tagId1", "Test Analog Input Tag", "AI1", "DefaultDriver", 1000, true, 0, 100, "Celsius", true);
            Console.WriteLine("Added new tag.");

            double tagValue = tagServiceProxy.GetTagValue("tagId1");
            Console.WriteLine($"Tag Value: {tagValue}");

            userServiceProxy.RegisterUser("testuser", "password123");
            Console.WriteLine("Registered new user.");

            bool isLoggedIn = userServiceProxy.LogIn("testuser", "password123");
            Console.WriteLine($"User validation result: {isLoggedIn}");

            ((IClientChannel)tagServiceProxy).Close();
            ((IClientChannel)userServiceProxy).Close();

            tagServiceFactory.Close();
            userServiceFactory.Close();

            Console.WriteLine("Press [Enter] to exit.");
            Console.ReadLine();
        }
    }
}
