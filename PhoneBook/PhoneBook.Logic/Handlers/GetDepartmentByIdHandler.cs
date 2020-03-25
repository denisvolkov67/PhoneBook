using AutoMapper;
using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PhoneBook.Data.Context;
using PhoneBook.Logic.Models;
using PhoneBook.Logic.Queries;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PhoneBook.Logic.Handlers
{
    public class GetDepartmentByIdHandler : IRequestHandler<GetDepartmentById, Maybe<Department>>
    {
        private readonly PhoneBookDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<GetDepartmentByIdHandler> _logger;

        public GetDepartmentByIdHandler(PhoneBookDbContext context, IMapper mapper, ILogger<GetDepartmentByIdHandler> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Maybe<Department>> Handle(GetDepartmentById request, CancellationToken cancellationToken)
        {
            var chosenDepartment= await _context.Departments
                .Where(x => x.Id == request.Id)
                .Select(d => _mapper.Map<Department>(d))
                .FirstOrDefaultAsync();

            if (chosenDepartment == null)
            {
                _logger.LogError($"There is not a department with the Id '{request.Id}'...");
            }

            return chosenDepartment != null ?
                Maybe<Department>.From(chosenDepartment) :
                Maybe<Department>.None;
        }
    }
}
