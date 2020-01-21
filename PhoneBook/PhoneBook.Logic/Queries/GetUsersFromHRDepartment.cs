using CSharpFunctionalExtensions;
using MediatR;
using PhoneBook.Logic.Models;
using System.Collections.Generic;
namespace PhoneBook.Logic.Queries
{
    public class GetUsersFromHRDepartment : IRequest<Maybe<IEnumerable<User>>>
    {
    }
}
