using AutoMapper;
using MediatR;
using EvrenDev.Application.DTOS.Settings.Department;
using EvrenDev.Application.Interfaces.Repositories;
using EvrenDev.Application.Specifications;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EvrenDev.Application.Features.Settings.Department.Queries.GetAll
{
    public class DepartmentsQueryHandler : IRequestHandler<DepartmentsQuery, List<DepartmentDto>>
    {
        private readonly IDepartmentRepository _repository;
        private readonly IMapper _mapper;

        public DepartmentsQueryHandler(IDepartmentRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<DepartmentDto>> Handle(DepartmentsQuery request, 
            CancellationToken cancellationToken)
        {
            var filterSpec = new DepartmentFilterSpecification(deleted: request.Deleted, 
                id: request.Id);

            var items = await _repository.GetItemsAsync(filterSpec, cancellationToken);
            var mappedItems = _mapper.Map<List<DepartmentDto>>(items);

            return mappedItems;
        }
    }
}