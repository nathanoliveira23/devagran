using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Devagran.Dtos
{
    public class UserRequestDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public IFormFile ImageProfile { get; set; }
    }
}