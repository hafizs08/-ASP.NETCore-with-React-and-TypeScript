using Microsoft.EntityFrameworkCore;
using microservices.Models;

namespace microservices.Data
{
    public class UserAppServiceContext : DbContext
    {
        public UserAppServiceContext(DbContextOptions<UserAppServiceContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}
