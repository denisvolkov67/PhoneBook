﻿using CSharpFunctionalExtensions;
using MediatR;
using PhoneBook.Logic.Models;
using System.Collections.Generic;

namespace PhoneBook.Logic.Queries
{
    public class GetEmployeesByDepartmentId : IRequest<Maybe<IEnumerable<Employee>>>
    {
        public string DepartmentId { get; set; }

        public string Login { get; set; }

        public GetEmployeesByDepartmentId(string id, string login)
        {
            DepartmentId = id;
            Login = login;
        }
    }
}
