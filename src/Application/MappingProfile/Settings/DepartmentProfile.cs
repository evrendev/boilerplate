using AutoMapper;
using EvrenDev.Domain.Entities;
using EvrenDev.Application.DTOS.Shared;
using EvrenDev.Application.DTOS.Settings.Department;

namespace EvrenDev.Application.MappingProfile
{
    public class DepartmentProfile : Profile
    {
        public DepartmentProfile()
        {
            //Department Mapping
            CreateMap<Department, BasicDepartmentDto>();

            CreateMap<Department, DepartmentDto>()
                .ForMember(department =>  department.Title, expression => expression.MapFrom(d => d.Title.ToUpper()))
                .ForMember(departmentDto => departmentDto.CreationDate, 
                    expression => expression.MapFrom(
                        department => DateTimeFunctions.GetDetailsDate(department.CreationDateTime)))
                .ForMember(departmentDto => departmentDto.ModifiedDate, 
                    expression => expression.MapFrom(
                        department => department.LastModificationTime.HasValue
                            ? DateTimeFunctions.GetDetailsDate(department.LastModificationTime.Value)
                            : null))
                .ForMember(departmentDto => departmentDto.DeletionDate, 
                    expression => expression.MapFrom(
                        department => department.DeletionTime.HasValue 
                            ? DateTimeFunctions.GetDetailsDate(department.DeletionTime.Value)
                            : null));

            CreateMap<UpdateDepartmentCommand, Department>()
                .ForMember(department =>  department.Title, expression => expression.MapFrom(d => d.Title.ToUpper()))
                .ReverseMap();

            CreateMap<CreateDepartmentCommand, Department>()
                .ForMember(department =>  department.Title, expression => expression.MapFrom(d => d.Title.ToUpper()))
                .ReverseMap();

        }
    }
}