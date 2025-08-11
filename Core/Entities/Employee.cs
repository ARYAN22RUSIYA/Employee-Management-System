namespace Core.Entities
{
    public class Employee : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public DateTime Dob { get; set; }
        public DateTime JoiningDate { get; set; }
        public int Age { get; set; }
        public ICollection<Document> Documents { get; set; } = new List<Document>();

        /// <summary>
        /// Foreign key to Department.
        /// </summary>
        public int DepartmentId { get; set; }

        /// <summary>
        /// Navigation property for the employee's department.
        /// </summary>
        public Department Department { get; set; }

        /// <summary>
        /// Foreign key to Designation.
        /// </summary>
        public int DesignationId { get; set; }

        /// <summary>
        /// Navigation property for the employee's designation.
        /// </summary>
        public Designation Designation { get; set; }

        /// <summary>
        /// Soft delete flag for employee.
        /// </summary>
        public bool IsDeleted { get; set; } = false;
    }
}
