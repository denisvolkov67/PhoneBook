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
                .ForMember(dest => dest.Name_Upper, e => e.MapFrom(src => src.Name.ToUpper()))
                .ForMember(dest => dest.Position_Upper, e => e.MapFrom(src => src.Position.ToUpper()))
                .ReverseMap()
                .ForMember(dest => dest.DepartmentId, e => e.MapFrom(src => src.DepartmentDbId));

            CreateMap<CreateEmployeeCommand, EmployeeDb>()
                .ForMember(dest => dest.DepartmentDbId, e => e.MapFrom(src => src.DepartmentId))
                .ForMember(dest => dest.Name_Upper, e => e.MapFrom(src => src.Name.ToUpper()))
                .ForMember(dest => dest.Position_Upper, e => e.MapFrom(src => src.Position.ToUpper()))
                .ReverseMap()
                .ForMember(dest => dest.DepartmentId, e => e.MapFrom(src => src.DepartmentDbId));

            CreateMap<UpdateEmployeeCommand, Employee>(); 

            CreateMap<UpdateEmployeeCommand, EmployeeDb>()
                .ForMember(dest => dest.DepartmentDbId, e => e.MapFrom(src => src.DepartmentId))
                .ForMember(dest => dest.Name_Upper, e => e.MapFrom(src => src.Name.ToUpper()))
                .ForMember(dest => dest.Position_Upper, e => e.MapFrom(src => src.Position.ToUpper()))
                .ReverseMap()
                .ForMember(dest => dest.DepartmentId, e => e.MapFrom(src => src.DepartmentDbId));

            CreateMap<Department, DepartmentDb>()
                .ReverseMap();

            CreateMap<Favorites, FavoritesDb>()
                .ForMember(dest => dest.WorkerDb, e => e.MapFrom(src => src.Worker))
                .ReverseMap()
                .ForMember(dest => dest.Worker, e => e.MapFrom(src => src.WorkerDb));

            CreateMap<CreateFavoritesCommand, FavoritesDb>();
        }

    }
}
