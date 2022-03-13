using System;
using EvrenDev.Application.Interfaces.Result;
using MediatR;

namespace EvrenDev.Infrastructure.DTOS.User
{
    public class DeleteApplicationUserCommand : IRequest<Result<Guid>>
    {
        public Guid Id { get; set; }
    }
}
