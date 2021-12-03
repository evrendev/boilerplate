using EvrenDev.Application.Interfaces.Result;
using MediatR;
using EvrenDev.Application.DTOS.Settings.Department;
using EvrenDev.Application.Interfaces.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EvrenDev.Application.Features.Settings.Department.Commands.Update
{
    public class UpdateDepartmentCommandHandler : IRequestHandler<UpdateDepartmentCommand, Result<Guid>>
    {
        private readonly IDepartmentRepository _repository;
        private IUnitOfWork _unitOfWork { get; set; }

        public UpdateDepartmentCommandHandler(IDepartmentRepository repository, 
            IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(UpdateDepartmentCommand command, 
            CancellationToken cancellationToken)
        {
                var item = await _repository.GetByIdAsync(command.Id);

                if (item == null)
                {
                    return Result<Guid>.Fail("Veri bulunamadı.");
                }
                else
                {
                    item.Title = command.Title ?? item.Title;

                    await _repository.UpdateAsync(item);
                    await _unitOfWork.Commit(cancellationToken);
                    return Result<Guid>.Success(item.Id, $"{item.Title} departmanı başarılı bir şekilde güncellendi.");
                }
        }
    }
}