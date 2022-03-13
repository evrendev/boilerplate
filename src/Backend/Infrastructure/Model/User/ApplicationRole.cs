using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace EvrenDev.Infrastructure.Model
{
    public class ApplicationRole : IdentityRole<Guid>
    {
        public virtual ICollection<ApplicationUserRole> UserRoles { get; set; }
    }
}
