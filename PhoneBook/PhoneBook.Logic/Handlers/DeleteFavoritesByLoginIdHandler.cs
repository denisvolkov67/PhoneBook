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
    public class DeleteFavoritesByLoginIdHandler : IRequestHandler<DeleteFavoritesByLoginIdCommand, bool>
    {
        private readonly PhoneBookDbContext _context;
        private readonly ILogger<DeleteFavoritesByLoginIdHandler> _logger;

        public DeleteFavoritesByLoginIdHandler(PhoneBookDbContext context, ILogger<DeleteFavoritesByLoginIdHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<bool> Handle(DeleteFavoritesByLoginIdCommand request, CancellationToken cancellationToken)
        {
            var deletedFavorites = _context.Favorites
                    .Where(x => x.Login == request.Login && x.WorkerDb.Id == request.EmployeeId)
                    .FirstOrDefault();

            if (deletedFavorites == null)
            {
                _logger.LogError($"There is not a favorites with the Login '{request.Login}' and Id '{request.EmployeeId}'...");
                return await Task.FromResult(false);
            }

            _context.Remove(deletedFavorites);
            await _context.SaveChangesAsync(cancellationToken);

            return await Task.FromResult(true);
        }
    }
}
