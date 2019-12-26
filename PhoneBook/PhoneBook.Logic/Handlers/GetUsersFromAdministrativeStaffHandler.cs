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
    public class GetUsersFromAdministrativeStaffHandler : IRequestHandler<GetUsersFromAdministrativeStaff, Maybe<IEnumerable<User>>>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<GetUsersFromAdministrativeStaffHandler> _logger;

        public GetUsersFromAdministrativeStaffHandler(IMapper mapper, ILogger<GetUsersFromAdministrativeStaffHandler> logger)
        {
            _mapper = mapper;
            _logger = logger;
        }


        public async Task<Maybe<IEnumerable<User>>> Handle(GetUsersFromAdministrativeStaff request, CancellationToken cancellationToken)
        {
            List<string> logins = new List<string>();
            logins.Add("eismonti");
            logins.Add("sev");
            logins.Add("gusachenko");
            logins.Add("elan");

            List<User> users = new List<User>();
            User user;
            ActiveDirectoryService adService = new ActiveDirectoryService();

            foreach (string login in logins)
            {
                user = _mapper.Map<User>(adService.GetUser(login));
                user.Office = adService.getOffice(login);
                user.MobileNumber = adService.getMobileNumber(login);
                users.Add(user);
            }


            return users != null ?
                Maybe<IEnumerable<User>>.From(users) :
                Maybe<IEnumerable<User>>.None;
        }
    }
}
