using System;
using Ardalis.Specification;
using EvrenDev.Domain.Entities;

namespace EvrenDev.Application.Specifications
{
    public class DepartmentFilterSpecification : Specification<Department>
    {
        public DepartmentFilterSpecification(bool? deleted,
            Guid? id)
        {
            Query.Where(
                d => (
                    (
                        !deleted.HasValue
                        ||
                        d.Deleted == deleted
                    )
                    &&
                    (
                        !id.HasValue || d.Id == id.Value
                    )
                )
            );
        }
    }
}
