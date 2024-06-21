using System.Collections.Generic;
using SCADA_Core.Models;

namespace SCADA_Core.Repositories.interfaces;

public interface IUserRepository
{
    bool RegisterUser(User user);
    IEnumerable<User> GetAllUsers();
    public User GetUser(string username);
}