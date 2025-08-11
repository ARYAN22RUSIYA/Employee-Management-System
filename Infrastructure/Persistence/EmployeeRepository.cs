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
    /// Employee repository implementation using JwtContext.
    /// </summary>
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly JwtContext _context;
        public EmployeeRepository(JwtContext context)
        {
            _context = context;
        }

        public async Task<Employee> GetByIdAsync(Guid id)
        {
            return await _context.Employees.FindAsync(id);
        }

        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            return await _context.Employees.ToListAsync();
        }

        public async Task AddAsync(Employee entity)
        {
            await _context.Employees.AddAsync(entity);
        }

        public void Update(Employee entity)
        {
            _context.Employees.Update(entity);
        }

        public void Delete(Employee entity)
        {
            _context.Employees.Remove(entity);
        }

        public async Task<IEnumerable<Employee>> FindAsync(Expression<Func<Employee, bool>> predicate)
        {
            return await _context.Employees.Where(predicate).ToListAsync();
        }
    }
} 