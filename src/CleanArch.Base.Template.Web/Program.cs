using CleanArch.Base.Template.Application;
using CleanArch.Base.Template.Infrastructure;
using CleanArch.Base.Template.Infrastructure.Options;
using CleanArch.Base.Template.Persistence;
using CleanArch.Base.Template.Presentation;
using CleanArch.Base.Template.Presentation.Common.Extensions;
using CleanArch.Base.Template.Presentation.Middlewares;
using CleanArch.Base.Template.Web;
using FastEndpoints;
using FastEndpoints.Swagger;

var builder = WebApplication.CreateBuilder(args);
{
    var infrastructureOptions = builder.Configuration.GetSection(InfrastructureOptions.SectionName).Get<InfrastructureOptions>();

    builder.Services
        .AddWeb()
        .AddPersistence()
        .AddApplication()
        .AddPresentation()
        .AddInfrastructure(infrastructureOptions);
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
    app.UseSwaggerUi3(c => c.ConfigureDefaults());

    app.UseRequestTimeInfo();
    app.UseHttpsRedirection();
    app.UseCustomExceptionHandler();
    app.Run();
}
