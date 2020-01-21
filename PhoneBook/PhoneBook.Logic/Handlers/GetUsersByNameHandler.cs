using AutoMapper;
using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.Extensions.Logging;
using PhoneBook.Logic.Models;
using PhoneBook.Logic.Queries;
using PhoneBook.Logic.Services;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PhoneBook.Logic.Handlers
{
    public class GetUsersByNameHandler : IRequestHandler<GetUsersByName, Maybe<IEnumerable<User>>>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<GetUsersByNameHandler> _logger;

        public GetUsersByNameHandler(IMapper mapper, ILogger<GetUsersByNameHandler> logger)
        {
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Maybe<IEnumerable<User>>> Handle(GetUsersByName request, CancellationToken cancellationToken)
        {
            ActiveDirectoryService adService = new ActiveDirectoryService();
            List<User> chosenUsers = _mapper.Map<List<User>>(adService.GetUsersByDisplayName(request.Name));

            chosenUsers.Sort();

            if (chosenUsers == null)
            {
                _logger.LogError($"There are not users with the Name '{request.Name}'...");
            }
            else
            {
                foreach (User user in chosenUsers)
                {
                    user.Office = adService.getOffice(user.Login);
                    user.MobileNumber = adService.getMobileNumber(user.Login);
                }
            }

                return chosenUsers != null ?
                Maybe<IEnumerable<User>>.From(chosenUsers) :
                Maybe<IEnumerable<User>>.None;
        }
    }
}
