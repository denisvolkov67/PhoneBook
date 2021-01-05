using AutoMapper;
using CSharpFunctionalExtensions;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PhoneBook.Data.Context;
using PhoneBook.Data.Models;
using PhoneBook.Logic.Command;
using PhoneBook.Logic.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PhoneBook.Logic.Handlers
{
    class ImportEmployeeByFileHandler : IRequestHandler<ImportEmployeeByFileCommand, Maybe<IEnumerable<Employee>>>
    {
        private readonly PhoneBookDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<ImportEmployeeByFileHandler> _logger;

        public ImportEmployeeByFileHandler(PhoneBookDbContext context, IMapper mapper, ILogger<ImportEmployeeByFileHandler> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Maybe<IEnumerable<Employee>>> Handle(ImportEmployeeByFileCommand request, CancellationToken cancellationToken)
        {
            List<Employee> list = new List<Employee>();

            if (request.EmployeeDelete != null)
            {
                Department department = new Department();
                List<string> listString = new List<string>();

                using (StreamReader reader = new StreamReader(request.EmployeeDelete.OpenReadStream(), System.Text.Encoding.Default))
                {
                    while (reader.Peek() >= 0)
                    {
                        listString.Add(await reader.ReadLineAsync());
                    }
                }

                foreach (string line in listString)
                {
                    try
                    {
                        department = new Department();
                        string[] mass = line.Split(';');

                        department = await _context.Departments
                                                    .Where(x => x.Name == mass[1])
                                                    .Select(d => _mapper.Map<Department>(d))
                                                    .FirstOrDefaultAsync();

                        if (department != null)
                        {
                            var deletedEmployee = await _context.Employees
                                                        .Where(x => x.Name_Upper.Equals(mass[0].ToUpper()) && x.DepartmentDbId.Equals(department.Id))
                                                        .Select(d => d)
                                                        .FirstOrDefaultAsync();
                            if (deletedEmployee != null)
                            {
                                _context.Remove(deletedEmployee);
                            }
                            if (deletedEmployee == null || department == null)
                            {
                                Employee employee = new Employee();
                                employee.Name = mass[0];
                                employee.Position = mass[1];
                                employee.Telephone = "Сотрудник не найден, удаление не завершено";
                                list.Add(employee);
                                _logger.LogError($"There is not a employee with the Name '{mass[0]}' and Position '{mass[1]}'...");
                            }
                        }


                    }
                    catch (Exception exc)
                    {
                        Employee employee = new Employee();
                        employee.Name = line;
                        employee.Telephone = exc.Message + " удаление не завершено";
                        list.Add(employee);
                    }

                }
                await _context.SaveChangesAsync(cancellationToken);

                //save file

                /*string webRootPath = _hostEnvironment.ContentRootPath;
                var newPath = Path.Combine(webRootPath, folderName);
                if (!Directory.Exists(newPath))
                {
                    Directory.CreateDirectory(newPath);
                }
                fileNameDelete = new String(Path.GetFileNameWithoutExtension(postedDelete.FileName).Take(10).ToArray()).Replace(" ", "-");
                fileNameDelete = fileNameDelete + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(postedDelete.FileName);
                string fullPath = Path.Combine(newPath, fileNameDelete);
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    postedDelete.CopyTo(stream);
                }*/
            }

            if (request.EmployeeNew != null)
            {
                Employee employee = new Employee();
                Department department = new Department();
                List<string> listString = new List<string>();

                using (StreamReader reader = new StreamReader(request.EmployeeNew.OpenReadStream(), System.Text.Encoding.Default))
                {
                    while (reader.Peek() >= 0)
                    {
                        listString.Add(await reader.ReadLineAsync());
                    }
                }
                    
                foreach(string line in listString)
                {
                    try
                    {
                        employee = new Employee();
                        department = new Department();
                        string[] mass = line.Split(';');

                        employee.Name = mass[0];
                        employee.Position = mass[2];

                        var choosenEmployee = await _context.Employees
                                .Where(x => x.Name_Upper.Equals(mass[0].ToUpper()) && x.Position_Upper.Equals(mass[2].ToUpper()))
                                .FirstOrDefaultAsync();

                        if (choosenEmployee != null)
                        {
                            employee.Telephone = "Сотрудник уже существует, импорт не завершен";
                            list.Add(employee);
                        }
                        else
                        {
                            department = await _context.Departments
                                                        .Where(x => x.Name == mass[1])
                                                        .Select(d => _mapper.Map<Department>(d))
                                                        .FirstOrDefaultAsync();

                            if (department != null)
                            {
                                employee.DepartmentId = department.Id;
                                var employeeDb = _mapper.Map<EmployeeDb>(employee);
                                _context.Add(employeeDb);
                            }
                            else
                            {
                                employee.Telephone = "Не найден отдел, импорт не завершен";
                                list.Add(employee);
                            }
                        }                      
                    }
                    catch (Exception exc)
                    {
                        employee.Name = line;
                        employee.Telephone = exc.Message + " импорт не завершен";
                        list.Add(employee);
                    }

                }
                await _context.SaveChangesAsync(cancellationToken);
            }


            return list != null ?
            Maybe<IEnumerable<Employee>>.From(list) :
            Maybe<IEnumerable<Employee>>.None;
        }
    }
}
