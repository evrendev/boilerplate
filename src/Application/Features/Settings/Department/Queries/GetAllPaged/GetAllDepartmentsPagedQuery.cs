using AutoMapper;
using MediatR;
using EvrenDev.Application.DTOS.Settings.Department;
using EvrenDev.Application.DTOS.Shared;
using EvrenDev.Application.Interfaces.Repositories;
using EvrenDev.Application.Specifications;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EvrenDev.Application.Interfaces.Result;

namespace EvrenDev.Application.Features.Settings.Department.Queries.GetAllPaged
{
    public class DepartmentsPagedQueryHandler : IRequestHandler<DepartmentsPagedQuery, PaginatedResult<BasicDepartmentDto>>
    {
        private readonly IDepartmentRepository _repository;
        private readonly IMapper _mapper;

        public DepartmentsPagedQueryHandler(IDepartmentRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<PaginatedResult<BasicDepartmentDto>> Handle(DepartmentsPagedQuery request, 
            CancellationToken cancellationToken)
        {
            var filterSpec = new DepartmentFilterSpecification(deleted: request.Deleted, 
                id: null);

            int totalCount = await _repository.CountAsync(filterSpec, cancellationToken);

            var pagedSpec = new DepartmentFilterPaginatedSpecification(
                skip: (request.PageNumber - 1) * request.PageSize,
                take: request.PageSize,
                deleted: request.Deleted);

            var items = await _repository.ListAsync(pagedSpec, cancellationToken);
            var data = _mapper.Map<List<BasicDepartmentDto>>(items);

            var response = PaginatedResult<BasicDepartmentDto>.Success(data, totalCount, request.PageNumber, request.PageSize);

            return response;
        }
    }
}