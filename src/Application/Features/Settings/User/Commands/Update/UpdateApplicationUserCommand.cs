using EvrenDev.Application.Interfaces.Result;
using MediatR;
using EvrenDev.Application.DTOS.User;
using EvrenDev.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EvrenDev.Application.Enums.Identity;

namespace EvrenDev.Application.Features.Settings.User.Commands.Update
{
    public class UpdateApplicationUserCommandHandler : IRequestHandler<UpdateApplicationUserCommand, Result<Guid>>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UpdateApplicationUserCommandHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<Result<Guid>> Handle(UpdateApplicationUserCommand command, 
            CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(command.Id.ToString());
            if (user == null)
                return Result<Guid>.Fail($"Kullanıcı bulunamadı. Id: {command.Id}");

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

                return Result<Guid>.Success($"{user.FirstName} {user.LastName} kullanıcısı başarılı bir şekilde güncellendi.");
            }
            else
            {
                return Result<Guid>.Fail($"{command.FirstName} {command.LastName} kullanıcısının bilgileri düzenlenirken bir hata oluştu.");
            }
        }
    }
}