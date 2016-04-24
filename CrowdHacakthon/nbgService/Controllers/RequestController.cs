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
    public class RequestController : TableController<Request>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            nbgContext context = new nbgContext();
            DomainManager = new EntityDomainManager<Request>(context, Request);
        }

        // GET tables/Request
        public IQueryable<Request> GetAllRequest()
        {
            return Query(); 
        }

        // GET tables/Request/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<Request> GetRequest(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/Request/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<Request> PatchRequest(string id, Delta<Request> patch)
        {
             return UpdateAsync(id, patch);
        }

        // POST tables/Request
        public async Task<IHttpActionResult> PostRequest(Request item)
        {
            Request current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/Request/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteRequest(string id)
        {
             return DeleteAsync(id);
        }
    }
}
