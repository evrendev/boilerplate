using System;
using MediatR;

namespace EvrenDev.Application.DTOS.Settings.Department
{
    public class GetDepartmentByIdQuery : IRequest<DepartmentDto>
    {
        public Guid Id { get; set; }
    }
}