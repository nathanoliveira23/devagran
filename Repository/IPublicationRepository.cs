using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Devagran.Models;

namespace Devagran.Repository
{
    public interface IPublicationRepository
    {
        public void AddPublication(Publication publication);
    }
}