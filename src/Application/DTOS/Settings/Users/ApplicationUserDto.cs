using System;
using System.Collections.Generic;
using EvrenDev.Application.DTOS.Settings.Department;
using EvrenDev.Domain.Entities;

namespace EvrenDev.Application.DTOS.User
{
    public class ApplicationUserDto
    {
        public Guid Id { get; set; }

        public BasicDepartmentDto Department { get; set; }
        
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
