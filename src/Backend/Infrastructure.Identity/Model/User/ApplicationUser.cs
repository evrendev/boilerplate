using EvrenDev.Application.DTOS.Auth;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace EvrenDev.Infrastructure.Identity.Model
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public virtual ICollection<ApplicationUserRole> UserRoles { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string JobDescription { get; set; }

        public bool Deleted { get; set; }
        
        public List<RefreshToken> RefreshTokens { get; set; }

        public bool OwnsToken(string token)
        {
            return this.RefreshTokens?.Find(x => x.Token == token) != null;
        }
        
    }
}
