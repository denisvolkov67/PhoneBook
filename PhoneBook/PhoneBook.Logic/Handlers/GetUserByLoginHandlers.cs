using AutoMapper;
using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.Extensions.Logging;
using PhoneBook.Logic.Models;
using PhoneBook.Logic.Queries;
using PhoneBook.Logic.Services;
using System.Threading;
using System.Threading.Tasks;

namespace PhoneBook.Logic.Handlers
{
    public class GetUserByLoginHandler : IRequestHandler<GetUserByLogin, Maybe<User>>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<GetUserByLoginHandler> _logger;

        public GetUserByLoginHandler(IMapper mapper, ILogger<GetUserByLoginHandler> logger)
        {
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Maybe<User>> Handle(GetUserByLogin request, CancellationToken cancellationToken)
        {
            ActiveDirectoryService adService = new ActiveDirectoryService();
            var chosenUserPrincipal = adService.GetUser(request.Login);
            var chosenUser= _mapper.Map<User>(chosenUserPrincipal);

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
