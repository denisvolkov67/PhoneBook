using AutoMapper;
using CSharpFunctionalExtensions;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PhoneBook.Data.Context;
using PhoneBook.Data.Models;
using PhoneBook.Logic.Command;
using PhoneBook.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PhoneBook.Logic.Handlers
{
    public class UpdateEmployeeHandler : IRequestHandler<UpdateEmployeeCommand, Result<Employee>>
    {
        private readonly PhoneBookDbContext _context;
        private readonly IMapper _mapper;
        private readonly IValidator<UpdateEmployeeCommand> _validator;

        public UpdateEmployeeHandler(PhoneBookDbContext context, IMapper mapper, IValidator<UpdateEmployeeCommand> validator)
        {
            _context = context;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<Result<Employee>> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var result = _validator.Validate(request, ruleSet: "PreValidationEmployeeUpdate");

            if (result.Errors.Count > 0)
            {
                return Result.Failure<Employee>(result.Errors.First().ErrorMessage);
            }

            var employeeDb = _mapper.Map<EmployeeDb>(request);
            employeeDb.Name_Upper = employeeDb.Name.ToUpper();
            employeeDb.Position_Upper = employeeDb.Position.ToUpper();

            _context.Entry(employeeDb).State = EntityState.Modified;
            await _context.SaveChangesAsync(cancellationToken);

            return Result.Ok<Employee>(_mapper.Map<Employee>(employeeDb));
        }
    }
}
