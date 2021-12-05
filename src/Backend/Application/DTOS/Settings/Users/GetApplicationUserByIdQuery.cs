using System;
using MediatR;

namespace EvrenDev.Application.DTOS.User
{
    public class GetApplicationUserByIdQuery : IRequest<ApplicationUserDto>
    {
        public Guid Id { get; set; }
    }
}
