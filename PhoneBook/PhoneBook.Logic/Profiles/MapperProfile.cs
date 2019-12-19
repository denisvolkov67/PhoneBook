using AutoMapper;
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
        }

    }
}
