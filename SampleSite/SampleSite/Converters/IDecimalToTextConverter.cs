namespace SampleSite.Converters
{
    public interface INumberToTextConverter<in T>
    {
        string Convert(T amount);
    }
}