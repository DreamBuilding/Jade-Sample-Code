using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Owin.Testing;
using NUnit.Framework;
using Owin;

namespace SampleSite.Tests.Controllers
{
    [TestFixture]
    internal class ConversionControllerTests
    {
        private TestServer _server;

        [SetUp]
        public void Setup()
        {
           _server = TestServer.Create(app =>
            {
                var startup = new Startup();

                startup.Configuration(app);
            });
        }

        [TearDown]
        public void TearDown()
        {
            _server.Dispose();
        }

        [TestCase(30.50)]
        [TestCase(5)]
        [TestCase(1000)]
        [TestCase(1000000)]
        public async Task Do_ValidValues_Return_Ok(decimal value)
        {
            //act
            var response = await _server.HttpClient.PostAsync(string.Format("/api/{0}/convert-to-word", value), new StringContent(string.Empty));

            //assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestCase(30.50, "thirty dollars and fifty cents")]
        [TestCase(5, "five dollars")]
        [TestCase(1000, "one thousand dollars")]
        [TestCase(1000000, "one million dollars")]
        public async Task Do_ValidValues_Return_CorrectValues(decimal value, string expected)
        {
            //act
            var response = await _server.HttpClient.PostAsync(string.Format("/api/{0}/convert-to-word", value), new StringContent(string.Empty));

            var actual = await response.Content.ReadAsStringAsync();

            //assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public async Task Does_Get_Return_MethodNotAllowed()
        {
            //act
            var response = await _server.HttpClient.GetAsync("/api/30/convert-to-word");

            //assert
            Assert.AreEqual(HttpStatusCode.MethodNotAllowed, response.StatusCode);
        }

        [Test]
        public async Task Does_ValueLessThanEqualZero_Return_BadRequest()
        {
            //act
            var response = await _server.HttpClient.PostAsync("/api/0.99/convert-to-word", new StringContent(string.Empty));

            //assert
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode, "Input dollar amount <=0 is not allowed"); // case says >=0 but that makes no sense, assumed typo.
        }

        [Test]
        public async Task Does_ValueGreaterThanOneMillionDollars_Return_BadRequest()
        {
            //act
            var response = await _server.HttpClient.PostAsync("/api/1000000.01/convert-to-word", new StringContent(string.Empty));

            //assert
            Assert.AreEqual(HttpStatusCode.NotAcceptable, response.StatusCode, "Maximium input dollar amount is 1 million dollars");
        }

        [Test]
        public async Task Does_Null_Return_404()
        {
            //act
            var response = await _server.HttpClient.PostAsync("/api//convert-to-word", new StringContent(string.Empty));

            //assert
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode, "Null is not a valid input value"); //this is not a good error code 406 would be better
        }

        [Test]
        public async Task Does_NonNumericValue_Return_BadRequest()
        {
            //act
            var response = await _server.HttpClient.PostAsync("/api/abc/convert-to-word", new StringContent(string.Empty));

            //assert
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode, "Input value must be a number");
        }
    }
}
