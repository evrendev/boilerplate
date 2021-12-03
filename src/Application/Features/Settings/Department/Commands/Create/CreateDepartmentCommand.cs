using EvrenDev.Application.Interfaces.Result;
using AutoMapper;
using MediatR;
using EvrenDev.Application.DTOS.Settings.Department;
using EvrenDev.Application.Interfaces.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EvrenDev.Application.Features.Settings.Department.Commands.Create
{ 
    public class CreateDepartmentCommandHandler : IRequestHandler<CreateDepartmentCommand, Result<Guid>>
    {
        private readonly IDepartmentRepository _repository;

        private readonly IMapper _mapper;

        private IUnitOfWork _unitOfWork { get; set; }

        public CreateDepartmentCommandHandler(IDepartmentRepository repository, 
            IUnitOfWork unitOfWork, 
            IMapper mapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<Guid>> Handle(CreateDepartmentCommand request, 
            CancellationToken cancellationToken)
        {
            var department = _mapper.Map<EvrenDev.Domain.Entities.Department>(request);
            await _repository.AddAsync(department);
            await _unitOfWork.Commit(cancellationToken);
            
            return Result<Guid>.Success(department.Id, $"{department.Title} departmanı başarılı bir şekilde eklendi.");
        }
    }
}