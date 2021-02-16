using MediatR;
using Microsoft.Extensions.Logging;
using PhoneBook.Data.Context;
using PhoneBook.Logic.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PhoneBook.Logic.Handlers
{
    public class DeleteFavoritesByIdHandler : IRequestHandler<DeleteFavoritesByIdCommand, bool>
    {
        private readonly PhoneBookDbContext _context;
        private readonly ILogger<DeleteFavoritesByIdHandler> _logger;

        public DeleteFavoritesByIdHandler(PhoneBookDbContext context, ILogger<DeleteFavoritesByIdHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<bool> Handle(DeleteFavoritesByIdCommand request, CancellationToken cancellationToken)
        {
            var deletedFavorites = _context.Favorites
                    .Where(x => x.Id == request.Id)
                    .FirstOrDefault();

            if (deletedFavorites == null)
            {
                _logger.LogError($"There is not a favorites with the Id '{request.Id}'...");
                return await Task.FromResult(false);
            }

            _context.Remove(deletedFavorites);
            await _context.SaveChangesAsync(cancellationToken);

            return await Task.FromResult(true);
        }
    }
}
