using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Devagran.Dtos
{
    public class ImageDto
    {
        public string Title { get; set; }
        public IFormFile Image { get; set; }
    }
}