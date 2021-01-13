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
    public class GetPreviousDepartmentHandler : IRequestHandler<GetPreviousDepartment, Maybe<Department>>
    {
        private readonly PhoneBookDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<GetPreviousDepartmentHandler> _logger;

        public GetPreviousDepartmentHandler(PhoneBookDbContext context, IMapper mapper, ILogger<GetPreviousDepartmentHandler> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Maybe<Department>> Handle(GetPreviousDepartment request, CancellationToken cancellationToken)
        {
            if (request.Id.Length < 3)
            {
                request.Id = "000";
            }

            var result = await _context.Departments
                                        .Where(x => x.Id.Equals(request.Id.Substring(0, request.Id.Length - 2))
                                            && request.Id.Length >= 3)
                                        .OrderBy(o => o.Id)
                                        .Select(b => _mapper.Map<Department>(b))
                                        .FirstOrDefaultAsync()
                                        .ConfigureAwait(false);



            if (result == null)
            {
                _logger.LogError($"There are not previous departments with the id '{request.Id}'...");
            }

            return result != null ?
            Maybe<Department>.From(result) :
            Maybe<Department>.None;
        }

    }
}
