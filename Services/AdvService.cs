using AutoMapper;
using LernApi.Models;
using LernApi.Models.Context;
using LernApi.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LernApi.Services
{
    public class AdvService : IAdvService
    {
        private readonly MyContext _myContext;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;

        public AdvService(MyContext myContext, IMapper mapper)
        {
            _mapper = mapper;
            _myContext = myContext; 
        }
        public void AssignUserToOffer(int userId, int adv)
        {
            var offer = _myContext.Advertisements.Find(adv);
            var user = _myContext.Users.Find(userId);
            offer.Users.Add(user);
            _myContext.SaveChanges();
        }

        public Advertisement CreateOffer(Advertisement adv)
        {
            _myContext.Advertisements.Add(adv);
            _myContext.SaveChanges();
            return adv;
        }

        public void DeleteOffer(int id)
        {
            var offer = _myContext.Advertisements.Find(id);
            if (offer != null)
            {
                _myContext.Advertisements.Remove(offer);
                _myContext.SaveChanges();
            }
        }

        public IEnumerable<Advertisement> GetAllOffers()
        {
            var advs = _myContext.Advertisements.Include(x => x.Users);           
            return advs;
            
        }

        public Advertisement GetOffer(int id)
        {
            return _myContext.Advertisements
                .Include(x => x.Users)
                .SingleOrDefault(x => x.Id == id);
        }
    }
}
