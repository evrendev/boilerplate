using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.Specification;
using EvrenDev.Domain.Entities;

namespace EvrenDev.Application.Interfaces.Repositories
{

    public interface IDepartmentRepository : IAsyncRepository<Department>
    {
        Task<List<Department>> GetItemsAsync(ISpecification<Department> spec, 
            CancellationToken cancellationToken = default);

        Task<Department> GetByIdWithDetailsAsync(Guid id, CancellationToken cancellationToken = default);
    }
}