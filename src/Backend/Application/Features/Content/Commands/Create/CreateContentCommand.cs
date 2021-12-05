using EvrenDev.Application.Interfaces.Result;
using AutoMapper;
using MediatR;
using EvrenDev.Application.DTOS.Content;
using EvrenDev.Application.Interfaces.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;

namespace EvrenDev.Application.Features.Content.Commands.Create
{ 
    public class CreateContentCommandHandler : IRequestHandler<CreateContentCommand, Result<Guid>>
    {
        private readonly IContentRepository _repository;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<CreateContentCommand> _loc;
        private IUnitOfWork _unitOfWork { get; set; }

        public CreateContentCommandHandler(IContentRepository repository, 
            IUnitOfWork unitOfWork, 
            IStringLocalizer<CreateContentCommand> loc,
            IMapper mapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _loc = loc;
            _mapper = mapper;
        }

        public async Task<Result<Guid>> Handle(CreateContentCommand request, 
            CancellationToken cancellationToken)
        {
            var item = _mapper.Map<EvrenDev.Domain.Entities.Content>(request);
            await _repository.AddAsync(item);
            await _unitOfWork.Commit(cancellationToken);

            var message = string.Format(_loc["create_content_success"], item.Title);
            
            return Result<Guid>.Success(item.Id, message);
        }
    }
}