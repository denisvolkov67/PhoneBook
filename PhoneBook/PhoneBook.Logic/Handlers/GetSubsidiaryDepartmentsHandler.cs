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
    public class GetSubsidiaryDepartmentsHandler : IRequestHandler<GetSubsidiaryDepartments, Maybe<IEnumerable<Department>>>
    {
        private readonly PhoneBookDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<GetSubsidiaryDepartmentsHandler> _logger;

        public GetSubsidiaryDepartmentsHandler(PhoneBookDbContext context, IMapper mapper, ILogger<GetSubsidiaryDepartmentsHandler> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Maybe<IEnumerable<Department>>> Handle(GetSubsidiaryDepartments request, CancellationToken cancellationToken)
        {
            var result = await _context.Departments
                .Where(x => EF.Functions.Like(x.Id.ToLower(), $"{request.Id}%") 
                    && x.Id.Length ==  (request.Id.Length + 2)
                    && !x.Id.Equals(request.Id))
                .OrderBy(o => o.Id)
                .Select(b => _mapper.Map<Department>(b))
                .ToListAsync()
                .ConfigureAwait(false);

            if (result == null)
            {
                _logger.LogError($"There are not subsidiary departments with the id '{request.Id}'...");
            }

            return result != null ?
            Maybe<IEnumerable<Department>>.From(result) :
            Maybe<IEnumerable<Department>>.None;
        }
    }
}
