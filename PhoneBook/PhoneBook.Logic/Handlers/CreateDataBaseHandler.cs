using MediatR;
using PhoneBook.Data.Context;
using PhoneBook.Logic.Command;
using System.Threading;
using System.Threading.Tasks;

namespace PhoneBook.Logic.Handlers
{
    public class CreateDataBaseHandler : AsyncRequestHandler<CreateDatabaseCommand>
    {
        private readonly PhoneBookDbContext _context;

        public CreateDataBaseHandler(PhoneBookDbContext context)
        {
            _context = context;
        }

        protected override async Task Handle(CreateDatabaseCommand request, CancellationToken cancellationToken)
        {
            await _context.Database.EnsureCreatedAsync(cancellationToken);
        }
    }
}
