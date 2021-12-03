using AutoMapper;
using MediatR;
using EvrenDev.Application.DTOS.Content;
using EvrenDev.Application.Interfaces.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace EvrenDev.Application.Features.Settings.Content.Queries.GetById
{
    public class GetContentByIdQueryHandler : IRequestHandler<GetContentByIdQuery, ContentDto>
    {
        private readonly IContentRepository _repository;
        private readonly IMapper _mapper;

        public GetContentByIdQueryHandler(IContentRepository repository, 
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ContentDto> Handle(GetContentByIdQuery query, 
            CancellationToken cancellationToken)
        {
            var item = await _repository.GetByIdAsync(query.Id, cancellationToken);
            var mappedProduct = _mapper.Map<ContentDto>(item);

            return mappedProduct;
        }
    }
}