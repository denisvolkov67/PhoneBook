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
    public class DeleteEmployeeByIdHandler : IRequestHandler<DeleteEmployeeByIdCommand, bool>
    {
        private readonly PhoneBookDbContext _context;
        private readonly ILogger<DeleteEmployeeByIdHandler> _logger;

        public DeleteEmployeeByIdHandler(PhoneBookDbContext context, ILogger<DeleteEmployeeByIdHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<bool> Handle(DeleteEmployeeByIdCommand request, CancellationToken cancellationToken)
        {
            var deletedEmployee = _context.Employees
                    .Where(x => x.Id == request.Id)
                    .FirstOrDefault();

            if (deletedEmployee == null)
            {
                _logger.LogError($"There is not a employee with the Id '{request.Id}'...");
                return await Task.FromResult(false);
            }

            _context.Remove(deletedEmployee);
            await _context.SaveChangesAsync(cancellationToken);

            return await Task.FromResult(true);
        }
    }
}
