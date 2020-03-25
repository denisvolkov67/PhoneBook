using CSharpFunctionalExtensions;
using MediatR;
using PhoneBook.Logic.Models;


namespace PhoneBook.Logic.Queries
{
    public class GetDepartmentById : IRequest<Maybe<Department>>
    {
        public string Id { get; set; }

        public GetDepartmentById(string id)
        {
            Id = id;
        }
    }
}
