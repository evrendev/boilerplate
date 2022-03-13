using System;
using System.Collections.Generic;

namespace EvrenDev.Infrastructure.DTOS.User
{
    public class BasicApplicationUserDto
    {
        public Guid Id { get; set; }
        
        public List<string> Roles { get; set; }

        public string Email { get; set; }

        public string FullName { get; set; }
    }
}
