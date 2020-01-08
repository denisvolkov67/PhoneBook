using CSharpFunctionalExtensions;
using MediatR;
using PhoneBook.Logic.Models;
using System.Collections.Generic;

namespace PhoneBook.Logic.Queries
{
    public class GetUsersByName : IRequest<Maybe<IEnumerable<User>>>
    {
        public string Name { get; set; }

        public GetUsersByName(string name)
        {
            Name = name;
        }
    }
}
