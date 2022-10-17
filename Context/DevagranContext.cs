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
        public DbSet<Follower> Followers { get; set; }
        public DbSet<Publication> Publications { get; set; }
        public DbSet<PublicationComments> Comments { get; set; }
    }
}