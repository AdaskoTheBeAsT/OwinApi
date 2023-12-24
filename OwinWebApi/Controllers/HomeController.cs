using System.Web.Http;

namespace OwinWebApi.Controllers
{
    [AllowAnonymous]
    public class HomeController : ApiController
    {
        [HttpGet]
        public IHttpActionResult Index()
        {
            return Ok("API Works");
        }
    }
}
