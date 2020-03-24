using CSharpFunctionalExtensions;
using MediatR;
using PhoneBook.Logic.Models;
using System.Collections.Generic;

namespace PhoneBook.Logic.Queries
{
    public class GetEmployeesByName : IRequest<Maybe<IEnumerable<Employee>>>
    {
        public string Name { get; set; }

        public GetEmployeesByName(string name)
        {
            Name = name;
        }
    }
}
