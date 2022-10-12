using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Devagran.Models;

namespace Devagran.Repository
{
    public interface IUserRepository
    {
        public void Save(User user);
        public bool EmailVerify(string email);
        public User GetUserLogin(string email, string password);
        public User GetUserById(int id);
        public void UpdateUser(User user);
  }
}