using System.Collections.Generic;
using SCADA_Core.Models;
using SCADA_Core.Repositories.interfaces;
using SCADA_Core.Services.interfaces;

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
        var user = new User { Username = username, Password = password };
        userRepository.RegisterUser(user);
        return true;
    }

    public bool LogIn(string username, string password)
    {
        return userRepository.ValidateUser(username, password);
    }

    public IEnumerable<User> GetAllUsers()
    {
        return userRepository.GetAllUsers();
    }
}