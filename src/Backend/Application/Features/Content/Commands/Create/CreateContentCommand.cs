using EvrenDev.Application.Interfaces.Result;
using AutoMapper;
using MediatR;
using EvrenDev.Application.DTOS.Content;
using EvrenDev.Application.Interfaces.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EvrenDev.Application.Features.Content.Commands.Create
{ 
    public class CreateContentCommandHandler : IRequestHandler<CreateContentCommand, Result<Guid>>
    {
        private readonly IContentRepository _repository;

        private readonly IMapper _mapper;

        private IUnitOfWork _unitOfWork { get; set; }

        public CreateContentCommandHandler(IContentRepository repository, 
            IUnitOfWork unitOfWork, 
            IMapper mapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<Guid>> Handle(CreateContentCommand request, 
            CancellationToken cancellationToken)
        {
            var item = _mapper.Map<EvrenDev.Domain.Entities.Content>(request);
            await _repository.AddAsync(item);
            await _unitOfWork.Commit(cancellationToken);
            
            return Result<Guid>.Success(item.Id, $"{item.Title} içeriği başarılı bir şekilde eklendi.");
        }
    }
}