using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic;
using EvrenDev.Domain.Entities;
using EvrenDev.Application.Interfaces.Repositories;
using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using System.Threading;
using System.Linq;

namespace EvrenDev.Infrastructure.Persistence.Repositories
{
    public class ContentRepository : EfRepository<Content>, IContentRepository
    {
        public ContentRepository(ApplicationContext dbContext) : base(dbContext)
        {
            
        }

        public async Task<List<Content>> GetItemsAsync(ISpecification<Content> spec,
            CancellationToken cancellationToken)
        {
            var specificationResult = ApplySpecification(spec);
            var items = specificationResult
                .ToListAsync(cancellationToken);

            return await items;
        }

        private IQueryable<Content> ApplySpecification(ISpecification<Content> specification)
        {
            return SpecificationEvaluator.Default.GetQuery(_dbContext.Set<Content>().AsQueryable(), specification);
        }
    }
}