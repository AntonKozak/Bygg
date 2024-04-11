using AutoMapper;

namespace API.Helpers;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<Core.Entities.Product, Dtos.ProductDto>()
            .ForMember(d => d.Category, o => o.MapFrom(s => s.Category.Name))
            .ForMember(d => d.ProductType, o => o.MapFrom(s => s.ProductType.Name))
            .ForMember(d => d.ProductBrand, o => o.MapFrom(s => s.ProductBrand.Name))
            .ForMember(d => d.MainPhotoUrl, o => o.MapFrom(s => s.Photos.Where(p => p.IsMain).FirstOrDefault().Url));
        CreateMap<Core.Entities.Photo, Dtos.PhotoDto>();
    }
}
