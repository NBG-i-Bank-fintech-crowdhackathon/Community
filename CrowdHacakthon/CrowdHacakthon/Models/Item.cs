using System;
using System.Collections.Generic;
using System.Linq;

namespace CrowdHacakthon.Models
{
    public class Item 
    {
        public string Id { get; set; }
        public byte[] Version { get; set; }
        public DateTimeOffset? CreatedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
        public bool Deleted { get; set; }

        public string Name { get; set; }
        public double Price { get; set; }
        public string BusinessId { get; set; }
        public string Image { get; set; }

    }
}