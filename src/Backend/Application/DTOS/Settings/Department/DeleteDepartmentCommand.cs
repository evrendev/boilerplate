using System;
using EvrenDev.Application.Interfaces.Result;
using MediatR;

namespace EvrenDev.Application.DTOS.Settings.Department
{
    public partial class DeleteDepartmentCommand : IRequest<Result<Guid>>
    {
        public Guid Id { get; set; }
    }   
}