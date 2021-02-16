using AutoMapper;
using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PhoneBook.Data.Context;
using PhoneBook.Logic.Models;
using PhoneBook.Logic.Queries;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PhoneBook.Logic.Handlers
{
    public class GetEmployeesByDepartmentIdHandler : IRequestHandler<GetEmployeesByDepartmentId, Maybe<IEnumerable<Employee>>>
    {
        private readonly PhoneBookDbContext _context;
        private readonly IMapper _mapper;

        public GetEmployeesByDepartmentIdHandler(PhoneBookDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Maybe<IEnumerable<Employee>>> Handle(GetEmployeesByDepartmentId request, CancellationToken cancellationToken)
        {
            var favorites = await _context.Favorites
                                        .Include(e => e.WorkerDb)
                                        .Where(x => x.Login.Equals(request.Login))
                                        .OrderBy(o => o.WorkerDb.Name)
                                        .Select(b => _mapper.Map<Favorites>(b))
                                        .ToListAsync()
                                        .ConfigureAwait(false);

            var employees = await _context.Employees
                            .Where(x => x.DepartmentDbId == request.DepartmentId)
                            .Select(b => _mapper.Map<Employee>(b))
                            .ToListAsync()
                            .ConfigureAwait(false);

            if (favorites != null)
            {
                for (int i = 0; i < favorites.Count; i++)
                    for (int j = 0; j < employees.Count; j++)
                        if (favorites[i].Worker.Equals(employees[j]))
                            employees[j].Favorites = true;
            }

            return employees != null ?
                Maybe<IEnumerable<Employee>>.From(employees) :
                Maybe<IEnumerable<Employee>>.None;
        }
    }
}
