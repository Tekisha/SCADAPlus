using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using SCADA_Core.Models;
using SCADA_Core.Repositories.interfaces;
using SCADA_Core.Services.interfaces;
using SCADA_Core.Utilities;

namespace SCADA_Core.Services.implementations;

public class UserService : IUserService
{
    private readonly IUserRepository userRepository;
    private static Dictionary<string, User> authenticatedUsers = new Dictionary<string, User>();

    public UserService(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    public bool RegisterUser(string username, string password)
    {
        string encryptedPassword = EncryptionHelper.EncryptPassword(password);
        var user = new User { Username = username, Password = encryptedPassword };
        return userRepository.RegisterUser(user);
    }

    public string LogIn(string username, string password)
    {
        var user = userRepository.GetUser(username);
        if (user == null || !EncryptionHelper.ValidatePassword(password, user.Password)) return null; 

        string token = TokenGenerator.GenerateToken(username);
        authenticatedUsers[token] = user;
        return token;
    }
    public bool LogOut(string token)
    {
        return authenticatedUsers.Remove(token);
    }

    public bool ValidateToken(string token)
    {
        return authenticatedUsers.ContainsKey(token);
    }

    public IEnumerable<User> GetAllUsers()
    {
        return userRepository.GetAllUsers();
    }
}