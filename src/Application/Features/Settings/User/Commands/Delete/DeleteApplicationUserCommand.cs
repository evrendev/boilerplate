using EvrenDev.Application.Interfaces.Result;
using MediatR;
using EvrenDev.Application.DTOS.User;
using EvrenDev.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EvrenDev.Application.Features.Settings.User.Commands.Delete
{
    public class DeleteApplicationUserCommandHandler : IRequestHandler<DeleteApplicationUserCommand, Result<Guid>>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public DeleteApplicationUserCommandHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<Result<Guid>> Handle(DeleteApplicationUserCommand command, 
            CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(command.Id.ToString());
            if (user == null)
                return Result<Guid>.Fail($"Kullanıcı bulunamadı. Id: {command.Id}");

            user.LockoutEnabled = !user.Deleted;
            user.LockoutEnd = !user.Deleted 
                ? DateTimeOffset.Now.AddDays(365 * 200) 
                : DateTimeOffset.Now.AddDays(-1);

            user.Deleted = !user.Deleted;

            await _userManager.UpdateAsync(user);

            return Result<Guid>.Success($"{user.FirstName} {user.LastName} kullanıcısı başarılı bir şekilde {(user.Deleted ? "silindi" : "geri yüklendi")}");
        }
    }
}