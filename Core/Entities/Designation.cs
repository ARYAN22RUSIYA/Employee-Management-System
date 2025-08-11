using System.Collections.Generic;

namespace Core.Entities
{
    /// <summary>
    /// Represents a job designation/title within the organization.
    /// </summary>
    public class Designation
    {
        /// <summary>
        /// Primary key.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Name of the designation.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Description of the designation.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Soft delete flag.
        /// </summary>
        public bool IsDeleted { get; set; } = false;

        /// <summary>
        /// Navigation property for employees with this designation.
        /// </summary>
        public ICollection<Employee> Employees { get; set; }
    }
} 