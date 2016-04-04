using SampleSite.Controllers;

namespace SampleSite.Converters
{
    public class DecimalToTextConverter : INumberToTextConverter<decimal>
    {
        public string Convert(decimal amount)
        {
            return null;
        }
    }
}