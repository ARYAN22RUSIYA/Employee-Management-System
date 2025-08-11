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

    /// <summary>
    /// Departments in the organization.
    /// </summary>
    public DbSet<Department> Departments { get; set; }

    /// <summary>
    /// Designations in the organization.
    /// </summary>
    public DbSet<Designation> Designations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure Employee -> Department relationship (required, no cascade delete)
        modelBuilder.Entity<Employee>()
            .HasOne(e => e.Department)
            .WithMany(d => d.Employees)
            .HasForeignKey(e => e.DepartmentId)
            .OnDelete(DeleteBehavior.Restrict);

        // Configure Employee -> Designation relationship (required, no cascade delete)
        modelBuilder.Entity<Employee>()
            .HasOne(e => e.Designation)
            .WithMany(d => d.Employees)
            .HasForeignKey(e => e.DesignationId)
            .OnDelete(DeleteBehavior.Restrict);

        // Optionally, filter out soft-deleted entities in queries (to be handled in repositories/queries)
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        AuditHelper.ApplyAuditInfo(ChangeTracker, _httpContextAccessor.HttpContext?.User);
        return await base.SaveChangesAsync(cancellationToken);
    }
}
