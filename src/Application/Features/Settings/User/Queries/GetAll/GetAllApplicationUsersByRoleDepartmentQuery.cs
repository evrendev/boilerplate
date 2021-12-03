﻿using AutoMapper;
using MediatR;
using System.Collections.Generic;
using EvrenDev.Application.DTOS.User;
using Microsoft.AspNetCore.Identity;
using EvrenDev.Domain.Entities;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EvrenDev.Application.Features.Settings.User.Queries.GetAll
{
    public class ApplicationUsersByRoleDepartmentQueryHandler : IRequestHandler<ApplicationUsersByRoleDepartmentQuery, List<ApplicationUserDto>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public ApplicationUsersByRoleDepartmentQueryHandler(UserManager<ApplicationUser> userManager, 
            IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<List<ApplicationUserDto>> Handle(ApplicationUsersByRoleDepartmentQuery request, 
            CancellationToken cancellationToken)
        {
            var allUsers = await _userManager.Users
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .Include(u => u.Department)
                .Where(au => 
                    (
                        au.Deleted == request.Deleted
                    )
                    &&
                    (
                        !request.DepartmentId.HasValue 
                        ||
                        au.DepartmentId == request.DepartmentId
                    )
                )
                .ToListAsync();

            var mappedUsers = _mapper.Map<List<ApplicationUserDto>>(allUsers).ToList();

            return mappedUsers;
        }
    }
}