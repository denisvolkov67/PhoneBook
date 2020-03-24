using AutoMapper;
using PhoneBook.Data.Models;
using PhoneBook.Logic.Command;
using PhoneBook.Logic.Models;
using System.DirectoryServices.AccountManagement;

namespace PhoneBook.Logic.Profiles
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<UserPrincipal, User>()
                .ForMember(dest => dest.Login, e => e.MapFrom(src => src.SamAccountName))
                .ForMember(dest => dest.Telephone, e => e.MapFrom(src => src.VoiceTelephoneNumber))
                .ForMember(dest => dest.Email, e => e.MapFrom(src => src.EmailAddress));

            CreateMap<User, UserPrincipal>()
                .ForMember(dest => dest.SamAccountName, e => e.MapFrom(src => src.Login))
                .ForMember(dest => dest.VoiceTelephoneNumber, e => e.MapFrom(src => src.Telephone))
                .ForMember(dest => dest.EmailAddress, e => e.MapFrom(src => src.Email));

            CreateMap<Employee, EmployeeDb>()
                .ForMember(dest => dest.DepartmentDbId, e => e.MapFrom(src => src.DepartmentId))
                .ReverseMap()
                .ForMember(dest => dest.DepartmentId, e => e.MapFrom(src => src.DepartmentDbId));

            CreateMap<Department, DepartmentDb>()
                .ReverseMap();

            CreateMap<CreateEmployeeCommand, EmployeeDb>();
        }

    }
}
