using System;
using EvrenDev.Application.Interfaces.Result;
using MediatR;

namespace EvrenDev.Application.DTOS.Settings.Department
{
    public partial class CreateDepartmentCommand : IRequest<Result<Guid>>
    {   
        public string Title { get; set; }
    }   
}