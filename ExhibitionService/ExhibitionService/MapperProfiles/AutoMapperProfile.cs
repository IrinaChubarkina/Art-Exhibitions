namespace ExhibitionService.MapperProfiles;

using AsyncMessaging;
using AutoMapper;
using FacadeModels;
using Models;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Gallery, GalleryResponse>();

        CreateMap<Exhibition, ExhibitionResponse>();

        CreateMap<CreateExhibitionRequest, Exhibition>();

        CreateMap<GalleryPublishedDto, Gallery>()
            .ForMember(d => d.ExternalId, opt => opt.MapFrom(s => s.Id));
    }
}
