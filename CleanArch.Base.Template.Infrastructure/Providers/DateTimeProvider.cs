using CleanArch.Base.Template.Application.Common.Interfaces.Providers;

namespace CleanArch.Base.Template.Infrastructure.Providers;

internal class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}
