using AutoMapper;
using MediatR;
using EvrenDev.Application.DTOS.Content;
using EvrenDev.Application.Interfaces.Repositories;
using EvrenDev.Application.Specifications;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EvrenDev.Application.Features.Content.Queries.GetAll
{
    public class ContentsQueryHandler : IRequestHandler<ContentsQuery, List<ContentBasicDto>>
    {
        private readonly IContentRepository _repository;
        private readonly IMapper _mapper;

        public ContentsQueryHandler(IContentRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<ContentBasicDto>> Handle(ContentsQuery request, 
            CancellationToken cancellationToken)
        {
            var filterSpec = new ContentFilterSpecification(deleted: request.Deleted,
                languageId: request.LanguageId
            );

            var items = await _repository.GetItemsAsync(filterSpec, cancellationToken);
            var mappedItems = _mapper.Map<List<ContentBasicDto>>(items);

            return mappedItems;
        }
    }
}