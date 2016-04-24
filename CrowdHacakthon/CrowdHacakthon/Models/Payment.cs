using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CrowdHacakthon.Converters;
using Newtonsoft.Json;

namespace CrowdHacakthon.Models
{
    public class Payment
    {
        public string Id { get; set; }
        public byte[] Version { get; set; }
        public DateTimeOffset? CreatedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
        public bool Deleted { get; set; }
        [JsonConverter(typeof(DateTimeToLocalDateTime))]
        public DateTime? Date { get; set; }
        public string BusinessId { get; set; }
        public int Type { get; set; }
        public double Amount { get; set; }
    }
}
