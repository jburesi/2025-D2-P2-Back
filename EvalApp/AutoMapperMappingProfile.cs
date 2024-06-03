using AutoMapper;
using EvalApp.Entities;
using EvalApp.Entities.Dto.DtoDown;

namespace EvalApp
{
    internal class AutoMapperMappingProfile : Profile
    {
        public AutoMapperMappingProfile()
        {
            CreateMap<EventDtoDown, Event>();
        }
    }
}
