namespace Core.EventMessaging;

public interface IMessageBusClient
{
    void PublishNewGallery(GalleryPublishedDto gallery);
}
