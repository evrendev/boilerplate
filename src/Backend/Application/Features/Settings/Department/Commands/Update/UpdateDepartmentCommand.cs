using EvrenDev.Application.Interfaces.Result;
using MediatR;
using EvrenDev.Application.DTOS.Settings.Department;
using EvrenDev.Application.Interfaces.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;

namespace EvrenDev.Application.Features.Settings.Department.Commands.Update
{
    public class UpdateDepartmentCommandHandler : IRequestHandler<UpdateDepartmentCommand, Result<Guid>>
    {
        private readonly IDepartmentRepository _repository;
        private IUnitOfWork _unitOfWork { get; set; }
        private readonly IStringLocalizer<UpdateDepartmentCommand> _loc;

        public UpdateDepartmentCommandHandler(IDepartmentRepository repository,
            IStringLocalizer<UpdateDepartmentCommand> loc,
            IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _loc = loc;
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

                    var message = string.Format(_loc["update_content_success"], item.Title);
                    
                    return Result<Guid>.Success(item.Id, message);
                }
        }
    }
}