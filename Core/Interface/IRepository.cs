using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Core.Interface
{
    /// <summary>
    /// Generic repository interface for basic CRUD operations.
    /// </summary>
    /// <typeparam name="T">Entity type</typeparam>
    public interface IRepository<T> where T : class
    {
        Task<T> GetByIdAsync(Guid id); // Get entity by ID
        Task<IEnumerable<T>> GetAllAsync(); // Get all entities
        Task AddAsync(T entity); // Add a new entity
        void Update(T entity); // Update an entity
        void Delete(T entity); // Delete an entity
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate); // Find entities by predicate
    }
} 