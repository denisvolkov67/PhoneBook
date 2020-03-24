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