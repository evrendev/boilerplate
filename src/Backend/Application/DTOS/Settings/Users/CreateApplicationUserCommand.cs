using System;
using System.Collections.Generic;
using EvrenDev.Application.Interfaces.Result;
using MediatR;

namespace EvrenDev.Application.DTOS.User
{
    public class CreateApplicationUserCommand : IRequest<Result<Guid>>
    {
        public Guid DepartmentId { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string JobDescription { get; set; }

        public string Password { get; set; }

        public List<string> Roles { get; set; }
    }
}
