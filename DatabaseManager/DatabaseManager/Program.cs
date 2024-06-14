﻿using System;
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

        var tagDto = new TagDto
        {
            Id = "tagId1",
            Description = "Test Analog Input Tag",
            IoAddress = "AI1",
            Driver = "DefaultDriver",
            ScanTime = 1000,
            OnOffScan = true,
            LowLimit = 0,
            HighLimit = 100,
            Units = "Celsius",
            Alarms = true
        };

        tagServiceProxy.AddTag(tagDto);
        Console.WriteLine("Added new tag.");

        var tagValue = tagServiceProxy.GetTagValue("tagId1");
        Console.WriteLine($"Tag Value: {tagValue}");

        var isUserRegistered = userServiceProxy.RegisterUser("testuser", "password123");
        Console.WriteLine($"Registered new user. {isUserRegistered}");

        var token = userServiceProxy.LogIn("testuser", "password123");
        Console.WriteLine($"User validation result token: {token}");

        ((IClientChannel)tagServiceProxy).Close();
        ((IClientChannel)userServiceProxy).Close();

        tagServiceFactory.Close();
        userServiceFactory.Close();

        Console.WriteLine("Press [Enter] to exit.");
        Console.ReadLine();
    }
}