using System;
using System.Web.Http;
using System.Web.Http.Description;

namespace SampleSite.Controllers
{
    [RoutePrefix("api")]
    public class ConversionController : ApiController
    {
        private readonly INumberToTextConverter<decimal> _numberToTextConverter;

        public ConversionController(INumberToTextConverter<decimal> numberToTextConverter)
        {
            _numberToTextConverter = numberToTextConverter;
        }

        [HttpPost]
        [ResponseType(typeof(string))]
        [Route("{amount:alpha}/convert-to-word")]
        public IHttpActionResult ConverToWord(string amount)
        {
            return BadRequest(); //ideally this would not be used and 'not found' would be an acceptable responce to not diffferenciate between invalid and null values.
        }

        [HttpPost]
        [ResponseType(typeof(string))]
        [Route("{amount:decimal}/convert-to-word")]
        public IHttpActionResult ConverToWord(decimal amount)
        {
            try
            {
                var convertedToWords = _numberToTextConverter.Convert(amount);

                return Ok(convertedToWords);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
