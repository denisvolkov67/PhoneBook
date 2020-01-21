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
        private readonly ILogger<GetUsersFromHRDepartmentHandler> _logger;

        public GetUsersFromHRDepartmentHandler(IMapper mapper, ILogger<GetUsersFromHRDepartmentHandler> logger)
        {
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Maybe<IEnumerable<User>>> Handle(GetUsersFromHRDepartment request, CancellationToken cancellationToken)
        {
            List<string> logins = new List<string>();
            logins.Add("kuzmickaya");
            logins.Add("shlykevich");
            logins.Add("shlv");
            logins.Add("zmp");
            logins.Add("kostukova");
            logins.Add("groi");
            logins.Add("kef");


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
