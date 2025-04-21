using Core.Entities;
using Infrastructure.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

public class JwtContext : IdentityDbContext<IdentityUser>
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public JwtContext(DbContextOptions<JwtContext> options, IHttpContextAccessor httpContextAccessor)
        : base(options)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public DbSet<Employee> Employees { get; set; }
    public DbSet<Document> Documents { get; set; }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        AuditHelper.ApplyAuditInfo(ChangeTracker, _httpContextAccessor.HttpContext?.User);
        return await base.SaveChangesAsync(cancellationToken);
    }
}
