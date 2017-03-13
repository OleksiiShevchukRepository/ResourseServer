using System.Web.Http;

namespace ResourceServerOauth.Controllers
{
    [RoutePrefix("Math")]
    public class MathController : ApiController
    {
        [Authorize]
        [HttpGet]
        [Route("Add")]
        public IHttpActionResult AddTwoNumbers(int a, int b)
        {
            return Ok(a + b);
        }
    }
}