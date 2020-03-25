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
using System.Threading;
using System.Threading.Tasks;

namespace PhoneBook.Logic.Handlers
{

    public class GetEmployeesByNameHandler : IRequestHandler<GetEmployeesByName, Maybe<IEnumerable<Employee>>>
    {
        private readonly PhoneBookDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<GetEmployeesByNameHandler> _logger;

        public GetEmployeesByNameHandler(PhoneBookDbContext context, IMapper mapper, ILogger<GetEmployeesByNameHandler> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Maybe<IEnumerable<Employee>>> Handle(GetEmployeesByName request, CancellationToken cancellationToken)
        {
            var result = await _context.Employees
                .Where(x => x.Name_Upper.Contains(request.Name.ToUpper()) || x.Position_Upper.Contains(request.Name.ToUpper()))
                //.Where(x => x.Name.ToUpper().Contains(request.Name.ToUpper()))
                //.Where(x => x.Name.Contains(request.Name, StringComparison.OrdinalIgnoreCase))
                //.Where(x => EF.Functions.Like(x.Name.ToLower(), $"%{request.Name}%"))
                .OrderBy(o => o.Name)
                .Select(b => _mapper.Map<Employee>(b))
                .ToListAsync()
                .ConfigureAwait(false);

            if (result == null)
            {
                _logger.LogError($"There are not employees with the Name '{request.Name}'...");
            }

            return result != null ?
            Maybe<IEnumerable<Employee>>.From(result) :
            Maybe<IEnumerable<Employee>>.None;
        }
    }
}
