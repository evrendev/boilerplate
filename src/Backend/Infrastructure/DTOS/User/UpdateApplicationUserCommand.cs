using System;
using System.Collections.Generic;
using EvrenDev.Application.Interfaces.Result;
using MediatR;

namespace EvrenDev.Infrastructure.DTOS.User
{
    public class UpdateApplicationUserCommand : IRequest<Result<Guid>>
    {
        public Guid Id { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string JobDescription { get; set; }

        public List<string> Roles { get; set; }
    }
}
