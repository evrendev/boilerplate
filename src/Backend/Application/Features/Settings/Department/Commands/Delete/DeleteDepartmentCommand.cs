using EvrenDev.Application.Interfaces.Result;
using MediatR;
using EvrenDev.Application.DTOS.Settings.Department;
using EvrenDev.Application.Interfaces.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;

namespace EvrenDev.Application.Features.Settings.Department.Commands.Delete
{
    public class DeleteDepartmentCommandHandler : IRequestHandler<DeleteDepartmentCommand, Result<Guid>>
    {
        private readonly IDepartmentRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStringLocalizer<DeleteDepartmentCommand> _loc;

        public DeleteDepartmentCommandHandler(IDepartmentRepository repository,
            IStringLocalizer<DeleteDepartmentCommand> loc, 
            IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _loc = loc;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(DeleteDepartmentCommand command, 
            CancellationToken cancellationToken)
        {
            var item = await _repository.GetByIdAsync(command.Id);
            await _repository.DeleteAsync(item);
            await _unitOfWork.Commit(cancellationToken);
            
            var message = string.Format(_loc["delete_content_success"], item.Title, item.Deleted ? _loc["deleted"] : _loc["restored"]);

            return Result<Guid>.Success(item.Id, message);
        }
    }
}