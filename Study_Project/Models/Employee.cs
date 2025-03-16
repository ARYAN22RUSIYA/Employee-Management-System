namespace Study_Project.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Dob { get; set; }  // Updated field
        public DateTime JoiningDate { get; set; }  // Updated field
        public int Age { get; set; }  // Updated field
        public DateTime CreatedOn { get; set; }  // New field
        public DateTime UpdatedOn { get; set; }  // New field
    }
}
