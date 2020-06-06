using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using LernApi.Models;
using LernApi.Models.Context;
using LernApi.Models.DTO;
using LernApi.Utilities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace LernApi.Services
{
    public class UserService : IUserService
    {

        private readonly MyContext _userContext;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;

        public UserService(MyContext UserContext, IMapper mapper, IOptions<AppSettings> appSettings)
        {
            _userContext = UserContext;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }

        private User Validate(string username, string password)
        {

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return null;

            var user = _userContext.Users.SingleOrDefault(x => x.Login == username);

            if (user == null)
                return null;

            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;

            return user;
        }

        public UserWithToken Authenticate(string username, string password)
        {
            var user = Validate(username, password);

            if (user == null)
                return null;

            var token = GenerateToken(user.Id.ToString());

            return new UserWithToken(user, token);
        }



        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            if (storedHash.Length > 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length > 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }



        private string GenerateToken(string userId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, userId)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }


        public IEnumerable<UserInfo> GetAllUsers()
        {
            return _mapper.Map<IEnumerable<UserInfo>>(_userContext.Users);
        }


        public User GetUser(int id)
        {
            return _userContext.Users.Find(id); ;
        }

        public void Delete(int id)
        {
            var user = _userContext.Users.Find(id);
            if (user != null)
            {
                _userContext.Users.Remove(user);
                _userContext.SaveChanges();
            }
        }

        public void Update(User userParam, string password = null)
        {
            var user = _userContext.Users.Find(userParam.Id);

            if (user == null)
                throw new Exception("User not found");

            if (userParam.Login != user.Login)
            {
                // UserName has changed so check if the new UserName is already taken
                if (_userContext.Users.Any(x => x.Login == userParam.Login))
                    throw new Exception("Username " + userParam.Login + " is already taken");
            }

            // update user properties
            user.FirstName = userParam.FirstName;
            user.LastName = userParam.LastName;
            user.Login = userParam.Login;

            // update password if it was entered
            if (!string.IsNullOrWhiteSpace(password))
            {
                byte[] passwordHash, passwordSalt;
                CreatePasswordHash(password, out passwordHash, out passwordSalt);

                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
            }

            _userContext.Users.Update(user);
            _userContext.SaveChanges();
        }

        public User Create(UserInfo userInfo)
        {
            var password = userInfo.Password;
            var username = userInfo.Login;

            if (string.IsNullOrWhiteSpace(password))
                throw new Exception("Password is required");

            if (_userContext.Users.Any(x => x.Login == username))
                throw new Exception("Username \"" + username + "\" is already taken");

            var user = _mapper.Map<User>(userInfo);

            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            _userContext.Users.Add(user);
            _userContext.SaveChanges();

            return user;
        }


    }
}