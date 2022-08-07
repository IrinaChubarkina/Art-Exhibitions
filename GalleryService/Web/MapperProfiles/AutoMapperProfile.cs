namespace Web.MapperProfiles;

using AutoMapper;
using Core.EventMessaging;
using Core.Models;
using FacadeModels;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Gallery, GalleryResponse>()
            .ReverseMap();

        CreateMap<Gallery, CreateGalleryRequest>()
            .ReverseMap();
        
        CreateMap<GalleryResponse, GalleryPublishedDto>()
            .ReverseMap();
    }
}
