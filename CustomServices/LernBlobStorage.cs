using LernApi.Services.Blobs;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LernApi.CustomServices
{
    public static class LernBlobStorage
    {
        public static IServiceCollection AddLernBlobStorage(this IServiceCollection services)
        {
            services.AddScoped<ILernContainerService, LernContainer>();
            services.AddScoped<IBlobService, BlobService>();

            return services;
        }
    }
}
