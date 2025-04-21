using System.Security.Claims;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Infrastructure.Helpers
{
    public static class AuditHelper
    {
        public static void ApplyAuditInfo(ChangeTracker changeTracker, ClaimsPrincipal? user)
        {
            var userId = GetUserId(user);

            foreach (var entry in changeTracker.Entries<BaseEntity>())
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedOn = DateTime.UtcNow;
                    entry.Entity.CreatedBy = userId;
                    entry.Entity.UpdatedOn = DateTime.UtcNow;
                    entry.Entity.UpdatedBy = userId;
                }
                else if (entry.State == EntityState.Modified)
                {
                    entry.Entity.UpdatedOn = DateTime.UtcNow;
                    entry.Entity.UpdatedBy = userId;
                }
            }
        }

        private static string GetUserId(ClaimsPrincipal? user)
        {
            return user?.FindFirst(ClaimTypes.NameIdentifier)?.Value
                ?? user?.FindFirst("sub")?.Value  // fallback for JWT tokens
                ?? "System";
        }
    }
}
