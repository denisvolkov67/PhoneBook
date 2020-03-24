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
    public class EmployeeValidator : AbstractValidator<CreateEmployeeCommand>
    {
        private readonly PhoneBookDbContext _context;
        private readonly ILogger<EmployeeValidator> _logger;
        public EmployeeValidator(PhoneBookDbContext context, ILogger<EmployeeValidator> logger)
        {
            _context = context;
            _logger = logger;

            RuleSet("PreValidationEmployee", () =>
            {
                RuleFor(x => x.Name)
                    .NotEmpty()
                    .WithMessage("Name must be set up obligatory!")
                    .Length(1, 100)
                    .WithMessage("Name must contain at least one and can't be longer, than 100 symbols!");
            });

            RuleSet("CheckExistingEmployeeValidation", () =>
            {
                RuleFor(x => x)
                    .MustAsync(
                        (o, s, token) => CheckExistingEmployee(o)
                    ).WithMessage("A employee with the same Name and Position already exists, check the Name!");
            });
        }
        private async Task<bool> CheckExistingEmployee(CreateEmployeeCommand model)
        {
            var result = await _context.Employees.AnyAsync(c => c.Name == model.Name && c.Position == model.Position)
                .ConfigureAwait(false);
            _logger.LogError("There was an attempt to create a employee with the existing name.");

            return !result;
        }
    }
}
