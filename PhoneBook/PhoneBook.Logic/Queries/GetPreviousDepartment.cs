using CSharpFunctionalExtensions;
using MediatR;
using PhoneBook.Logic.Models;
using System.Collections.Generic;

namespace PhoneBook.Logic.Queries
{
    public class GetPreviousDepartment : IRequest<Maybe<Department>>
    {
        public string Id { get; set; }

        public GetPreviousDepartment(string id)
        {
            Id = id;
        }
    }
}
