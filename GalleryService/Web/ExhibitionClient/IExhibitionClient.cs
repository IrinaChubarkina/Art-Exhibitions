namespace Web.ExhibitionClient;

using Web.FacadeModels;

public interface IExhibitionClient
{
    Task SendGalleryToExhibition(GalleryResponse gallery);
}
