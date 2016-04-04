namespace SampleSite.Controllers
{
    public class DecimalToTextConverter : INumberToTextConverter<decimal>
    {
        public string Convert(decimal amount)
        {
            return null;
        }
    }
}