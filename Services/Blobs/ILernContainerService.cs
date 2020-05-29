using Microsoft.WindowsAzure.Storage.Blob;

namespace LernApi.Services.Blobs
{
    public interface ILernContainerService
    {
        CloudBlobContainer GetCloudBlobContainer();
    }
}