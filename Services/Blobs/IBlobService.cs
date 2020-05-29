using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LernApi.Services.Blobs
{
    public interface IBlobService
    {
        Task<HashSet<string>> GetBlobs();
    }
}
