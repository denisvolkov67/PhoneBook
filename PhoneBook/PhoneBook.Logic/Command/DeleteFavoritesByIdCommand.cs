using MediatR;

namespace PhoneBook.Logic.Command
{
    public class DeleteFavoritesByIdCommand : IRequest<bool>
    {
        public long Id { get; set; }

        public DeleteFavoritesByIdCommand(long id)
        {
            Id = id;
        }
    }
}
