using AutoMapper;
using MediatR;
using System.Collections.Generic;
using EvrenDev.Application.DTOS.User;
using Microsoft.AspNetCore.Identity;
using EvrenDev.Domain.Entities;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EvrenDev.Application.Interfaces.Result;

namespace EvrenDev.Application.Features.Settings.User.Queries.GetPaged
{
    public class ApplicationUsersPagedQueryHandler : IRequestHandler<ApplicationUsersPagedQuery, PaginatedResult<BasicApplicationUserDto>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public ApplicationUsersPagedQueryHandler(UserManager<ApplicationUser> userManager, 
            IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<PaginatedResult<BasicApplicationUserDto>> Handle(ApplicationUsersPagedQuery request, CancellationToken cancellationToken)
        {
            var allUsersExceptCurrentUser = await _userManager.Users
                .Include(u => u.Department)
                .Include(u => u.UserRoles)
                    .ThenInclude(r => r.Role)
                .Where(au => au.Id != request.CurrentUserId && au.Deleted == request.Deleted)
                .ToListAsync();

            var totalUsersCount = allUsersExceptCurrentUser.Count();

            var filteredUser = allUsersExceptCurrentUser
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToList();

            var users = _mapper.Map<IEnumerable<BasicApplicationUserDto>>(filteredUser).ToList();

            var response = PaginatedResult<BasicApplicationUserDto>.Success(users, totalUsersCount, request.PageNumber, request.PageSize);
            
            return response;
        }
    }
}