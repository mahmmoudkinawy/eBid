namespace SearchApi.Services;
public interface IDateTimeProvider
{
    DateTime UtcNow { get; }
}
