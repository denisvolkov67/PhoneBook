using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NJsonSchema.Annotations;
using NSwag.Annotations;
using PhoneBook.Logic.Command;
using PhoneBook.Logic.Models;
using PhoneBook.Logic.Queries;

namespace PhoneBook.Web.Controllers
{
    [ApiController]
    public class EmployeesController : Controller
    {
        private readonly IMediator _mediator;
        private readonly ILogger<EmployeesController> _logger;

        public EmployeesController(IMediator mediator, ILogger<EmployeesController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet("employee/{id}")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(Employee), Description = "Success")]
        [SwaggerResponse(HttpStatusCode.NotFound, typeof(void), Description = "Employee not found")]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(void), Description = "Invalid data")]
        public async Task<IActionResult> GetEmployeeByIdAsync(int id)
        {
            if (id <= 0)
            {
                _logger.LogError($"Incorrect value for the employee's Id was set. '{id}' <= 0.");
                return BadRequest();
            }
            var employees = await _mediator.Send(new GetEmployeeById(id));

            return employees.HasValue ? (IActionResult)Ok(employees.Value) : NotFound();
        }

        [HttpGet("employees/department/{id}")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(IEnumerable<Employee>), Description = "Success")]
        [SwaggerResponse(HttpStatusCode.NotFound, typeof(void), Description = "Employee not found")]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(void), Description = "Invalid data")]
        public async Task<IActionResult> GetEmployeesByDepartmentIdAsync(string id)
        {
            if (String.IsNullOrEmpty(id))
            {
                _logger.LogError($"Incorrect value for the employee's DepartmentId was set. '{id}' - is null...");
                return BadRequest();
            }

            string login = HttpContext.User.Identity.Name;
            if (login.Contains(@"BTRC\"))
                login = login.Substring(5);

            var employees = await _mediator.Send(new GetEmployeesByDepartmentId(id, login));

            return employees.HasValue ? (IActionResult)Ok(employees.Value) : NotFound();
        }
      
        [HttpGet("employees/{name}")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(IEnumerable<Employee>), Description = "Success")]
        [SwaggerResponse(HttpStatusCode.NotFound, typeof(void), Description = "Employees not found")]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(void), Description = "Invalid data")]
        public async Task<IActionResult> GetEmployeesByNameAsync(string name)
        {
            string login = HttpContext.User.Identity.Name;
            if (login.Contains(@"BTRC\"))
                login = login.Substring(5);

            var employees = await _mediator.Send(new GetEmployeesByName(name, login));

            return employees.HasValue ? (IActionResult)Ok(employees.Value) : NotFound();
        }

        [HttpPost("employee")]
        [Authorize(Roles = "Phonebook_Edit")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(Employee), Description = "Success")]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(void), Description = "Invalid data")]
        public async Task<IActionResult> CreateEmployee([FromBody, NotNull, CustomizeValidator(RuleSet = "PreValidationEmployee")] CreateEmployeeCommand model)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid model for create a new employee was used.");
                return BadRequest();
            }

            var result = await _mediator.Send(model);

            return result.IsFailure ? (IActionResult)BadRequest(result.Error) : Ok(result.Value); //FP is here           
        }

        [HttpPost("employee/update")]
        [Authorize(Roles = "Phonebook_Edit")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(UpdateEmployeeCommand), Description = "Success")]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(void), Description = "Invalid data")]
        public async Task<IActionResult> EditEmployee([FromBody, NotNull, CustomizeValidator(RuleSet = "PreValidationEmployeeUpdate")]UpdateEmployeeCommand model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var result = await _mediator.Send(model);

            return result.IsFailure ? (IActionResult)BadRequest(result.Error) : Ok(result.Value); //FP is here
        }

        [HttpPost("employee/delete/{id}")]
        [Authorize(Roles = "Phonebook_Edit")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(Employee), Description = "Success")]
        [SwaggerResponse(HttpStatusCode.NotFound, typeof(void), Description = "Employee not found")]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(void), Description = "Invalid data")]
        public async Task<IActionResult> DeleteEmployeeByIdAsync(int id)
        {
            if (id <= 0)
            {
                _logger.LogError($"Incorrect value for the employee's Id was set. '{id}' <= 0.");
                return BadRequest();
            }
            var employees = await _mediator.Send(new DeleteEmployeeByIdCommand(id));

            return employees == true ? (IActionResult)Ok() : BadRequest();
        }

        [HttpPost("employee/import")]
        [Authorize(Roles = "Phonebook_Edit")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(Employee), Description = "Success")]
        [SwaggerResponse(HttpStatusCode.NotFound, typeof(void), Description = "Employee not found")]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(void), Description = "Invalid data")]
        public async Task<IActionResult> ImportEmployeeByFileAsync()
        {
            var request = new ImportEmployeeByFileCommand { 
                EmployeeDelete = Request.Form.Files["DeleteUpload"],
                EmployeeNew = Request.Form.Files["NewUpload"]
                };

            var employees = await _mediator.Send(request);

            return employees.HasValue ? (IActionResult)Ok(employees.Value) : NotFound();
        }
    }
}