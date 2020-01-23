using AutoMapper;
using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using PhoneBook.Logic.Models;
using PhoneBook.Logic.Queries;
using PhoneBook.Logic.Services;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PhoneBook.Logic.Handlers
{
    public class GetUsersFromAdministrativeStaffHandler : IRequestHandler<GetUsersFromAdministrativeStaff, Maybe<IEnumerable<User>>>
    {
        private readonly IMapper _mapper;
        private readonly IMemoryCache _memoryCache;
        private readonly ILogger<GetUsersFromAdministrativeStaffHandler> _logger;

        public GetUsersFromAdministrativeStaffHandler(IMapper mapper, IMemoryCache memoryCache, ILogger<GetUsersFromAdministrativeStaffHandler> logger)
        {
            _mapper = mapper;
            _memoryCache = memoryCache;
            _logger = logger;
        }


        public async Task<Maybe<IEnumerable<User>>> Handle(GetUsersFromAdministrativeStaff request, CancellationToken cancellationToken)
        {
            if (!_memoryCache.TryGetValue("AdministrativeStaff", out List<User> users))
            {
                users = new List<User>();
                List<string> logins = GetLogins();
                User user;
                ActiveDirectoryService adService = new ActiveDirectoryService();

                foreach (string login in logins)
                {
                    user = _mapper.Map<User>(adService.GetUser(login));
                    user.Office = adService.getOffice(login);
                    user.MobileNumber = adService.getMobileNumber(login);
                    users.Add(user);
                }

                if (users != null)
                {
                    _memoryCache.Set("AdministrativeStaff", users,
                        new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromHours(4)));
                }
                else
                {
                    _logger.LogError($"Not found users by HR Department '{request}'...");
                }
            }


            return users != null ?
                Maybe<IEnumerable<User>>.From(users) :
                Maybe<IEnumerable<User>>.None;
        }

        List<string> GetLogins()
        {
            List<string> logins = new List<string>();
            logins.Add("eismonti");
            logins.Add("sev");
            logins.Add("gusachenko");
            logins.Add("elan");
            logins.Add("briv");
            logins.Add("bor");

            return logins;
        }
    }
}
