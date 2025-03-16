using Study_Project.Models;
using System.Collections.Generic;

namespace Study_Project.Interfaces
{
    public interface IEmployeeService
    {
        List<Employee> GetEmployeeDetails();
        Employee GetEmployeeById(int id);  // ✅ New: Fetch employee by ID
        Employee AddEmployee(Employee employee);
        Employee UpdateEmployee(int id, Employee employee);
        bool DeleteEmployee(int id);
    }
}
