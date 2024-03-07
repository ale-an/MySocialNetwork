using AutoMapper;
using BusinessLayer.Models;
using DataLayer.Entities;

namespace BusinessLayer.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<RegisterViewModel, User>()
            .ForMember(x => x.UserName, opt => opt.MapFrom(c => c.Email));
        CreateMap<LoginViewModel, User>();
        CreateMap<User, ProfileViewModel>()
            .ForMember(x => x.FullName, opt => opt.MapFrom(z => $"{z.FirstName} {z.MiddleName} {z.LastName}"));

        CreateMap<User, UserEditViewModel>();

    }
}