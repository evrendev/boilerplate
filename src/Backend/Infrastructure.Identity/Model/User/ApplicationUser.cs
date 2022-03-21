using EvrenDev.Application.DTOS.Auth;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EvrenDev.Infrastructure.Identity.Model
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public virtual ICollection<ApplicationUserRole> UserRoles { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FullName => $"{FirstName} {LastName}";

        public string JobDescription { get; set; }

        public bool Deleted { get; set; }
        
        public virtual List<IdentityRefreshToken> RefreshTokens { get; set; } = new List<IdentityRefreshToken>();

        public bool HasValidRefreshToken(string token)
        {
            return this.RefreshTokens.Any(rt => string.Equals(rt.Token, token) && !rt.IsExpired);
        }

        public IdentityRefreshToken GetRefreshToken(string token)
        {
            return this.RefreshTokens.Single(rt => string.Equals(rt.Token, token) && !rt.IsExpired);
        }
    }
}
