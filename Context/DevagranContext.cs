using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Devagran.Models;
using Microsoft.EntityFrameworkCore;

namespace Devagran.Context
{
    public class DevagranContext : DbContext
    {
        public DevagranContext(DbContextOptions<DevagranContext> options) : base(options)
        {
            
        }

        public DbSet<User> Users { get; set; }
    }
}