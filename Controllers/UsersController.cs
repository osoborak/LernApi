using System;
using AutoMapper;
using LernApi.Models;
using LernApi.Models.DTO;
using LernApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LernApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : Controller
    {

        private IUserService _userService;
        private IMapper _mapper;


        public UsersController(IUserService userService,
        IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;

        }


        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] UserInfo userParam)
        {
            var user = _userService.Authenticate(userParam.UserName, userParam.Password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(user);
        }


        [HttpGet]
        public IActionResult GetAllUsers()
        {
            var users = _userService.GetAllUsers();
            return Ok(users);
        }


        [HttpGet("id")]
        public ActionResult<User> Get(int id)
        {
            var user = _userService.GetUser(id);

            if (user == null)
                return NotFound();

            return user;
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _userService.Delete(id);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody]UserInfo userParam)
        {
            // map dto to entity and set id
            var user = _mapper.Map<User>(userParam);
            user.Id = id;

            try
            {
                // save
                _userService.Update(user, userParam.Password);
                return Ok();
            }
            catch (Exception ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody]UserInfo userInfo)
        {
            try
            {
                var newUser = _userService.Create(userInfo);

                return StatusCode(201);
            }
            catch (Exception ex)
            {

                return BadRequest(new { message = ex.Message });
            }
        }

    }
}
