using AutoMapper;
using NZwalksApi.Models.Domain;
using NZwalksApi.Models.DTO;

namespace NZwalksApi.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Region, RegionDto>().ReverseMap();
            CreateMap<AddRegionRequest,Region>().ReverseMap();
            CreateMap<UpdateRegionRequest, Region>().ReverseMap();
            CreateMap<AddWalkRequest, Walk>().ReverseMap();
            CreateMap<Walk, WalkDto>().ReverseMap();
            CreateMap<DifficultyDto,Difficulty>().ReverseMap();
            CreateMap<UpdateWalkRequest, Walk>().ReverseMap();
        }
    }
}
