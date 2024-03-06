using Microsoft.EntityFrameworkCore;
using UserLogin.Models;

namespace UserLogin.Data
{
    public class UserContext:DbContext
    {
        public DbSet<User> Users {get; set;}
        public UserContext(DbContextOptions<UserContext> options) : base(options) { 

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity => { entity.HasIndex(e => e.Email).IsUnique(); });
        }
    }
}
