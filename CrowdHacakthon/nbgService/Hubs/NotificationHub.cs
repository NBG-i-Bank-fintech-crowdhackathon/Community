using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using nbgService.DataObjects;
using nbgService.Models;

namespace nbgService.Hubs
{
    public class NotificationHub : Hub
    {
        public void SendBusiness(string businessId,string itemId,int quantity,string userId)
        {
            Request req = new Request
            {
                Id = Guid.NewGuid().ToString().Replace("-",""),
                Accepted = false,
                BusinessId = businessId,
                //ItemId = itemId,
                //Quantity = quantity,
                Completed = false,
                UserId = userId
            };
            using (nbgContext cont = new nbgContext())
            {
                cont.Requests.Add(req);
                cont.SaveChanges();
            }
                Clients.All.ReceiveBusiness(req);
            
        }
        public void SendUser(string reqId, bool accepted)
        {
            Request req;
            using (nbgContext cont = new nbgContext())
            {
                req = cont.Requests.Find(reqId);
                req.Accepted = accepted;
                req.Completed = true;
                cont.Requests.Attach(req);
                cont.Entry(req).State = EntityState.Modified;
                cont.SaveChanges();
            }
            Clients.Client("user").ReceiveUser(req);
        }
    }

}