using ScadaPlus.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCADA_Core.Repositories.interfaces
{
    public interface IUserRepository
    {
        void RegisterUser(User user);
        bool ValidateUser(string username, string password);
        IEnumerable<User> GetAllUsers();
    }
}
