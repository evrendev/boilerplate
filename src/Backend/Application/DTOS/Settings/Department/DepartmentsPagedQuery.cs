using MediatR;
using EvrenDev.Application.Interfaces.Result;

namespace EvrenDev.Application.DTOS.Settings.Department
{
    public class DepartmentsPagedQuery : IRequest<PaginatedResult<BasicDepartmentDto>>
    {
        public DepartmentsPagedQuery()
        {
            
        }

        public DepartmentsPagedQuery(int pageNumber,
            int pageSize,
            bool? deleted)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            Deleted = deleted;
        }

        public int PageNumber { get; set; }
        
        public int PageSize { get; set; }

        public bool? Deleted { get; set; }
    }
}