using CleanArch.Base.Template.Application;
using CleanArch.Base.Template.Infrastructure;
using CleanArch.Base.Template.Infrastructure.Settings;
using CleanArch.Base.Template.Integration;
using CleanArch.Base.Template.Persistence;
using CleanArch.Base.Template.Presentation;
using CleanArch.Base.Template.Presentation.Common.Extensions;
using CleanArch.Base.Template.Presentation.Middlewares;
using FastEndpoints;

var builder = WebApplication.CreateBuilder(args);
{
    var infrastructureSettings = builder.Configuration
        .GetSection(InfrastructureSettings.SectionName)
        .Get<InfrastructureSettings>();

    builder.Services
        .AddPersistence()
        .AddApplication()
        .AddPresentation()
        .AddIntegration()
        .AddInfrastructure(infrastructureSettings);
}

var app = builder.Build();
{
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });

    app.UseFastEndpoints(c =>
    {
        c.Endpoints.RoutePrefix = "api";
    });

    app.UseOpenApi();

    app.UseRequestTimeInfo();
    app.UseHttpsRedirection();
    app.UseCustomExceptionHandler();

    app.UseAuthorization();
    app.Run();
}