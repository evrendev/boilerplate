using System;

namespace EvrenDev.Application.DTOS.Shared
{
    public class BaseEntityDto
    {
        public Guid Id { get; set; }
        
        public bool Deleted { get; set; }
        
        public DateTimeDto CreationDate { get; set; }

        public DateTimeDto ModifiedDate { get; set; }

        public DateTimeDto DeletionDate { get; set; }
    }
}