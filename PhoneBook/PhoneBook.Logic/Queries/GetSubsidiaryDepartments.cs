using CSharpFunctionalExtensions;
using MediatR;
using PhoneBook.Logic.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhoneBook.Logic.Queries
{
    public class GetSubsidiaryDepartments : IRequest<Maybe<IEnumerable<Department>>>
    {
        public string Id { get; set; }

        public GetSubsidiaryDepartments(string id)
        {
            Id = id;
        }
    }
}
