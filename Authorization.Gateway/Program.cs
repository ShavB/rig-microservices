using Microsoft.OpenApi.Models;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Service API", Version = "v1" });
});

builder.Configuration.SetBasePath(builder.Environment.ContentRootPath)
.AddJsonFile($"ocelot.json", optional: true, reloadOnChange: true).AddEnvironmentVariables();
builder.Services.AddOcelot(builder.Configuration);

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Service API V1");
});
app.UseOcelot().Wait();

app.Run();
