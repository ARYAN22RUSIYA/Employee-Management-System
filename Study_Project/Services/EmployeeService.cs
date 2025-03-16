using System.Linq;
using System.Collections.Generic;
using Study_Project.Context;
using Study_Project.Interfaces;
using Study_Project.Models;

namespace Study_Project.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly JwtContext _context;

        public EmployeeService(JwtContext context)
        {
            _context = context;
        }

        public List<Employee> GetEmployeeDetails()
        {
            return _context.Employees.ToList();
        }

        // ✅ New: Fetch Employee by ID
        public Employee GetEmployeeById(int id)
        {
            return _context.Employees.FirstOrDefault(e => e.Id == id);
        }

        public Employee AddEmployee(Employee employee)
        {
            employee.CreatedOn = DateTime.UtcNow;
            employee.UpdatedOn = DateTime.UtcNow;

            var emp = _context.Employees.Add(employee);
            _context.SaveChanges();
            return emp.Entity;
        }

        public Employee UpdateEmployee(int id, Employee updatedEmployee)
        {
            var employee = _context.Employees.FirstOrDefault(e => e.Id == id);
            if (employee == null) return null;

            employee.Name = updatedEmployee.Name;
            employee.Dob = updatedEmployee.Dob;
            employee.JoiningDate = updatedEmployee.JoiningDate;
            employee.Age = updatedEmployee.Age;
            employee.UpdatedOn = DateTime.UtcNow;

            _context.SaveChanges();
            return employee;
        }

        public bool DeleteEmployee(int id)
        {
            var employee = _context.Employees.FirstOrDefault(e => e.Id == id);
            if (employee == null) return false;

            _context.Employees.Remove(employee);
            _context.SaveChanges();
            return true;
        }
    }
}
