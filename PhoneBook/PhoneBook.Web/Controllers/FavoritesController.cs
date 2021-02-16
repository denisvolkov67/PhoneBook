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
    public class FavoritesController : Controller
    {
        private readonly IMediator _mediator;
        private readonly ILogger<FavoritesController> _logger;

        public FavoritesController(IMediator mediator, ILogger<FavoritesController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }
      
        [HttpGet("favorites/{name}")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(IEnumerable<Favorites>), Description = "Success")]
        [SwaggerResponse(HttpStatusCode.NotFound, typeof(void), Description = "Favorites not found")]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(void), Description = "Invalid data")]
        public async Task<IActionResult> GetFavoritesByLoginAsync(string name)
        {
            var favorites = await _mediator.Send(new GetFavoritesByLogin(name));

            return favorites.HasValue ? (IActionResult)Ok(favorites.Value) : NotFound();
        }

        [HttpPost("favorites")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(Favorites), Description = "Success")]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(void), Description = "Invalid data")]
        public async Task<IActionResult> CreateFavorites([FromBody, NotNull] CreateFavoritesCommand model)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid model for create a new favorites was used.");
                return BadRequest();
            }

            var result = await _mediator.Send(model);

            return result.IsFailure ? (IActionResult)BadRequest(result.Error) : Ok(result.Value); //FP is here           
        }

        [HttpPost("favorites/delete/{id}")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(Favorites), Description = "Success")]
        [SwaggerResponse(HttpStatusCode.NotFound, typeof(void), Description = "Favorites not found")]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(void), Description = "Invalid data")]
        public async Task<IActionResult> DeleteFavoritesByIdAsync(int id)
        {
            if (id <= 0)
            {
                _logger.LogError($"Incorrect value for the favorites Id was set. '{id}' <= 0.");
                return BadRequest();
            }
            var favorites = await _mediator.Send(new DeleteFavoritesByIdCommand(id));

            return favorites == true ? (IActionResult)Ok() : BadRequest();
        }

        [HttpPost("favorites/delete")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(Favorites), Description = "Success")]
        [SwaggerResponse(HttpStatusCode.NotFound, typeof(void), Description = "Favorites not found")]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(void), Description = "Invalid data")]
        public async Task<IActionResult> DeleteFavoritesByLoginIdAsync([FromBody, NotNull] DeleteFavoritesByLoginIdCommand model)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid model for delete favorites was used.");
                return BadRequest();
            }

            var favorites = await _mediator.Send(model);

            return favorites == true ? (IActionResult)Ok() : BadRequest();
        }
    }
}