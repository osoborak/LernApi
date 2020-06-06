using System;
using System.Collections.Generic;
using System.Linq;
using LernApi.Models.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace LernApi.Models.Seeders
{
    public static class DataSeeder
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using var context = new MyContext(serviceProvider
                .GetRequiredService<DbContextOptions<MyContext>>());
            if (context.Users.Any())
            {
                return;
            }
            if (context.Advertisements.Any())
            {
                return;
            }
            context.Advertisements.AddRange(

               new Advertisement
               {
                   Description = "Sexi niunia, posprzata, ugotuje i dzieci wybolcuje",
                   Id = 70,
                   Tags = new string[] { "Android", "Vue" },
                   Title = "Podoba mi sie to co slysze"
               },
               new Advertisement
               {
                   Description = "S434343",
                   Id = 71,
                   Tags = new string[] { "Android" },
                   Title = "rwerew"
               },
               new Advertisement
               {
                   Description = "Sexi niunia, posprzata, ugotuje i dzieci wybolcuje",
                   Id = 69,
                   Tags = new string[] { "elo" },
                   Title = "Podoba mi sie to co slysze",
                   Users = new List<User>()
                   {    new User
                         {
                             FirstName = "Kamik",
                             LastName = "Koblarz",
                             Login = "Baba",
                             PasswordHash = new byte[] { 255 },
                             PasswordSalt = new byte[] { 133 },
                             Id = 105,
                             Address = "wiezienna",
                             Description = "kradne",
                             Company = "Rowerek",
                             Phone ="6969696961"
                         }
                   }
               },
                new Advertisement
                {
                    Description = "Sexi niunia, posprzata, ugotuje i dzieci wybolcuje",
                    Id = 68,
                    Tags = new string[] { "elo" },
                    Title = "Podoba mi sie to co slysze",
                    Users = new List<User>()
                    {   new User
                        {
                             FirstName = "Lukasz",
                             LastName = "Zabawowicz",
                             Login = "Lukaszpl",
                             PasswordHash = new byte[] { 255 },
                             PasswordSalt = new byte[] { 133 },
                             Id = 104,
                             Address = "Messerszmitowa",
                             Company = "Wawel",
                             Description = "hehe",
                             Phone = "500133249"
                        }
                    }
                }
            );
            context.SaveChanges();
            context.Users.AddRange(
                new User
                {
                    FirstName = "Jan",
                    LastName = "Kowalski",
                    Login = "Janpl",
                    PasswordHash = new byte[] { 255 },
                    PasswordSalt = new byte[] { 133 },
                    Address = "Swietojanska",
                    Company = "UG",
                    Description = "Prosze mi wierzyc na slowo",
                    Id = 100,
                    Phone = "501006022"
                },
                new User
                {
                    FirstName = "Marcin",
                    LastName = "Malinowski",
                    Login = "marcinpl",
                    PasswordHash = new byte[] { 255 },
                    PasswordSalt = new byte[] { 133 },
                    Address = "Sopocka",
                    Company = "WZR",
                    Description = "Jeszcze jak",
                    Id = 101,
                    Phone = "601006022"

                },
                new User
                {
                    FirstName = "Waclaw",
                    LastName = "Sosin",
                    Login = "Waclawpl",
                    PasswordHash = new byte[] { 255 },
                    PasswordSalt = new byte[] { 133 },
                    Address = "Rurska",
                    Company = "Ikea",
                    Description = "Jest super",
                    Id = 102,
                    Phone = "701006022"
                },
                new User
                {
                    FirstName = "Donatan",
                    LastName = "Wiebke",
                    Login = "Donatapl",
                    PasswordHash = new byte[] { 255 },
                    PasswordSalt = new byte[] { 133 },
                    Address = "Kolorowa",
                    Company = "Nasa",
                    Description = "rakieta leci",
                    Id = 103,
                    Phone = "880006022"

                }
            );

            context.SaveChanges();
        }
    }
}