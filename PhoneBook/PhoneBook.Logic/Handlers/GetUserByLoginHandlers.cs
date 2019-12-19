using AutoMapper;
using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.Extensions.Logging;
using PhoneBook.Logic.Models;
using PhoneBook.Logic.Queries;
using System.Threading;
using System.Threading.Tasks;

namespace PhoneBook.Logic.Handlers
{
    public class GetUserByLoginHandlers : IRequestHandler<GetUserByLogin, Maybe<User>>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<GetUserByLoginHandlers> _logger;

        public async Task<Maybe<User>> Handle(GetUserByLogin request, CancellationToken cancellationToken)
        {
            var chosenUser = new User(); //заглушка

            if (chosenUser == null)
            {
                _logger.LogError($"There is not a user with the Login '{request.Login}'...");
            }

            return chosenUser != null ?
                Maybe<User>.From(chosenUser) :
                Maybe<User>.None;
        }
    }
}
