using AutoMapper;
using PhoneBook.Data.Models;
using PhoneBook.Logic.Command;
using PhoneBook.Logic.Models;

namespace PhoneBook.Logic.Profiles
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Employee, EmployeeDb>()
                .ForMember(dest => dest.DepartmentDbId, e => e.MapFrom(src => src.DepartmentId))
                .ReverseMap()
                .ForMember(dest => dest.DepartmentId, e => e.MapFrom(src => src.DepartmentDbId));

            CreateMap<CreateEmployeeCommand, EmployeeDb>()
                .ForMember(dest => dest.DepartmentDbId, e => e.MapFrom(src => src.DepartmentId))
                .ReverseMap()
                .ForMember(dest => dest.DepartmentId, e => e.MapFrom(src => src.DepartmentDbId));

            CreateMap<UpdateEmployeeCommand, Employee>();

            CreateMap<UpdateEmployeeCommand, EmployeeDb>()
                .ForMember(dest => dest.DepartmentDbId, e => e.MapFrom(src => src.DepartmentId))
                .ReverseMap()
                .ForMember(dest => dest.DepartmentId, e => e.MapFrom(src => src.DepartmentDbId));

            CreateMap<Department, DepartmentDb>()
                .ReverseMap();
        }

    }
}
