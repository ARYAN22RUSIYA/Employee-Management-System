using System.Collections.Generic;

namespace Core.Entities
{
    /// <summary>
    /// Represents a department within the organization.
    /// </summary>
    public class Department
    {
        /// <summary>
        /// Primary key.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Name of the department.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Description of the department.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Number of non-deleted employees in the department.
        /// </summary>
        public int People { get; set; }

        /// <summary>
        /// Soft delete flag.
        /// </summary>
        public bool IsDeleted { get; set; } = false;

        /// <summary>
        /// Navigation property for employees in this department.
        /// </summary>
        public ICollection<Employee> Employees { get; set; }
    }
} 