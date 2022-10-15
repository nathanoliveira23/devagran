using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Devagran.Dtos
{
    public class PublicationRequestDto
    {
        public string Description { get; set; }
        public IFormFile ImagePublication { get; set; }
    }
}