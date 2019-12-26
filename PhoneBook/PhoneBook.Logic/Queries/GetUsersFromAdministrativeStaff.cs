using CSharpFunctionalExtensions;
using MediatR;
using PhoneBook.Logic.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhoneBook.Logic.Queries
{
    public class GetUsersFromAdministrativeStaff : IRequest<Maybe<IEnumerable<User>>>
    {

    }
}
