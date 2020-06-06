using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LernApi.Models;
using LernApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LernApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdvertisementsController : ControllerBase
    {

        private IAdvService _myService;
        private IMapper _mapper;
        public AdvertisementsController(IAdvService myService, IMapper mapper)
        {
            _myService = myService;
            _mapper = mapper;
        }

        [HttpGet]
        public IEnumerable<Advertisement> GetAllOffers()
        {
            var advs = _myService.GetAllOffers();
            return advs;
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _myService.DeleteOffer(id);
            return Ok();
        }
        [HttpPost]
        public Advertisement Create(Advertisement adv)
        {
            return _myService.CreateOffer(adv);
            
        }

        [HttpGet("{id}")]
        public ActionResult<Advertisement> GetOffer(int id)
        {
            return _myService.GetOffer(id);
        }
        [HttpPut]
        public IActionResult Assign(int userId, int adv)
        {
             _myService.AssignUserToOffer(userId, adv);
            return Ok();
        }



    }
}
