using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace EvrenDev.Domain.Entities
{
    public class ApplicationRole : IdentityRole<Guid>
    {
        public virtual ICollection<ApplicationUserRole> UserRoles { get; set; }
    }
}
