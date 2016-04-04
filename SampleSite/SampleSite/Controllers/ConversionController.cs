using System.Web.Http;

namespace SampleSite.Controllers
{
    [RoutePrefix("api")]
    public class ConversionController : ApiController
    {
        [HttpGet]
        [Route("{amount}/convert-to-word")]
        public IHttpActionResult ConverToWord(string amount)
        {
            return BadRequest();
        }
    }
}
