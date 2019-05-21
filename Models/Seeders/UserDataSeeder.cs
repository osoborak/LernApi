using System;
using System.Linq;
using LernApi.Models.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace LernApi.Models.Seeders
{
    public static class UserDataSeeder
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new UserContext(serviceProvider
                .GetRequiredService<DbContextOptions<UserContext>>()))
            {
                if (context.Users.Any())
                {
                    return;
                }

                context.Users.AddRange(
                    new User
                    {
                        FirstName = "Oskar",
                        LastName = "Soborak",
                        UserName = "oskarpl",
                        PasswordHash
                     = new byte[] { 255 },
                        PasswordSalt = new byte[] { 133 }
                    },
                    new User
                    {
                        FirstName = "Jan",
                        LastName = "Kowalski",
                        UserName = "Janpl",
                        PasswordHash
                     = new byte[] { 255 },
                        PasswordSalt = new byte[] { 133 }
                    },
                    new User
                    {
                        FirstName = "Ala",
                        LastName = "Kowalska",
                        UserName = "Alapl",
                        PasswordHash
                     = new byte[] { 255 },
                        PasswordSalt = new byte[] { 133 }
                    },
                    new User
                    {
                        FirstName = "Pawel",
                        LastName = "Kowalski",
                        UserName = "Pawelpl",
                        PasswordHash
                     = new byte[] { 255 },
                        PasswordSalt = new byte[] { 133 }
                    }
                );
                context.SaveChanges();
            }
        }
    }
}