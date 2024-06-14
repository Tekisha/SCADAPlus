using System.Collections.Generic;
using SCADA_Core.Models;

namespace SCADA_Core.Services.interfaces;

public interface IUserService
{
    bool RegisterUser(string username, string password);
    string LogIn(string username, string password);
    bool LogOut(string token);
    IEnumerable<User> GetAllUsers();
}