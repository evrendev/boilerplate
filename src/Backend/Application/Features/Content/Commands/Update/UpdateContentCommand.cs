using EvrenDev.Application.Interfaces.Result;
using MediatR;
using EvrenDev.Application.DTOS.Content;
using EvrenDev.Application.Interfaces.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;

namespace EvrenDev.Application.Features.Content.Commands.Update
{
    public class UpdateContentCommandHandler : IRequestHandler<UpdateContentCommand, Result<Guid>>
    {
        private readonly IContentRepository _repository;
        private IUnitOfWork _unitOfWork { get; set; }
        private readonly IStringLocalizer<UpdateContentCommand> _loc;

        public UpdateContentCommandHandler(IContentRepository repository, 
            IStringLocalizer<UpdateContentCommand> loc,
            IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _loc = loc;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(UpdateContentCommand command, 
            CancellationToken cancellationToken)
        {
                var item = await _repository.GetByIdAsync(command.Id);

                if (item == null)
                {
                    return Result<Guid>.Fail("Veri bulunamadı.");
                }
                else
                {
                    item.LanguageId = command.LanguageId;
                    item.Title = command.Title;
                    item.Overview = command.Overview;
                    item.Body = command.Body;
                    item.Image = command.Image;
                    item.MenuPosition = command.MenuPosition;
                    item.PublishDate = command.PublishDate;
                    item.MetaTitle = command.MetaTitle;
                    item.MetaDescription = command.MetaDescription;
                    item.MetaKeywords = command.MetaKeywords;

                    await _repository.UpdateAsync(item);
                    await _unitOfWork.Commit(cancellationToken);

                    var message = string.Format(_loc["update_content_success"], item.Title);
                    
                    return Result<Guid>.Success(item.Id, message);
                }
        }
    }
}