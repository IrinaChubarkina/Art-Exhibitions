namespace Core.AsyncMessaging;

public interface IMessageBusClient
{
    void PublishNewGallery(GalleryPublishedDto gallery);
}
