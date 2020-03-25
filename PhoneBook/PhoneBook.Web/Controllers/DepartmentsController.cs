using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSwag.Annotations;
using PhoneBook.Logic.Models;
using PhoneBook.Logic.Queries;

namespace PhoneBook.Web.Controllers
{
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<DepartmentsController> _logger;

        public DepartmentsController(IMediator mediator, ILogger<DepartmentsController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet("department/{id}")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(Department), Description = "Success")]
        [SwaggerResponse(HttpStatusCode.NotFound, typeof(void), Description = "Department not found")]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(void), Description = "Invalid data")]
        public async Task<IActionResult> GetDepartmentByIdAsync(string id)
        {
            if (String.IsNullOrEmpty(id))
            {
                _logger.LogError($"Incorrect value for the sessions's Id was set.");
                return BadRequest();
            }
            var result = await _mediator.Send(new GetDepartmentById(id));

            return result != null ? (IActionResult)Ok(result.Value) : NotFound();
        }

        [HttpGet("departments/getSubsidiary/{id}")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(IEnumerable<Department>), Description = "Success")]
        [SwaggerResponse(HttpStatusCode.NotFound, typeof(void), Description = "Departments not found")]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(void), Description = "Invalid data")]
        public async Task<IActionResult> GetSubsidiaryDepartmentsAsync(string id)
        {
            var departments = await _mediator.Send(new GetSubsidiaryDepartments(id));

            return departments.HasValue ? (IActionResult)Ok(departments.Value) : NotFound();
        }

        [HttpGet("departments/getTopLevel")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(IEnumerable<Department>), Description = "Success")]
        [SwaggerResponse(HttpStatusCode.NotFound, typeof(void), Description = "Departments not found")]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(void), Description = "Invalid data")]
        public async Task<IActionResult> GetTopLevelAsync()
        {
            var departments = await _mediator.Send(new GetDepartmentsTopLevel());

            return departments.HasValue ? (IActionResult)Ok(departments.Value) : NotFound();
        }
    }
}