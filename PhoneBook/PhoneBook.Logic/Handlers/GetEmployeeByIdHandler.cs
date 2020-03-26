using AutoMapper;
using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PhoneBook.Data.Context;
using PhoneBook.Logic.Models;
using PhoneBook.Logic.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PhoneBook.Logic.Handlers
{
    public class GetEmployeeByIdHandler : IRequestHandler<GetEmployeeById, Maybe<Employee>>
    {
        private readonly PhoneBookDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<GetEmployeeByIdHandler> _logger;

        public GetEmployeeByIdHandler(PhoneBookDbContext context, IMapper mapper, ILogger<GetEmployeeByIdHandler> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Maybe<Employee>> Handle(GetEmployeeById request, CancellationToken cancellationToken)
        {
            var choosenEmployee = await _context.Employees
                .Where(x => x.Id == request.Id)
                .Select(b => _mapper.Map<Employee>(b))
                .FirstOrDefaultAsync();

            if (choosenEmployee == null)
            {
                _logger.LogError($"There are not employee with the Id '{request.Id}'...");
            }

            return choosenEmployee != null ?
            Maybe<Employee>.From(choosenEmployee) :
            Maybe<Employee>.None;
        }
    }
}
