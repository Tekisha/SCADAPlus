using System.Collections.Generic;
using System.Linq;
using SCADA_Core.Models;
using SCADA_Core.Repositories.interfaces;

namespace SCADA_Core.Repositories.implementations;

public class UserRepository(ScadaDbContext dbContext) : IUserRepository
{
    public void RegisterUser(User user)
    {
        if (dbContext.Users.Any(u => u.Username == user.Username)) return;
        dbContext.Users.Add(user);
        dbContext.SaveChanges();
    }

    public bool ValidateUser(string username, string password)
    {
        return dbContext.Users.Any(u => u.Username == username && u.Password == password);
    }

    public IEnumerable<User> GetAllUsers()
    {
        return dbContext.Users.ToList();
    }
}