using CSharpFunctionalExtensions;
using MediatR;
using PhoneBook.Logic.Models;
using System.Collections.Generic;

namespace PhoneBook.Logic.Queries
{
    public class GetFavoritesByLogin : IRequest<Maybe<IEnumerable<Favorites>>>
    {
        public string Login { get; set; }

        public GetFavoritesByLogin(string login)
        {
            Login = login;
        }
    }
}
