using System;
using MediatR;
using EvrenDev.Application.Interfaces.Result;

namespace EvrenDev.Infrastructure.DTOS.User
{
    public class ApplicationUsersPagedQuery : IRequest<PaginatedResult<BasicApplicationUserDto>>
    {
        public ApplicationUsersPagedQuery()
        {
            
        }

        public ApplicationUsersPagedQuery(Guid currentUserId, 
            int pageNumber,
            int pageSize,
            bool? deleted)
        {
            CurrentUserId = currentUserId;
            PageNumber = pageNumber;
            PageSize = pageSize;
            Deleted = deleted;
        }

        public Guid CurrentUserId { get; set; }

        public int PageNumber { get; set; }
        
        public int PageSize { get; set; }

        public bool? Deleted { get; set; }
    }
}