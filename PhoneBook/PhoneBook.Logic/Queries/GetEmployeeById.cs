
using CSharpFunctionalExtensions;
using MediatR;
using PhoneBook.Logic.Models;

namespace PhoneBook.Logic.Queries
{ 
    public class GetEmployeeById : IRequest<Maybe<Employee>>
    {
        public long Id { get; set; }

        public GetEmployeeById(long id)
        {
            Id = id;
        }
    }
}
