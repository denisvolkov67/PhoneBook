﻿using CSharpFunctionalExtensions;
using MediatR;
using PhoneBook.Logic.Models;
using System.Collections.Generic;

namespace PhoneBook.Logic.Queries
{
    public class GetUsersFromAdministrativeStaff : IRequest<Maybe<IEnumerable<User>>>
    {

    }
}
