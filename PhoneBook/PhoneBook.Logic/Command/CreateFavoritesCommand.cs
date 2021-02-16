using CSharpFunctionalExtensions;
using MediatR;
using PhoneBook.Logic.Models;

namespace PhoneBook.Logic.Command
{
    public class CreateFavoritesCommand : IRequest<Result<Favorites>>
    {
        public string Login { get; set; }

        public long EmployeeId { get; set; }
    }
}
