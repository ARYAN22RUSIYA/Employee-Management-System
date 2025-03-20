using Core.Entities;

namespace Study_Project.Interfaces
{
    public interface IEmployeeService
    {
        List<Employee> GetEmployeeDetails();
        Employee GetEmployeeById(int id);  
        Employee AddEmployee(Employee employee);
        Employee UpdateEmployee(int id, Employee employee);
        bool DeleteEmployee(int id);
    }
}
