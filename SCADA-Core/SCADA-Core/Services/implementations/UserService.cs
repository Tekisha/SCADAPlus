using SCADA_Core.Repositories.implementations;
using SCADA_Core.Repositories.interfaces;
using SCADA_Core.Services.interfaces;
using SCADA_Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCADA_Core.Services.implementations
{
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
}
