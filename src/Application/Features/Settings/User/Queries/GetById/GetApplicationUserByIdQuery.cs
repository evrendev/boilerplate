using AutoMapper;
using MediatR;
using EvrenDev.Application.DTOS.User;
using EvrenDev.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EvrenDev.Application.Features.Settings.User.Queries.GetById
{
    public class GetApplicationUserByIdQueryHandler : IRequestHandler<GetApplicationUserByIdQuery, ApplicationUserDto>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public GetApplicationUserByIdQueryHandler(UserManager<ApplicationUser> userManager, 
            IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<ApplicationUserDto> Handle(GetApplicationUserByIdQuery query, 
            CancellationToken cancellationToken)
        {
            var applicationUser = await _userManager.Users
                .Include(u => u.Department)
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u => u.Id == query.Id);

            var applicationUserDto = _mapper.Map<ApplicationUserDto>(applicationUser);

            return applicationUserDto;
        }
    } 
}