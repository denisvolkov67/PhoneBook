using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSwag.Annotations;
using PhoneBook.Logic.Models;
using PhoneBook.Logic.Queries;
using System;
using System.Net;
using System.Threading.Tasks;

namespace PhoneBook.Web.Controllers
{
    [ApiController]
    public class UsersController : ControllerBase
    {

        private readonly IMediator _mediator;
        private readonly ILogger<UsersController> _logger;

        public UsersController(IMediator mediator, ILogger<UsersController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        /// <summary>
        /// Fetches a user from the AD by the unique Login.
        /// </summary>        
        [HttpGet("api/user/{login}")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(User), Description = "Success")]
        [SwaggerResponse(HttpStatusCode.NotFound, typeof(void), Description = "User not found")]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(void), Description = "Invalid data")]
        public async Task<IActionResult> GetUserByLoginAsync(string login)
        {
            if (String.IsNullOrEmpty(login))
            {
                _logger.LogError($"Incorrect value for the user's Login was set. '{login}' - is null...");
                return BadRequest();
            }
            var result = await _mediator.Send(new GetUserByLogin(login));

            return result != null ? (IActionResult)Ok(result.Value) : NotFound();
        }
    }
}