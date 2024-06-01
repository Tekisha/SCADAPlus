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

            var newTag = new AnalogInputTag
            {
                Id = "tagId1",
                Description = "Test Analog Input Tag",
                IOAddress = "AI1",
                Driver = "DefaultDriver",
                ScanTime = 1000,
                OnOffScan = true,
                LowLimit = 0,
                HighLimit = 100,
                Units = "Celsius",
                Alarms = true
            };

            tagServiceProxy.AddTag(newTag);
            Console.WriteLine("Added new tag.");

            double tagValue = tagServiceProxy.GetTagValue("tagId1");
            Console.WriteLine($"Tag Value: {tagValue}");

            var newUser = new User
            {
                Username = "testuser",
                Password = "password123"
            };

            userServiceProxy.RegisterUser(newUser);
            Console.WriteLine("Registered new user.");

            bool isValidUser = userServiceProxy.ValidateUser("testuser", "password123");
            Console.WriteLine($"User validation result: {isValidUser}");

            ((IClientChannel)tagServiceProxy).Close();
            ((IClientChannel)userServiceProxy).Close();

            tagServiceFactory.Close();
            userServiceFactory.Close();

            Console.WriteLine("Press [Enter] to exit.");
            Console.ReadLine();
        }
    }
}
