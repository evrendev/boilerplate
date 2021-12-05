using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic;
using EvrenDev.Domain.Entities;
using EvrenDev.Application.Interfaces.Repositories;
using System.Threading;
using Ardalis.Specification.EntityFrameworkCore;
using Ardalis.Specification;
using System.Linq;
using System;

namespace EvrenDev.Infrastructure.Repositories
{
    public class DepartmentRepository : EfRepository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            
        }

        public async Task<Department> GetByIdWithDetailsAsync(Guid id, 
            CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<Department>()
                    .Include(d => d.Users)
                    .FirstOrDefaultAsync(ba => ba.Id == id, cancellationToken);
        }

        public async Task<List<Department>> GetItemsAsync(ISpecification<Department> spec,
            CancellationToken cancellationToken)
        {
            var specificationResult = ApplySpecification(spec);
            var items = specificationResult
                    .Include(d => d.Users)
                    .ThenInclude(ba => ba.UserRoles)
                    .ToListAsync(cancellationToken);

            return await items;
        }

        private IQueryable<Department> ApplySpecification(ISpecification<Department> specification)
        {
            return SpecificationEvaluator.Default.GetQuery(_dbContext.Set<Department>().AsQueryable(), specification);
        }
    }
}