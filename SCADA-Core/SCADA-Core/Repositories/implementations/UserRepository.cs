using SCADA_Core.Repositories.interfaces;
using ScadaPlus.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCADA_Core.Repositories.implementations
{
    public class UserRepository : IUserRepository
    {
        private List<User> users = new List<User>();

        public void RegisterUser(User user)
        {
            if (!users.Exists(u => u.Username == user.Username))
            {
                users.Add(user);
            }
        }

        public bool ValidateUser(string username, string password)
        {
            return users.Exists(u => u.Username == username && u.Password == password);
        }

        public IEnumerable<User> GetAllUsers()
        {
            return users;
        }
    }
}
