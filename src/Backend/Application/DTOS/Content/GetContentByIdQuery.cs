using System;
using MediatR;

namespace EvrenDev.Application.DTOS.Content
{
    public class GetContentByIdQuery : IRequest<ContentDto>
    {
        public Guid Id { get; set; }
    }
}