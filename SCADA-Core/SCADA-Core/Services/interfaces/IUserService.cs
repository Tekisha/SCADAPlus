using ScadaPlus.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace SCADA_Core.Services.interfaces
{
    public interface IUserService
    {
        bool RegisterUser(string username, string password);
        bool LogIn(string username, string password);
        IEnumerable<User> GetAllUsers();
    }
}
