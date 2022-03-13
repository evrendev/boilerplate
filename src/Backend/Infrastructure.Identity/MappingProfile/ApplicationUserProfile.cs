using AutoMapper;
using System.Linq;
using EvrenDev.Infrastructure.Identity.Model;
using EvrenDev.Application.DTOS.User;

namespace EvrenDev.Infrastructure.Identity.MappingProfile
{
    public class ApplicationUserProfile : Profile
    {
        public ApplicationUserProfile()
        {
            //User Mapping
            CreateMap<ApplicationUser, BasicApplicationUserDto>()
                .ForMember(user => user.Roles,
                    expression => expression.MapFrom(user => user.UserRoles.Select(r => r.Role.Name)))
                .ForMember(user => user.FullName,
                    expression => expression.MapFrom(user => $"{user.FirstName} {user.LastName}"));

            //User Mapping
            CreateMap<ApplicationUser, ApplicationUserDto>()
                .ForMember(user => user.Roles,
                    expression => expression.MapFrom(user => user.UserRoles.Select(r => r.Role.Name)))
                .ForMember(user => user.Phone,
                    expression => expression.MapFrom(user => $"{user.PhoneNumber:(###) ###-####}"))
                .ForMember(user => user.FullName,
                    expression => expression.MapFrom(user => $"{user.FirstName} {user.LastName}"));

            CreateMap<CreateApplicationUserCommand, ApplicationUser>()
                .ForMember(user => user.UserName, expression => expression.MapFrom(user => user.Email))
                .AfterMap((s, d) => {
                    d.EmailConfirmed = true;
                    d.PhoneNumberConfirmed = true;
                    d.LockoutEnabled = true;
                    d.TwoFactorEnabled = false;

                })
                .ReverseMap();

            CreateMap<UpdateApplicationUserCommand, ApplicationUser>()
                .ForMember(user => user.UserName, expression => expression.MapFrom(user => user.Email))
                .ReverseMap();
        }
    }
}