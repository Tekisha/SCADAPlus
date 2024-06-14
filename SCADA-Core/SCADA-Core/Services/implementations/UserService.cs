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

    public bool LogIn(string username, string password)
    {
        var user = userRepository.GetUser(username);
        if (user == null) return false;
        return EncryptionHelper.ValidatePassword(password, user.Password);
    }

    public IEnumerable<User> GetAllUsers()
    {
        return userRepository.GetAllUsers();
    }
}