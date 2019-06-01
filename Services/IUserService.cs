using System.Collections.Generic;
using LernApi.Models;
using LernApi.Models.DTO;

namespace LernApi.Services
{
    public interface IUserService
    {

        UserWithToken Authenticate(string username, string password);
        IEnumerable<UserInfo> GetAllUsers();
        User GetUser(int id);
        User Create(UserInfo userInfo);
        void Delete(int id);
        void Update(User user, string password);

    }
}
