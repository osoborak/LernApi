using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace LernApi.Services.Blobs
{
    public class LernContainer : ILernContainerService
    {
        private readonly IConfiguration _config;
        public LernContainer(IConfiguration config)
        {
            _config = config;
        }
        public CloudBlobContainer GetCloudBlobContainer()
        {
            var storageAccount = CloudStorageAccount.Parse(_config.GetConnectionString("blobCS"));
            var container = storageAccount.CreateCloudBlobClient().GetContainerReference("lern");
            return container;
        }
    }
}
