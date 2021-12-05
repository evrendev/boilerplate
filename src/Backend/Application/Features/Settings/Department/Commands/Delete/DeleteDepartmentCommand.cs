using EvrenDev.Application.Interfaces.Result;
using MediatR;
using EvrenDev.Application.DTOS.Settings.Department;
using EvrenDev.Application.Interfaces.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EvrenDev.Application.Features.Settings.Department.Commands.Delete
{
    public class DeleteDepartmentCommandHandler : IRequestHandler<DeleteDepartmentCommand, Result<Guid>>
    {
        private readonly IDepartmentRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteDepartmentCommandHandler(IDepartmentRepository repository, 
            IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(DeleteDepartmentCommand command, 
            CancellationToken cancellationToken)
        {
            var item = await _repository.GetByIdAsync(command.Id);
            await _repository.DeleteAsync(item);
            await _unitOfWork.Commit(cancellationToken);
            return Result<Guid>.Success(item.Id, $"{item.Title} departmanı başarılı bir şekilde {(item.Deleted ? "silindi" : "geri yüklendi")}");
        }
    }
}