
using API.Model.Domain;
using API.Model.DTO;
using AutoMapper;
namespace API.Mappings


{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<RegionDto, Region>()
                .ReverseMap();
            CreateMap<AddRegionRequestDto, Region>().ReverseMap();
            CreateMap<PutRegionDto, Region>().ReverseMap();
        }
    }

    
}
