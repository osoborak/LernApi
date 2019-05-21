using System;
using LernApi.Models.Context;
using LernApi.Models.Seeders;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace LernApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    var context = services.GetRequiredService<UserContext>();
                    context.Database.Migrate();
                    UserDataSeeder.Initialize(services);

                } catch(Exception ex)
                {
                    var logger =
                        services.GetRequiredService<ILogger<Program>>();
                    logger
                        .LogError(ex, "An error occured on seeding");
                }
            }

            host.Run();

        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseUrls("http://localhost:4000");
    }
}
