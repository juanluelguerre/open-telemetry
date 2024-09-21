using System.Diagnostics;
using HelloOpenTelemetry.Extensions;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Hello OpenTelemetry API", Version = "v1" });
});

builder.AddCustomOpenTelemetry();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Hello OpenTelemetry API v1"));
}

app.UseOpenTelemetryPrometheusScrapingEndpoint();

var activitySource = new ActivitySource("HelloOpenTelemetry");

app.MapGet("/", () =>
    {
        using var activity = activitySource.StartActivity("Greetings");
        activity?.SetTag("Informal", "Hi");
        activity?.SetTag("Formal", "Good afternoon");

        Console.WriteLine(
            $"Activity: {activity?.OperationName}, Tags: {String.Join(", ", activity?.Tags?.Select(tag => $"{tag.Key}: {tag.Value}") ?? Array.Empty<string>())}");


        return "Hello World!";
    })
    .WithName("GetHelloWorld")
    .WithOpenApi();

app.Run();
