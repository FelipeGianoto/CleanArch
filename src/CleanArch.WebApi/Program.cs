using CleanArch.Infra.IoC;
using CleanArch.WebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddPresentation()
    .AddInfrastructure(builder.Configuration);

var app = builder.Build();

app.UseGlobalExceptionHandler();

app.UsePresentation(app.Environment);

app.Run();
