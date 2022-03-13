using System;
using MediatR;

namespace EvrenDev.Infrastructure.DTOS.User
{
    public class GetApplicationUserByIdQuery : IRequest<ApplicationUserDto>
    {
        public Guid Id { get; set; }
    }
}
