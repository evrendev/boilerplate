using AutoMapper;
using MediatR;
using EvrenDev.Application.DTOS.Content;
using EvrenDev.Application.Interfaces.Repositories;
using EvrenDev.Application.Specifications;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EvrenDev.Application.Interfaces.Result;

namespace EvrenDev.Application.Features.Settings.Content.Queries.GetAllPaged
{
    public class ContentsPagedQueryHandler : IRequestHandler<ContentsPagedQuery, PaginatedResult<ContentBasicDto>>
    {
        private readonly IContentRepository _repository;
        private readonly IMapper _mapper;

        public ContentsPagedQueryHandler(IContentRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<PaginatedResult<ContentBasicDto>> Handle(ContentsPagedQuery request, 
            CancellationToken cancellationToken)
        {
            var filterSpec = new ContentFilterSpecification(deleted: request.Deleted,
                languageId: request.LanguageId
            );

            int totalCount = await _repository.CountAsync(filterSpec, cancellationToken);

            var pagedSpec = new ContentFilterPaginatedSpecification(
                skip: (request.PageNumber - 1) * request.PageSize,
                take: request.PageSize,
                deleted: request.Deleted,
                languageId: request.LanguageId);

            var items = await _repository.ListAsync(pagedSpec, cancellationToken);
            var data = _mapper.Map<List<ContentBasicDto>>(items);

            var response = PaginatedResult<ContentBasicDto>.Success(data, totalCount, request.PageNumber, request.PageSize);

            return response;
        }
    }
}