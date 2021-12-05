using System;
using System.Collections.Generic;
using MediatR;

namespace EvrenDev.Application.DTOS.User
{
    public class ApplicationUsersByRoleDepartmentQuery : IRequest<List<ApplicationUserDto>>
    {
        public ApplicationUsersByRoleDepartmentQuery()
        {
            
        }

        public ApplicationUsersByRoleDepartmentQuery(bool deleted,
            Guid currentUserId,
            Guid? departmentId,
            int level)
        {
            Deleted = deleted;
            CurrentUserId = currentUserId;
            DepartmentId = departmentId;
            Level = level;
        }

        public Guid CurrentUserId { get; set; }

        public bool Deleted { get; set; }

        public Guid? DepartmentId { get; set; }

        public int Level { get; set; }
    }
}