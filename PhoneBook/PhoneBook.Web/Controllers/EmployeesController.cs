using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.Logging;
using NJsonSchema.Annotations;
using NSwag.Annotations;
using PhoneBook.Logic.Command;
using PhoneBook.Logic.Models;
using PhoneBook.Logic.Queries;

namespace PhoneBook.Web.Controllers
{
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<EmployeesController> _logger;

        public EmployeesController(IMediator mediator, ILogger<EmployeesController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet("employees/department/{id}")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(Employee), Description = "Success")]
        [SwaggerResponse(HttpStatusCode.NotFound, typeof(void), Description = "Employee not found")]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(void), Description = "Invalid data")]
        public async Task<IActionResult> GetEmployeesByDepartmentIdAsync(string id)
        {
            if (String.IsNullOrEmpty(id))
            {
                _logger.LogError($"Incorrect value for the employee's DepartmentId was set. '{id}' - is null...");
                return BadRequest();
            }
            var employees = await _mediator.Send(new GetEmployeesByDepartmentId(id));

            return employees.HasValue ? (IActionResult)Ok(employees.Value) : NotFound();
        }
      
        [HttpGet("employees/{name}")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(IEnumerable<Employee>), Description = "Success")]
        [SwaggerResponse(HttpStatusCode.NotFound, typeof(void), Description = "Employees not found")]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(void), Description = "Invalid data")]
        public async Task<IActionResult> GetEmployeesByNameAsync(string name)
        {
            var employees = await _mediator.Send(new GetEmployeesByName(name));

            return employees.HasValue ? (IActionResult)Ok(employees.Value) : NotFound();
        }

        [HttpPost("employees")]
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
    }
}