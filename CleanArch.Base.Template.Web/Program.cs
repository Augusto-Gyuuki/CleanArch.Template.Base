using CleanArch.Base.Template.Application;
using CleanArch.Base.Template.Infrastructure;
using CleanArch.Base.Template.Presentation;
using CleanArch.Base.Template.Web;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
        .AddWeb()
        .AddApplication()
        .AddPresentation()
        .AddInfrastructure();

    Console.ReadLine();
}

var app = builder.Build();
{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });

    app.UseHttpsRedirection();
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllers();
    app.Run();
}