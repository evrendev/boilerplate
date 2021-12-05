using System;
using EvrenDev.Domain.Interfaces;
using EvrenDev.Domain.Shared;

namespace EvrenDev.Domain.Entities
{
    public class Content : FullBaseEntity, IAggregateRoot
    {
        public Content()
        {

        }

        public Content(int languageId,
            string title,
            string overview,
            string body,
            string image,
            int[] menuPosition,
            DateTime publishDate)
        {
            LanguageId = languageId;
            Title = title;
            Overview = overview;
            Body = body;
            Image = image;
            MenuPosition = menuPosition;
            PublishDate = publishDate;
        }
        
        public int LanguageId { get; set;}

        public string Overview { get; set;}
        
        public string Title { get; set;}

        public string Body { get; set;}
        
        public string Image { get; set;}

        public int[] MenuPosition { get; set; }

        public DateTime PublishDate { get; set;}
    }
}
