using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class JwtContext : IdentityDbContext<IdentityUser>
    {
        public JwtContext(DbContextOptions<JwtContext> options) : base(options) { }

        public DbSet<Core.Entities.Employee> Employees { get; set; }
    }
}
