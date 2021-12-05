using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EvrenDev.Domain.Shared
{
    public abstract class BaseEntity
    {
        protected BaseEntity()
        {

        }

        protected BaseEntity(Guid id, 
            DateTime creationDateTime,
            string creator,
            DateTime lastModificationTime,
            string lastModifier,
            bool deleted,
            string deleter,
            DateTime deletionTime)
        {
            Id = id;
            CreationDateTime = creationDateTime;
            Creator = creator;
            LastModificationTime = lastModificationTime;
            LastModifier = lastModifier;
            Deleted = deleted;
            Deleter = deleter;
            DeletionTime = deletionTime;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public DateTime CreationDateTime { get; set; }

        public string Creator { get; set; }

        public DateTime? LastModificationTime { get; set; }

        public string LastModifier { get; set; }

        public bool Deleted { get; set; }

        public string Deleter { get; set; }

        public DateTime? DeletionTime { get; set; }
    }
}