namespace SampleSite.Controllers
{
    public interface INumberToTextConverter<in T>
    {
        string Convert(T amount);
    }
}