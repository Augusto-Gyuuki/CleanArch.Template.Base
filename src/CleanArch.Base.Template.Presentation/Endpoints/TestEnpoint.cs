using FastEndpoints;

namespace CleanArch.Base.Template.Presentation.Endpoints;

public sealed class TestEndpoint : EndpointWithoutRequest
{
    public override void Configure()
    {
        Get("all");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}
