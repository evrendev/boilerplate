using System;
using System.Collections.Generic;
using MediatR;

namespace EvrenDev.Application.DTOS.Settings.Department
{
    public class DepartmentsQuery : IRequest<List<DepartmentDto>>
    {
        public DepartmentsQuery()
        {
            
        }

        public DepartmentsQuery(bool deleted,
            Guid? id)
        {
            Id = id;
            Deleted = deleted;
        }

        public Guid? Id { get; set; }

        public bool Deleted { get; set; }
    }
}