using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace EvrenDev.Domain.Entities
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public virtual ICollection<ApplicationUserRole> UserRoles { get; set; }

        public Guid DepartmentId { get; set; }

        public virtual Department Department { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string JobDescription { get; set; }

        public bool Deleted { get; set; }
        
    }
}
