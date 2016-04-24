using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Azure.Mobile.Server.Tables;

namespace nbgService.DataObjects
{
    public class User : ITableData
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