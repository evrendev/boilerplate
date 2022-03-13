using EvrenDev.Application.Interfaces.Result;
using MediatR;
using EvrenDev.Infrastructure.DTOS.User;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using EvrenDev.Infrastructure.Model;

namespace EvrenDev.Application.Features.Settings.User.Commands.Update
{
    public class UpdateApplicationUserCommandHandler : IRequestHandler<UpdateApplicationUserCommand, Result<Guid>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IStringLocalizer<UpdateApplicationUserCommand> _loc;

        public UpdateApplicationUserCommandHandler(UserManager<ApplicationUser> userManager,
            IStringLocalizer<UpdateApplicationUserCommand> loc)
        {
            _userManager = userManager;
            _loc = loc;
        }

        public async Task<Result<Guid>> Handle(UpdateApplicationUserCommand command, 
            CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(command.Id.ToString());
            if (user == null)
            {
                var message = string.Format(_loc["user_not_found"], command.Id);

                return Result<Guid>.Fail($"Kullanıcı bulunamadı. Id: {command.Id}");
            }

            user.FirstName = command.FirstName;
            user.LastName = command.LastName;
            user.Email = command.Email;
            user.UserName = command.Email;
            user.PhoneNumber = command.PhoneNumber;
            user.JobDescription = command.JobDescription;

            var roles = await _userManager.GetRolesAsync(user);

            await _userManager.RemoveFromRolesAsync(user, roles);

            var updateResult = await _userManager.UpdateAsync(user);

            if (updateResult.Succeeded)
            {
                foreach(var role in command.Roles) 
                {
                    await _userManager.AddToRoleAsync(user, role);
                }

                var message = string.Format(_loc["update_user_success"], user.Email);

                return Result<Guid>.Success(message);
            }
            else
            {
                var message = string.Format(_loc["update_user_failed"], user.Email);

                return Result<Guid>.Fail(message);
            }
        }
    }
}