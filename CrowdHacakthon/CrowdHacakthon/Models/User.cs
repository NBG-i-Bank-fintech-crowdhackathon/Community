using System;
using System.Collections.Generic;
using System.Linq;

namespace CrowdHacakthon.Models
{
    public class User 
    {
        public string Id { get; set; }
        public byte[] Version { get; set; }
        public DateTimeOffset? CreatedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
        public bool Deleted { get; set; }

        public string Name { get; set; }
        public string Surname { get; set; }

        public string ProfileImage { get; set; }
        public double Balance { get; set; }
        public string CardNumber { get; set; }
        public int Points { get; set; }
    }
}