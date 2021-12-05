using System.Collections.Generic;
using MediatR;

namespace EvrenDev.Application.DTOS.User
{
    public class ApplicationUsersQuery : IRequest<List<ApplicationUserDto>>
    {
        public ApplicationUsersQuery()
        {
            
        }

        public ApplicationUsersQuery(bool deleted)
        {
            Deleted = deleted;
        }

        public bool Deleted { get; set; }
    }
}