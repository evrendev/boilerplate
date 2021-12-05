using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.Specification;
using EvrenDev.Domain.Entities;

namespace EvrenDev.Application.Interfaces.Repositories
{
    public interface IContentRepository : IAsyncRepository<Content>
    {
        Task<List<Content>> GetItemsAsync(ISpecification<Content> spec, 
            CancellationToken cancellationToken = default);
    }
}