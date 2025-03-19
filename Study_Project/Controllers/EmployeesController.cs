using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Study_Project.Interfaces;
using Study_Project.Models;
using System.Collections.Generic;

namespace Study_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeesController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        [Authorize(Policy = "UserPolicy")]
        [ProducesResponseType(typeof(List<Employee>), 200)]
        [ProducesResponseType(403)]
        public List<Employee> GetEmployees()
        {
            return _employeeService.GetEmployeeDetails();
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "UserPolicy")]
        [ProducesResponseType(typeof(Employee), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(403)]
        public IActionResult GetEmployeeById(int id)
        {
            var employee = _employeeService.GetEmployeeById(id);
            if (employee == null)
                return NotFound(new { message = "Employee not found" });

            return Ok(employee);
        }

        [HttpPost]
        [Authorize(Policy = "UserPolicy")]
        [ProducesResponseType(typeof(Employee), 201)]
        [ProducesResponseType(403)]
        public Employee AddEmployee([FromBody] Employee emp)
        {
            return _employeeService.AddEmployee(emp);
        }

        [HttpPut("{id}")]
        [Authorize(Policy = "AdminPolicy")]
        [ProducesResponseType(typeof(Employee), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(403)]
        public IActionResult UpdateEmployee(int id, [FromBody] Employee emp)
        {
            var updatedEmployee = _employeeService.UpdateEmployee(id, emp);
            if (updatedEmployee == null)
                return NotFound(new { message = "Employee not found" });

            return Ok(updatedEmployee);
        }

        
        [HttpDelete("{id}")]
        [Authorize(Policy = "AdminPolicy")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(403)]
        public IActionResult DeleteEmployee(int id)
        {
            var isDeleted = _employeeService.DeleteEmployee(id);
            if (!isDeleted)
                return NotFound(new { message = "Employee not found" });

            return Ok(new { message = "Employee deleted successfully" });
        }
    }
}
