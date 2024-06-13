using System;
using System.ServiceModel;
using DatabaseManager.TagService;
using DatabaseManager.UserService;

namespace DatabaseManager;

internal class Program
{
    private static void Main()
    {
        const string tagServiceBaseAddress = "http://localhost:8733/SCADA/TagController/";
        const string userServiceBaseAddress = "http://localhost:8733/SCADA/UserController/";

        var binding = new BasicHttpBinding();

        var tagServiceEndpoint = new EndpointAddress(tagServiceBaseAddress);
        var userServiceEndpoint = new EndpointAddress(userServiceBaseAddress);

        var tagServiceFactory = new ChannelFactory<ITagController>(binding, tagServiceEndpoint);
        var userServiceFactory = new ChannelFactory<IUserController>(binding, userServiceEndpoint);

        var tagServiceProxy = tagServiceFactory.CreateChannel();
        var userServiceProxy = userServiceFactory.CreateChannel();

        tagServiceProxy.AddTag("tagId1", "Test Analog Input Tag", "AI1", "DefaultDriver", 1000, true, 0, 100,
            "Celsius", true);
        Console.WriteLine("Added new tag.");

        var tagValue = tagServiceProxy.GetTagValue("tagId1");
        Console.WriteLine($"Tag Value: {tagValue}");

        userServiceProxy.RegisterUser("testuser", "password123");
        Console.WriteLine("Registered new user.");

        var isLoggedIn = userServiceProxy.LogIn("testuser", "password123");
        Console.WriteLine($"User validation result: {isLoggedIn}");

        ((IClientChannel)tagServiceProxy).Close();
        ((IClientChannel)userServiceProxy).Close();

        tagServiceFactory.Close();
        userServiceFactory.Close();

        Console.WriteLine("Press [Enter] to exit.");
        Console.ReadLine();
    }
}