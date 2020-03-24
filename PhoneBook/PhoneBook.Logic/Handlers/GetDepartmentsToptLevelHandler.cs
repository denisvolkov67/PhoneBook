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
    public class GetDepartmentsToptLevelHandler : IRequestHandler<GetDepartmentsTopLevel, Maybe<IEnumerable<Department>>>
    {
        private readonly PhoneBookDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<GetDepartmentsToptLevelHandler> _logger;

        public GetDepartmentsToptLevelHandler(PhoneBookDbContext context, IMapper mapper, ILogger<GetDepartmentsToptLevelHandler> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Maybe<IEnumerable<Department>>> Handle(GetDepartmentsTopLevel request, CancellationToken cancellationToken)
        {
            var result = await _context.Departments
                .Where(x => x.Id.Length == 1)
                .OrderBy(o => o.Id)
                .Select(b => _mapper.Map<Department>(b))
                .ToListAsync()
                .ConfigureAwait(false);

            if (result == null)
            {
                _logger.LogError($"There are not found departments top level...");
            }

            return result != null ?
            Maybe<IEnumerable<Department>>.From(result) :
            Maybe<IEnumerable<Department>>.None;
        }
    }
}
