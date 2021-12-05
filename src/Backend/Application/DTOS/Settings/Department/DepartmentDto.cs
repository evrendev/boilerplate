using System.Collections.Generic;
using EvrenDev.Application.DTOS.Shared;
using EvrenDev.Application.DTOS.User;

namespace EvrenDev.Application.DTOS.Settings.Department
{
    public class DepartmentDto : BaseEntityDto
    {
        public string Title { get; set; }

        public int UsersCount { get; set; }

        public List<ApplicationUserDto> Users { get; set; }
    }
}
