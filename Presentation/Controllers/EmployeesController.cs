using Study_Project.Application.DTOs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Study_Project.Application.Features.Employees.Commands.CreateEmployee;
using Study_Project.Application.Features.Employees.Commands.DeleteEmployee;
using Study_Project.Application.Features.Employees.Commands.UpdateEmployee;
using Study_Project.Application.Features.Employees.Queries.GetEmployeeList;
using Study_Project.Application.Features.Employees.Queries.GetEmployeeById;
using Study_Project.Application.Features.Documents.Commands.UploadDocument;
using Study_Project.Application.Features.Documents.Queries;
using Microsoft.AspNetCore.Identity;
using Application.Features.Documents.Events;
using Core.Interface;

namespace Study_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly UserManager<IdentityUser> _userManager;

        public EmployeesController(IMediator mediator, UserManager<IdentityUser> userManager)
        {
            _mediator = mediator;
            _userManager = userManager;
        }

        [HttpGet]
        [Authorize(Policy = "UserPolicy")]
        [ProducesResponseType(typeof(List<EmployeeDto>), 200)]
        [ProducesResponseType(403)]
        public async Task<IActionResult> GetEmployees()
        {
            var employees = await _mediator.Send(new GetEmployeeListQuery());
            return Ok(employees);
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "UserPolicy")]
        [ProducesResponseType(typeof(EmployeeDto), 200)]
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
        [ProducesResponseType(typeof(EmployeeDto), 201)]
        [ProducesResponseType(403)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> AddEmployee([FromBody] CreateEmployeeCommand command)
        {
            var createdEmployee = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetEmployeeById), new { id = createdEmployee.Id }, createdEmployee);
        }


        [HttpPost("upload-document")]
        [Authorize(Policy = "UserPolicy")]
        [ProducesResponseType(typeof(UploadDocumentDto), 201)]
        [ProducesResponseType(403)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> UploadDocument([FromForm] UploadDocumentDto dto)
        {
            var command = new UploadDocumentCommand
            {
                EmployeeId = dto.EmployeeId,
                File = dto.File
            };

            var result = await _mediator.Send(command);

            // Fetch employee details
            var employee = await _mediator.Send(new GetEmployeeByIdQuery(dto.EmployeeId));
            if (employee == null)
                return NotFound(new { message = "Employee not found" });

            // Fetch username from claims
            var username = User.Identity?.Name;
            if (string.IsNullOrEmpty(username))
                return Unauthorized(new { message = "User identity not found in claims" });

            // Fetch email from Identity using username
            var identityUser = await _userManager.FindByNameAsync(username);
            if (identityUser == null)
                return NotFound(new { message = "User not found in Identity" });

            // Publish notification for email
            await _mediator.Publish(new DocumentUploadedNotification(
                employee.Id,
                identityUser.Email,
                employee.Name,
                dto.File.FileName
            ));

            return Ok(result);
        }


        [HttpGet("download-document")]
        [Authorize(Policy = "UserPolicy")]
        [ProducesResponseType(typeof(FileDownloadDto), 201)]
        [ProducesResponseType(403)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> DownloadDocument(int employeeId, [FromQuery] string fileName)
        {
            var query = new GetEmployeeDocumentQuery
            {
                EmployeeId = employeeId,
                FileName = fileName
            };

            var result = await _mediator.Send(query);
            return File(result.FileContent, result.ContentType, result.FileName);
        }

        [HttpGet("documents/{employeeId}")]
        public async Task<IActionResult> GetDocumentsByEmployee(int employeeId)
        {
            var result = await _mediator.Send(new GetDocumentsByEmployeeQuery(employeeId));
            return Ok(result);
        }

        [HttpPut("{id}")]
        [Authorize(Policy = "AdminPolicy")]
        [ProducesResponseType(typeof(EmployeeDto), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(403)]
        public async Task<IActionResult> UpdateEmployee(int id, [FromBody] UpdateEmployeeCommand command)
        {
            command.Id = id;
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
            var result = await _mediator.Send(new DeleteEmployeeCommand(id));
            if (!result)
                return NotFound(new { message = "Employee not found" });

            return Ok(new { message = "Employee deleted successfully" });
        }


        [HttpPost("test-sendgrid")]
        public async Task<IActionResult> TestSendGrid([FromServices] IEmailService emailService)
        {
            await emailService.SendEmailAsync(
                "your@email.com",
                "Test Subject",
                "Test body",
                null,
                "your-template-id",
                new { name = "Test", fileName = "Test.txt" }
            );
            return Ok("Sent");
        }

    }
}
