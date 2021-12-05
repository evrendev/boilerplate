using System.Collections.Generic;
using EvrenDev.Application.DTOS.Shared;
using EvrenDev.Application.Enums.Language;

namespace EvrenDev.Application.DTOS.Content
{
    public class ContentDto : FullBaseEntityDto
    {
        public Languages Language { get; set;}

        public string Overview { get; set;}
        
        public string Title { get; set;}

        public string Body { get; set;}
        
        public ImageDto Image { get; set;}

        public List<MenuPositions> MenuPositions { get; set; }

        public DateTimeDto PublishDate { get; set; }

        
    }
}
