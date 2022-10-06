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
  }
}