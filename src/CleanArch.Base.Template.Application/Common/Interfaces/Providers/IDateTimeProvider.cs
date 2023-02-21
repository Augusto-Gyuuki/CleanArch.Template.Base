namespace CleanArch.Base.Template.Application.Common.Interfaces.Providers;
public interface IDateTimeProvider
{
    DateTime UtcNow { get; }
}
