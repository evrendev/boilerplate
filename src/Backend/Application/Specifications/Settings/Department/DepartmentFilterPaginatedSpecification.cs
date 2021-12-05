using Ardalis.Specification;
using EvrenDev.Domain.Entities;

namespace EvrenDev.Application.Specifications
{
    public class DepartmentFilterPaginatedSpecification : Specification<Department>
    {
        public DepartmentFilterPaginatedSpecification(bool? deleted,
            int skip = 0, 
            int take = 25)
            : base()
        {                           
            Query
                .Where(d => 
                    (
                        !deleted.HasValue
                        ||
                        d.Deleted == deleted
                    )
                )
                .Skip(skip)
                .Take(take);
        }
    }
}
