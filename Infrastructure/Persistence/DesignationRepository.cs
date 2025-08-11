using Core.Entities;
using Core.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Infrastructure.Persistence
{
    /// <summary>
    /// Designation repository implementation using JwtContext.
    /// </summary>
    public class DesignationRepository : IDesignationRepository
    {
        private readonly JwtContext _context;
        public DesignationRepository(JwtContext context)
        {
            _context = context;
        }

        // Interface requirement (not used for Designation, which uses int as key)
        //public Task<Designation> GetByIdAsync(Guid id)
        //{
        //    throw new NotImplementedException("Use GetByIdAsync(int id) for Designation.");
        //}

        public async Task<Designation> GetByIdAsync(Guid id)
        {
            // Only return non-deleted designations
            return await _context.Designations.Include(d => d.Employees)
                .FirstOrDefaultAsync(d => d.Id == id && !d.IsDeleted);
        }

        public async Task<IEnumerable<Designation>> GetAllAsync()
        {
            // Only return non-deleted designations
            return await _context.Designations.Include(d => d.Employees)
                .Where(d => !d.IsDeleted).ToListAsync();
        }

        public async Task AddAsync(Designation entity)
        {
            await _context.Designations.AddAsync(entity);
        }

        public void Update(Designation entity)
        {
            _context.Designations.Update(entity);
        }

        public void Delete(Designation entity)
        {
            // Soft delete
            entity.IsDeleted = true;
            _context.Designations.Update(entity);
        }

        public async Task<IEnumerable<Designation>> FindAsync(Expression<Func<Designation, bool>> predicate)
        {
            // Only return non-deleted designations
            return await _context.Designations.Include(d => d.Employees)
                .Where(d => !d.IsDeleted).Where(predicate).ToListAsync();
        }
    }
} 