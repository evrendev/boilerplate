using EvrenDev.Application.Interfaces.Result;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using EvrenDev.Infrastructure.Model;
using EvrenDev.Infrastructure.DTOS.User;

namespace EvrenDev.Application.Features.Settings.User.Commands.Create
{ 
    public class CreateApplicationUserCommandHandler : IRequestHandler<CreateApplicationUserCommand, Result<Guid>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<CreateApplicationUserCommand> _loc;

        public CreateApplicationUserCommandHandler(UserManager<ApplicationUser> userManager,
            IStringLocalizer<CreateApplicationUserCommand> loc, 
            IMapper mapper)
        {
            _userManager = userManager;
            _loc = loc;
            _mapper = mapper;
        }

        public async Task<Result<Guid>> Handle(CreateApplicationUserCommand request, 
            CancellationToken cancellationToken)
        {
            var userIsExist = await _userManager.FindByEmailAsync(request.Email);

            if(userIsExist != null) {
                var message = string.Format(_loc["user_is_exist"], request.Email);

                return Result<Guid>.Fail(message);
            }

            var applicationUser = _mapper.Map<ApplicationUser>(request);

            var createUser = await _userManager.CreateAsync(applicationUser, request.Password);

            if (createUser.Succeeded)
            {
                foreach(var role in request.Roles) 
                {
                    await _userManager.AddToRoleAsync(applicationUser, role);
                }

                var message = string.Format(_loc["create_user_success"], request.Email);

                return Result<Guid>.Success(message);
            }
            else
            {
                var message = string.Format(_loc["create_user_failed"], request.Email);
                
                return Result<Guid>.Fail(message);
            }
        }
    }
}