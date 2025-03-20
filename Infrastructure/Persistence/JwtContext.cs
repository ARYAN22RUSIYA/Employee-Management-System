using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Study_Project.Models;

namespace Study_Project.Context
{
    public class JwtContext : IdentityDbContext<IdentityUser>
    {
        public JwtContext(DbContextOptions options) : base(options) { }
        public DbSet<Employee> Employees { get; set; }
    }
}
