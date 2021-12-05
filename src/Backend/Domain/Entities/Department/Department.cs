using EvrenDev.Domain.Interfaces;
using EvrenDev.Domain.Shared;
using System.Collections.Generic;

namespace EvrenDev.Domain.Entities
{
    public class Department : BaseEntity, IAggregateRoot
    {
        public Department()
        {

        }

        public Department(string title)
        {
            Title = title;
        }

        public string Title { get; set; }

        public virtual ICollection<ApplicationUser> Users { get; set; }
    }
}
