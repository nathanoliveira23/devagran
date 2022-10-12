using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Devagran.Context;
using Devagran.Models;

namespace Devagran.Repository.Impl
{
  public class UserRepositoryImpl : IUserRepository
  {
      public UserRepositoryImpl(DevagranContext context) => _context = context;
      private readonly DevagranContext _context;

      public void Save(User user)
      {
          _context.Users.Add(user);
          _context.SaveChanges();
      }

      public bool EmailVerify(string email)
      {
          return _context.Users.Any(x => x.Email == email);
      }

      public User GetUserLogin(string email, string password)
      {
          return _context.Users.FirstOrDefault(x => x.Email == email && x.Password == password);
      }

      public User GetUserById(int id)
      {
          return _context.Users.FirstOrDefault(x => x.Id == id);
      }

      public void UpdateUser(User user)
      {
          _context.Users.Update(user);
          _context.SaveChanges();
      }
  }
}