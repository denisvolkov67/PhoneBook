using MediatR;

namespace PhoneBook.Logic.Command
{
    public class DeleteFavoritesByLoginIdCommand : IRequest<bool>
    {
        public string Login { get; set; }

        public long EmployeeId { get; set; }
    }
}
