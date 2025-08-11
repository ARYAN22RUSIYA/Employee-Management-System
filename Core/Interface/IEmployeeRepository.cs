using Core.Entities;
using System.Threading.Tasks;

namespace Core.Interface
{
    /// <summary>
    /// Employee-specific repository interface.
    /// </summary>
    public interface IEmployeeRepository : IRepository<Employee>
    {
        // Add employee-specific methods here if needed
    }
} 