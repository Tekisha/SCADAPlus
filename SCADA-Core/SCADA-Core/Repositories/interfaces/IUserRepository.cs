using System.Collections.Generic;
using SCADA_Core.Models;

namespace SCADA_Core.Repositories.interfaces;

public interface IUserRepository
{
    void RegisterUser(User user);
    bool ValidateUser(string username, string password);
    IEnumerable<User> GetAllUsers();
}