using AutoMapper;
using MediatR;
using EvrenDev.Infrastructure.DTOS.User;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EvrenDev.Infrastructure.Model;

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
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u => u.Id == query.Id);

            var applicationUserDto = _mapper.Map<ApplicationUserDto>(applicationUser);

            return applicationUserDto;
        }
    } 
}