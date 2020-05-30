using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LernApi.Services.Blobs
{
    public class BlobService : IBlobService
    {
        private readonly ILernContainerService _storageService;
        public BlobService(ILernContainerService storageService)
        {
            _storageService = storageService;
        }
        public async Task<HashSet<string>> GetBlobs()
        {
            var container = _storageService.GetCloudBlobContainer();
            var url = GetBlobSasUri(container);
            return await url;

        }
        private static async Task<HashSet<string>> GetBlobSasUri(CloudBlobContainer container, string policyName = null)
        {
            string sasBlobToken;
            var resultSegment = await container.ListBlobsSegmentedAsync(null);
            var blobNames = new HashSet<string>();
            var blobsUrls = new HashSet<string>();

            foreach (IListBlobItem item in resultSegment.Results)
            {
                if (item.GetType() == typeof(CloudBlockBlob))
                {
                    CloudBlockBlob blob = (CloudBlockBlob)item;
                    blobNames.Add(blob.Name);
                }

                foreach (var blobitem in blobNames)
                {
                    CloudBlockBlob blob = container.GetBlockBlobReference(blobitem);

                    if (policyName == null)
                    {
                        SharedAccessBlobPolicy adHocSAS = new SharedAccessBlobPolicy()
                        {
                            SharedAccessExpiryTime = DateTime.UtcNow.AddHours(24),
                            Permissions = SharedAccessBlobPermissions.Read
                        };
                        sasBlobToken = blob.GetSharedAccessSignature(adHocSAS);
                    }
                    else
                    {
                        sasBlobToken = blob.GetSharedAccessSignature(null, policyName);
                    }
                    blobsUrls.Add(blob.Uri + sasBlobToken);
                }
            }
                return blobsUrls;
        }
    }
}