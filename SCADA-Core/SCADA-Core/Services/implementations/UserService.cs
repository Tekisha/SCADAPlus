using System.Collections.Generic;
using SCADA_Core.Models;
using SCADA_Core.Repositories.interfaces;
using SCADA_Core.Services.interfaces;
using SCADA_Core.Utilities;

namespace SCADA_Core.Services.implementations;

public class UserService(IUserRepository userRepository) : IUserService
{
    private static readonly Dictionary<string, User> AuthenticatedUsers = new();

    public bool RegisterUser(string username, string password)
    {
        var encryptedPassword = EncryptionHelper.EncryptPassword(password);
        var user = new User { Username = username, Password = encryptedPassword };
        return userRepository.RegisterUser(user);
    }

    public string LogIn(string username, string password)
    {
        var user = userRepository.GetUser(username);
        if (user == null || !EncryptionHelper.ValidatePassword(password, user.Password)) return null;

        var token = TokenGenerator.GenerateToken(username);
        AuthenticatedUsers[token] = user;
        return token;
    }

    public bool LogOut(string token)
    {
        return AuthenticatedUsers.Remove(token);
    }

    public bool ValidateToken(string token)
    {
        return AuthenticatedUsers.ContainsKey(token);
    }

    public IEnumerable<User> GetAllUsers()
    {
        return userRepository.GetAllUsers();
    }
}