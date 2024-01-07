namespace Search.Api.Services;
public interface IDateTimeProvider
{
    DateTime UtcNow { get; }
}
