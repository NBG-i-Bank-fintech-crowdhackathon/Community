using System;
using System.Collections.Generic;
using System.Linq;

namespace CrowdHacakthon.Models
{
    public class Request 
    {
        public string Id { get; set; }
        public byte[] Version { get; set; }
        public DateTimeOffset? CreatedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
        public bool Deleted { get; set; }

        public string BusinessId { get; set; }
        public string UserId { get; set; }
        public int Amount { get; set; }
        public bool Completed { get; set; }
        public bool Accepted { get; set; }
    }
}