using AutoMapper;
using CSharpFunctionalExtensions;
using FluentValidation;
using MediatR;
using PhoneBook.Data.Context;
using PhoneBook.Data.Models;
using PhoneBook.Logic.Command;
using PhoneBook.Logic.Models;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PhoneBook.Logic.Handlers
{
    public class CreateEmployeeHandler : IRequestHandler<CreateEmployeeCommand, Result<Employee>>
    {
        private readonly PhoneBookDbContext _context;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateEmployeeCommand> _validator;

        public CreateEmployeeHandler(PhoneBookDbContext context, IMapper mapper, IValidator<CreateEmployeeCommand> validator)
        {
            _context = context;
            _mapper = mapper;
            _validator = validator;
        }
        public async Task<Result<Employee>> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var result = _validator.Validate(request, ruleSet: "CheckExistingEmployeeValidation");
            
            if (result.Errors.Count > 0)
            {
                return Result.Failure<Employee>(result.Errors.First().ErrorMessage);
            }

            var employeeDb = _mapper.Map<EmployeeDb>(request);
            employeeDb.Name_Upper = employeeDb.Name.ToUpper();
            employeeDb.Position_Upper = employeeDb.Position.ToUpper();

            _context.Add(employeeDb);
            await _context.SaveChangesAsync(cancellationToken);

            return Result.Ok<Employee>(_mapper.Map<Employee>(employeeDb));
        }
    }
}
