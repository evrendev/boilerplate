using System.Collections.Generic;
using MediatR;

namespace EvrenDev.Infrastructure.DTOS.User
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