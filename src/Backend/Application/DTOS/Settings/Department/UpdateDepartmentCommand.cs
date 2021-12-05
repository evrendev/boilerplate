using System;
using EvrenDev.Application.Interfaces.Result;
using MediatR;

namespace EvrenDev.Application.DTOS.Settings.Department
{
    public partial class UpdateDepartmentCommand : CreateDepartmentCommand, IRequest<Result<Guid>>
    {
        public Guid Id { get; set; }
    }   
}