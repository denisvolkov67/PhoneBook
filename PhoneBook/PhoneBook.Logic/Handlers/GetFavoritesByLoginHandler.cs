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

    public class GetFavoritesByLoginHandler : IRequestHandler<GetFavoritesByLogin, Maybe<IEnumerable<Favorites>>>
    {
        private readonly PhoneBookDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<GetFavoritesByLoginHandler> _logger;

        public GetFavoritesByLoginHandler(PhoneBookDbContext context, IMapper mapper, ILogger<GetFavoritesByLoginHandler> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Maybe<IEnumerable<Favorites>>> Handle(GetFavoritesByLogin request, CancellationToken cancellationToken)
        {
            var result = await _context.Favorites
                .Include(e => e.WorkerDb)
                .Where(x => x.Login.Equals(request.Login))
                .OrderBy(o => o.WorkerDb.Name)
                .Select(b => _mapper.Map<Favorites>(b))
                .ToListAsync()
                .ConfigureAwait(false);

            if (result == null)
            {
                _logger.LogError($"There are not favorites with the Login '{request.Login}'...");
            }

            return result != null ?
            Maybe<IEnumerable<Favorites>>.From(result) :
            Maybe<IEnumerable<Favorites>>.None;
        }
    }
}
