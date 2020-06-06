using Newtonsoft.Json;

namespace LernApi.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Login { get; set; }
        [JsonIgnore]
        public byte[] PasswordHash { get; set; }
        [JsonIgnore]
        public byte[] PasswordSalt { get; set; }
        public string Company { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }    
        public string Phone { get; set; }    


    }
}
