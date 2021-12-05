using System;
using EvrenDev.Application.DTOS.Shared;
using EvrenDev.Application.Enums.Language;

namespace EvrenDev.Application.DTOS.Content
{
    public class ContentBasicDto
    {
        public Guid Id { get; set; }
        
        public Languages Language { get; set;}

        public string Title { get; set; }

        public string Overview { get; set;}
        
        public ImageDto Image { get; set;}
    }
}
