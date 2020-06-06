using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LernApi.Models
{
    public class Advertisement
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string[] Tags { get; set; }
        public List<User> Users { get; set; } = new List<User>();
    }
}
