using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Devagran.Dtos
{
    public class LoginResponseDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
    }
}