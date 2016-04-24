using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Microsoft.Azure.Mobile.Server;
using nbgService.DataObjects;
using nbgService.Models;

namespace nbgService.Controllers
{
    public class ItemController : TableController<Item>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            nbgContext context = new nbgContext();
            DomainManager = new EntityDomainManager<Item>(context, Request);
        }

        // GET tables/Business
        public IQueryable<Item> GetAllItem()
        {
            return Query(); 
        }

        // GET tables/Business/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<Item> GetItem(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/Business/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<Item> PatchItem(string id, Delta<Item> patch)
        {
             return UpdateAsync(id, patch);
        }

        // POST tables/Business
        public async Task<IHttpActionResult> PostItem(Item item)
        {
            Item current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/Business/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteItem(string id)
        {
             return DeleteAsync(id);
        }
    }
}
