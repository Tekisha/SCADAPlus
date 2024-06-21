using System.Collections.Generic;
using System.Linq;
using SCADA_Core.Models;
using SCADA_Core.Repositories.interfaces;

namespace SCADA_Core.Repositories.implementations;

public class UserRepository(ScadaDbContext dbContext) : IUserRepository
{
    public bool RegisterUser(User user)
    {
        if (dbContext.Users.Any(u => u.Username == user.Username)) return false;
        dbContext.Users.Add(user);
        dbContext.SaveChanges();
        return true;
    }

    public User GetUser(string username)
    {
        return dbContext.Users.SingleOrDefault(u => u.Username == username);
    }

    public IEnumerable<User> GetAllUsers()
    {
        return dbContext.Users.ToList();
    }
}