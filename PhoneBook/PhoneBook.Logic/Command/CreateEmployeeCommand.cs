using CSharpFunctionalExtensions;
using MediatR;
using PhoneBook.Logic.Models;

namespace PhoneBook.Logic.Command
{
    public class CreateEmployeeCommand : IRequest<Result<Employee>>
    {
        public string Name { get; set; }

        public string Position { get; set; }

        public string Telephone { get; set; }

        public string Mobile { get; set; }

        public string Office { get; set; }

        public string Email { get; set; }

        public string DepartmentId { get; set; }
    }
}
