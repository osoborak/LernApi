namespace LernApi.Models.DTO
{
    public class UserWithToken : User
    {
        public string Token { get; set; }

        public UserWithToken(User user, string token)
        {
            Id = user.Id;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Login = user.Login;
            PasswordHash = user.PasswordHash;
            PasswordSalt = user.PasswordSalt;
            Token = token;
        }
    }
}
