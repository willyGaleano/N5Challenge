using AutoMapper;
using N5.Challenge.Core.Application.DTOs.PermissionTypes;
using N5.Challenge.Core.Domain.Entities;

namespace N5.Challenge.Core.Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<PermissionTypes, PermissionTypesDTO>()
                .ForMember(x => x.PermissionTypeId, y => y.MapFrom(z => z.Id))
                .ForMember(x => x.Descripcion, y => y.MapFrom(z => z.Descripcion));
        }
    }
}
