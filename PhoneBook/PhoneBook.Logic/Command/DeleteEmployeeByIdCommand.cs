using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhoneBook.Logic.Command
{
    public class DeleteEmployeeByIdCommand : IRequest<bool>
    {
        public long Id { get; set; }

        public DeleteEmployeeByIdCommand(long id)
        {
            Id = id;
        }
    }
}
