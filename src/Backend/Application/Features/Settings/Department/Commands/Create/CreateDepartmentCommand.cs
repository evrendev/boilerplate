using EvrenDev.Application.Interfaces.Result;
using AutoMapper;
using MediatR;
using EvrenDev.Application.DTOS.Settings.Department;
using EvrenDev.Application.Interfaces.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;

namespace EvrenDev.Application.Features.Settings.Department.Commands.Create
{ 
    public class CreateDepartmentCommandHandler : IRequestHandler<CreateDepartmentCommand, Result<Guid>>
    {
        private readonly IDepartmentRepository _repository;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<CreateDepartmentCommand> _loc;
        private IUnitOfWork _unitOfWork { get; set; }

        public CreateDepartmentCommandHandler(IDepartmentRepository repository, 
            IStringLocalizer<CreateDepartmentCommand> loc,
            IUnitOfWork unitOfWork, 
            IMapper mapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _loc = loc;
            _mapper = mapper;
        }

        public async Task<Result<Guid>> Handle(CreateDepartmentCommand request, 
            CancellationToken cancellationToken)
        {
            var item = _mapper.Map<EvrenDev.Domain.Entities.Department>(request);
            await _repository.AddAsync(item);
            await _unitOfWork.Commit(cancellationToken);

            var message = string.Format(_loc["create_content_success"], item.Title);
            
            return Result<Guid>.Success(item.Id, message);
        }
    }
}