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
    public class EmployeeIdValidator : AbstractValidator<CreateFavoritesCommand>
    {
        private readonly PhoneBookDbContext _context;
        private readonly ILogger<EmployeeIdValidator> _logger;
        public EmployeeIdValidator(PhoneBookDbContext context, ILogger<EmployeeIdValidator> logger)
        {
            _context = context;
            _logger = logger;

            RuleSet("CheckExistingEmployeeIdValidation", () =>
            {
                RuleFor(x => x)
                    .MustAsync(
                        (o, s, token) => CheckExistingEmployeeId(o)
                    ).WithMessage("A employee with the Id not exists, check the Id!");
            });
        }
        private async Task<bool> CheckExistingEmployeeId(CreateFavoritesCommand model)
        {
            var result = await _context.Employees.AnyAsync(c => c.Id == model.EmployeeId)
                .ConfigureAwait(false);
            _logger.LogError("An attempt was made to create a favorites with a non-existent employee.");

            return result;
        }
    }
}
