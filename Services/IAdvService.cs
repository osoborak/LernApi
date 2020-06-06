using LernApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LernApi.Services
{
    public interface IAdvService
    {      
        IEnumerable<Advertisement> GetAllOffers();
        Advertisement GetOffer(int id);
        Advertisement CreateOffer(Advertisement adv);
        void DeleteOffer(int id);
        void AssignUserToOffer(int userId, int adv);
    }
}
