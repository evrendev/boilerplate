using System;
using EvrenDev.Application.Interfaces.Result;
using MediatR;

namespace EvrenDev.Application.DTOS.Content
{
    public partial class UpdateContentCommand : CreateContentCommand, IRequest<Result<Guid>>
    {
        public Guid Id { get; set; }
    }   
}