using System;
using System.Collections.Generic;
using MediatR;

namespace EvrenDev.Application.DTOS.Content
{
    public class ContentsQuery : IRequest<List<ContentBasicDto>>
    {
        public ContentsQuery()
        {
            
        }

        public ContentsQuery(bool deleted,
            Guid? id,
            int? languageId)
        {
            Id = id;
            Deleted = deleted;
        }

        public Guid? Id { get; set; }

        public bool Deleted { get; set; }

        public int? LanguageId { get; set; }
    }
}