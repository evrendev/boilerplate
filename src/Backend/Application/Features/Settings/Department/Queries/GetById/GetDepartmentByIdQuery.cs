using AutoMapper;
using MediatR;
using EvrenDev.Application.DTOS.Settings.Department;
using EvrenDev.Application.Interfaces.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace EvrenDev.Application.Features.Settings.Department.Queries.GetById
{
    public class GetDepartmentByIdQueryHandler : IRequestHandler<GetDepartmentByIdQuery, DepartmentDto>
    {
        private readonly IDepartmentRepository _repository;
        private readonly IMapper _mapper;

        public GetDepartmentByIdQueryHandler(IDepartmentRepository repository, 
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<DepartmentDto> Handle(GetDepartmentByIdQuery query, 
            CancellationToken cancellationToken)
        {
            var item = await _repository.GetByIdWithDetailsAsync(query.Id, cancellationToken);
            var mappedProduct = _mapper.Map<DepartmentDto>(item);

            return mappedProduct;
        }
    }
}