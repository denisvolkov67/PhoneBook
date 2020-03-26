using CSharpFunctionalExtensions;
using MediatR;
using PhoneBook.Logic.Models;

namespace PhoneBook.Logic.Command
{
    public class UpdateEmployeeCommand : IRequest<Result<Employee>>
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Position { get; set; }

        public string Telephone { get; set; }

        public string Mobile { get; set; }

        public string Office { get; set; }

        public string Email { get; set; }

        public string DepartmentId { get; set; }

        public UpdateEmployeeCommand() { }

        public UpdateEmployeeCommand(long id, string name, string position, string telephone, string mobile, string office, string email, string departmentId)
        {
            this.Id = id;
            this.Name = name;
            this.Position = position;
            this.Telephone = telephone;
            this.Mobile = mobile;
            this.Office = office;
            this.Email = email;
            this.DepartmentId = departmentId;
        }
    }
}
