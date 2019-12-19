using CSharpFunctionalExtensions;
using MediatR;
using PhoneBook.Logic.Models;

namespace PhoneBook.Logic.Queries
{
    public class GetUserByLogin : IRequest<Maybe<User>>
    {
        public string Login { get; set; }

        public GetUserByLogin(string login)
        {
            Login = login;
        }
    }
}
