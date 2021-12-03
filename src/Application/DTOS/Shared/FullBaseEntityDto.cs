using System;

namespace EvrenDev.Application.DTOS.Shared
{
    public class FullBaseEntityDto : BaseEntityDto
    {
        public string MetaTitle { get; set; }
        public string MetaDescription { get; set; }
        public string[] MetaKeywords { get; set; }
    }
}