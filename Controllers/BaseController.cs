using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Devagran.Repository;
using Devagran.Models;
using System.Security.Claims;

namespace Devagran.Controllers
{
    [Authorize]
    public class BaseController : ControllerBase
    {
        public BaseController(IUserRepository userRepository) => _userRepository = userRepository;
        protected readonly IUserRepository _userRepository;

        protected User ReadToken()
        {
            string userId = User.Claims.Where(c => c.Type == ClaimTypes.Sid).Select(c => c.Value).FirstOrDefault();

            if (String.IsNullOrEmpty(userId))
            {
                return null;
            }
            else
            {
                return _userRepository.GetUserById(int.Parse(userId));
            }
        }
    }
}