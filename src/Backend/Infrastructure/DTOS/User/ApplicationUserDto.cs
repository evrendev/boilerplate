using System;
using System.Collections.Generic;

namespace EvrenDev.Infrastructure.DTOS.User
{
    public class ApplicationUserDto
    {
        public Guid Id { get; set; }
        
        public List<string> Roles { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string JobDescription { get; set; }

        public bool Deleted { get; set; }

        public string FullName { get; set; }

        public string Phone { get; set; }
    }
}
