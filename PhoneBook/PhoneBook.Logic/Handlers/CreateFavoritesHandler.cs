using AutoMapper;
using CSharpFunctionalExtensions;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PhoneBook.Data.Context;
using PhoneBook.Data.Models;
using PhoneBook.Logic.Command;
using PhoneBook.Logic.Models;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PhoneBook.Logic.Handlers
{
    public class CreateFavoritesHandler : IRequestHandler<CreateFavoritesCommand, Result<Favorites>>
    {
        private readonly PhoneBookDbContext _context;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateFavoritesCommand> _validator;

        public CreateFavoritesHandler(PhoneBookDbContext context, IMapper mapper, IValidator<CreateFavoritesCommand> validator)
        {
            _context = context;
            _mapper = mapper;
            _validator = validator;
        }
        public async Task<Result<Favorites>> Handle(CreateFavoritesCommand request, CancellationToken cancellationToken)
        {
            var result = _validator.Validate(request, ruleSet: "CheckExistingEmployeeIdValidation");
            
            if (result.Errors.Count > 0)
            {
                return Result.Failure<Favorites>(result.Errors.First().ErrorMessage);
            }

            var favoriteDb = _mapper.Map<FavoritesDb>(request);
            var employee = await _context.Employees
                            .Where(x => x.Id == request.EmployeeId)
                            .Select(b => b)
                            .FirstOrDefaultAsync();
            favoriteDb.WorkerDb = employee;
            _context.Add(favoriteDb);
            await _context.SaveChangesAsync(cancellationToken);

            return Result.Ok<Favorites>(_mapper.Map<Favorites>(favoriteDb));
        }
    }
}
