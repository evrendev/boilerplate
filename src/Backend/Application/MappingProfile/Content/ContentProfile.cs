using AutoMapper;
using EvrenDev.Domain.Entities;
using EvrenDev.Application.DTOS.Shared;
using EvrenDev.Application.DTOS.Content;
using EvrenDev.Application.Enums.Language;
using System.Linq;

namespace EvrenDev.Application.MappingProfile
{
    public class ContentProfile : Profile
    {
        public ContentProfile()
        {
            //Content Mapping
            CreateMap<Content, ContentDto>()
                .ForMember(contentDto =>  contentDto.Language, 
                    expression => expression.MapFrom(
                        content => Languages.FromValue(content.LanguageId)))
                .ForMember(contentDto =>  contentDto.MenuPositions, 
                    expression => expression.MapFrom(content => content.MenuPosition.Select(mp => MenuPositions.FromValue(mp))))
                .ForMember(contentDto => contentDto.Image, 
                    expression => expression.MapFrom(
                        content => ImageFunctions.Get("contents", content.Image)))
                .ForMember(contentDto => contentDto.PublishDate, 
                    expression => expression.MapFrom(
                        content => DateTimeFunctions.GetDetailsDate(content.PublishDate)))
                .ForMember(contentDto => contentDto.CreationDate, 
                    expression => expression.MapFrom(
                        content => DateTimeFunctions.GetDetailsDate(content.CreationDateTime)))
                .ForMember(contentDto => contentDto.ModifiedDate, 
                    expression => expression.MapFrom(
                        content => content.LastModificationTime.HasValue
                            ? DateTimeFunctions.GetDetailsDate(content.LastModificationTime.Value)
                            : null))
                .ForMember(contentDto => contentDto.DeletionDate, 
                    expression => expression.MapFrom(
                        Content => Content.DeletionTime.HasValue 
                            ? DateTimeFunctions.GetDetailsDate(Content.DeletionTime.Value)
                            : null));

            CreateMap<Content, ContentBasicDto>()
                .ForMember(contentDto =>  contentDto.Language, 
                    expression => expression.MapFrom(
                        content => Languages.FromValue(content.LanguageId)))
                .ForMember(contentDto => contentDto.Image, 
                    expression => expression.MapFrom(
                        content => ImageFunctions.Get("contents", content.Image)));

            CreateMap<UpdateContentCommand, Content>()
                .ReverseMap();

            CreateMap<CreateContentCommand, Content>()
                .ReverseMap();

        }
    }
}