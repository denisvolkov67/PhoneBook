using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.AspNetCore.Http;
using PhoneBook.Logic.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhoneBook.Logic.Command
{
    public class ImportEmployeeByFileCommand : IRequest<Maybe<IEnumerable<Employee>>>
    {
        public IFormFile EmployeeDelete { get; set; }

        public IFormFile EmployeeNew{ get; set; }
    }
}
