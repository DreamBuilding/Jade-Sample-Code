using NUnit.Framework;
using SampleSite.Converters;

namespace SampleSite.Tests
{
    [TestFixture]
    internal class DecimalToTextConverterTests
    {
        [TestCase(30.50, "thirty dollars and fifty cents")]
        [TestCase(5, "five dollars")]
        [TestCase(1000,"one thousand dollars")]
        [TestCase(1000000,"one million dollars")]
        public void Does_Convert_Return_Correct_Text(decimal value, string expected)
        {
            //arrange
            var converter = new DecimalToTextConverter();

            //act
            var actual = converter.Convert(value);

            //assert
            Assert.AreEqual(expected,actual);
        }
    }
}