using EvrenDev.Application.Interfaces.Result;
using AutoMapper;
using MediatR;
using EvrenDev.Application.DTOS.User;
using EvrenDev.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EvrenDev.Application.Features.Settings.User.Commands.Create
{ 
    public class CreateApplicationUserCommandHandler : IRequestHandler<CreateApplicationUserCommand, Result<Guid>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public CreateApplicationUserCommandHandler(UserManager<ApplicationUser> userManager, 
            IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<Result<Guid>> Handle(CreateApplicationUserCommand request, 
            CancellationToken cancellationToken)
        {
            var userIsExist = await _userManager.FindByEmailAsync(request.Email);

            if(userIsExist != null) 
                return Result<Guid>.Fail("Mevcut eposta adresi ile daha önceden kullanıcı oluşturulmuş.");

            var applicationUser = _mapper.Map<ApplicationUser>(request);

            var createUser = await _userManager.CreateAsync(applicationUser, request.Password);

            if (createUser.Succeeded)
            {
                foreach(var role in request.Roles) 
                {
                    await _userManager.AddToRoleAsync(applicationUser, role);
                }

                return Result<Guid>.Success($"{request.FirstName} {request.LastName} kullanıcısı başarılı bir şekilde oluşturuldu.");
            }
            else
            {
                return Result<Guid>.Fail($"{request.FirstName} {request.LastName} kullanıcısı eklenirken bir hata oluştu.");
            }
        }
    }
}