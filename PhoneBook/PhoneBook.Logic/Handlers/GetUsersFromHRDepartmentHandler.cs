using AutoMapper;
using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.Extensions.Logging;
using PhoneBook.Logic.Models;
using PhoneBook.Logic.Queries;
using PhoneBook.Logic.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;

namespace PhoneBook.Logic.Handlers
{
    public class GetUsersFromHRDepartmentHandler : IRequestHandler<GetUsersFromHRDepartment, Maybe<IEnumerable<User>>>
    {
        private readonly IMapper _mapper;
        private readonly IMemoryCache _memoryCache;
        private readonly ILogger<GetUsersFromHRDepartmentHandler> _logger;

        public GetUsersFromHRDepartmentHandler(IMapper mapper, IMemoryCache memoryCache, ILogger<GetUsersFromHRDepartmentHandler> logger)
        {
            _mapper = mapper;
            _memoryCache = memoryCache;
            _logger = logger;
        }

        public async Task<Maybe<IEnumerable<User>>> Handle(GetUsersFromHRDepartment request, CancellationToken cancellationToken)
        {
            if (!_memoryCache.TryGetValue("HRDepartment", out List<User> users)) 
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
                    _memoryCache.Set("HRDepartment", users,
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
            logins.Add("kuzmickaya");
            logins.Add("shlykevich");
            logins.Add("shlv");
            logins.Add("zmp");
            logins.Add("kostukova");
            logins.Add("groi");
            logins.Add("kef");

            return logins;
        }
    }
}
