using System;
using EvrenDev.Application.Interfaces.Result;
using MediatR;

namespace EvrenDev.Application.DTOS.Content
{
    public partial class CreateContentCommand : IRequest<Result<Guid>>
    {   
        public int LanguageId { get; set;}

        public string Overview { get; set;}
        
        public string Title { get; set;}

        public string Body { get; set;}
        
        public string Image { get; set;}

        public int[] MenuPosition { get; set; }

        public DateTime PublishDate { get; set;}

        public string MetaTitle { get; set; }

        public string MetaDescription { get; set; }
        
        public string[] MetaKeywords { get; set; }
    }   
}