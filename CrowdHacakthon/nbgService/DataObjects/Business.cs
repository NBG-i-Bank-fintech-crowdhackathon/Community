using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Azure.Mobile.Server.Tables;

namespace nbgService.DataObjects
{
    public class Business : ITableData
    {
        public string Id { get; set; }
        public byte[] Version { get; set; }
        public DateTimeOffset? CreatedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
        public bool Deleted { get; set; }
        public double Donation { get; set; }
        public double RoundUp { get; set; }
        public string Name { get; set; }
        public double Loan { get; set; }
        public double Paid { get; set; }
        public string Type { get; set; }
        public string ProfileImage { get; set; }
        public string BackImage { get; set; }
        public string Location { get; set; }
        public string Details { get; set; }
        public int Employees { get; set; }
        public int PayDay { get; set; }
        public string LocationDetails { get; set; }
    }
}