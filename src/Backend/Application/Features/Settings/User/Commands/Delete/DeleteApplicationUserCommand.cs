using EvrenDev.Application.Interfaces.Result;
using MediatR;
using EvrenDev.Application.DTOS.User;
using EvrenDev.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;

namespace EvrenDev.Application.Features.Settings.User.Commands.Delete
{
    public class DeleteApplicationUserCommandHandler : IRequestHandler<DeleteApplicationUserCommand, Result<Guid>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IStringLocalizer<DeleteApplicationUserCommand> _loc;

        public DeleteApplicationUserCommandHandler(UserManager<ApplicationUser> userManager,
            IStringLocalizer<DeleteApplicationUserCommand> loc)
        {
            _userManager = userManager;
            _loc = loc;
        }

        public async Task<Result<Guid>> Handle(DeleteApplicationUserCommand command, 
            CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(command.Id.ToString());
            if (user == null)
            {
                var message = string.Format(_loc["user_not_found"], command.Id);

                return Result<Guid>.Fail($"Kullanıcı bulunamadı. Id: {command.Id}");
            } else {
                user.LockoutEnabled = !user.Deleted;
                user.LockoutEnd = !user.Deleted 
                    ? DateTimeOffset.Now.AddDays(365 * 200) 
                    : DateTimeOffset.Now.AddDays(-1);

                user.Deleted = !user.Deleted;

                await _userManager.UpdateAsync(user);

                var message = string.Format(_loc["delete_user_success"], $"{user.FirstName} {user.LastName}", user.Deleted ? "silindi" : "geri yüklendi");

                return Result<Guid>.Success(message);
            }
        }
    }
}