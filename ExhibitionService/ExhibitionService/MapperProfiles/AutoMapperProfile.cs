namespace ExhibitionService.MapperProfiles;

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
    }
}
