using System.Runtime.InteropServices;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Study_Project.Application.Commands;
using Study_Project.Application.Queries;
using Study_Project.Core.Entities;

namespace Study_Project.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EmployeesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Authorize(Policy = "UserPolicy")]
        [ProducesResponseType(typeof(List<Employee>), 200)]
        [ProducesResponseType(403)]
        public async Task<IActionResult> GetEmployees()
        {
            var employees = await _mediator.Send(new GetEmployeeListQuery());
            return Ok(employees);
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "UserPolicy")]
        [ProducesResponseType(typeof(Employee), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(403)]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            var employee = await _mediator.Send(new GetEmployeeByIdQuery(id));
            if (employee == null)
                return NotFound(new { message = "Employee not found" });

            return Ok(employee);
        }

        [HttpPost]
        [Authorize(Policy = "UserPolicy")]
        [ProducesResponseType(typeof(Employee), 201)]
        [ProducesResponseType(403)]
        public async Task<IActionResult> AddEmployee([FromBody] AddEmployeeCommand command)
        {
            var employee = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetEmployeeById), new { id = employee.Id }, employee);
        }

        [HttpPut("{id}")]
        [Authorize(Policy = "AdminPolicy")]
        [ProducesResponseType(typeof(Employee), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(403)]
        public async Task<IActionResult> UpdateEmployee(int id, [FromBody] UpdateEmployeeCommand command)
        {
            if (id != command.Id)
                return BadRequest("Employee ID mismatch.");

            var updatedEmployee = await _mediator.Send(command);
            if (updatedEmployee == null)
                return NotFound(new { message = "Employee not found" });

            return Ok(updatedEmployee);
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "AdminPolicy")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(403)]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var isDeleted = await _mediator.Send(new DeleteEmployeeCommand(id));
            if (!isDeleted)
                return NotFound(new { message = "Employee not found" });

            return Ok(new { message = "Employee deleted successfully" });
        }
    }
}
