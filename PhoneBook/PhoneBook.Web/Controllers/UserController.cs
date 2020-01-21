﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSwag.Annotations;
using PhoneBook.Logic.Models;
using PhoneBook.Logic.Queries;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace PhoneBook.Web.Controllers
{
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IMediator _mediator;
        private readonly ILogger<UserController> _logger;

        public UserController(IMediator mediator, ILogger<UserController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        /// <summary>
        /// Fetches a user from the AD by the unique Login.
        /// </summary>        
        [HttpGet("user/{login}")]
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
            var user = await _mediator.Send(new GetUserByLogin(login));

            return user.HasValue ? (IActionResult)Ok(user.Value) : NotFound();
        }

        /// <summary>
        /// Fetches users from the AD and from administrative staff.
        /// </summary>        
        [HttpGet("users/{name}")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(IEnumerable<User>), Description = "Success")]
        [SwaggerResponse(HttpStatusCode.NotFound, typeof(void), Description = "Users not found")]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(void), Description = "Invalid data")]
        public async Task<IActionResult> GetUsersByNameAsync(string name)
        {
            var users = await _mediator.Send(new GetUsersByName(name));

            return users.HasValue ? (IActionResult)Ok(users.Value) : NotFound();
        }

        /// <summary>
        /// Fetches users from the AD and from administrative staff.
        /// </summary>        
        [HttpGet("usersFromAdministrativeStaff/")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(IEnumerable<User>), Description = "Success")]
        [SwaggerResponse(HttpStatusCode.NotFound, typeof(void), Description = "Users not found")]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(void), Description = "Invalid data")]
        public async Task<IActionResult> GetUsersFromAdministrativeStaffAsync()
        {
            var users = await _mediator.Send(new GetUsersFromAdministrativeStaff());

            return users.HasValue  ? (IActionResult)Ok(users.Value) : NotFound();
        }


        /// <summary>
        /// Fetches users from the AD and from HR Department.
        /// </summary>        
        [HttpGet("usersFromHRDepartment/")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(IEnumerable<User>), Description = "Success")]
        [SwaggerResponse(HttpStatusCode.NotFound, typeof(void), Description = "Users not found")]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(void), Description = "Invalid data")]
        public async Task<IActionResult> GetUsersFromHRDepartmentAsync()
        {
            var users = await _mediator.Send(new GetUsersFromHRDepartment());

            return users.HasValue ? (IActionResult)Ok(users.Value) : NotFound();
        }
    }
}