using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PhoneBook.Data.Context;
using PhoneBook.Logic.Command;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook.Logic.Validators
{
    public class EmployeeUpdateValidator : AbstractValidator<UpdateEmployeeCommand>
    {
        public EmployeeUpdateValidator()
        {
            RuleSet("PreValidationEmployeeUpdate", () =>
            {
                RuleFor(x => x.Name)
                    .NotEmpty()
                    .WithMessage("Name must be set up obligatory!")
                    .Length(1, 100)
                    .WithMessage("Name must contain at least one and can't be longer, than 100 symbols!");
                RuleFor(x => x.Position)
                    .NotEmpty()
                    .WithMessage("Position must be set up obligatory!")
                    .Length(1, 100)
                    .WithMessage("Position must contain at least one and can't be longer, than 200 symbols!");
            });
        }
    }
}
