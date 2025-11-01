using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Net;
using HelloOpenTelemetry.Extensions;
using Microsoft.OpenApi.Models;
using OpenTelemetry.Logs;
using OpenTelemetry.Resources;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();

builder.AddCustomOpenTelemetry();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Hello OpenTelemetry API", Version = "v1" });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Hello OpenTelemetry API v1"));
}

app.UseOpenTelemetryPrometheusScrapingEndpoint();

var activitySource = new ActivitySource("HelloOpenTelemetry");

app.MapGet("/", async (ILoggerFactory loggerFactory) =>
    {
        //throw new Exception("This is an exception");

        using var activity = activitySource.StartActivity("Greetings");
        activity?.SetTag("Informal", "Hi");
        activity?.SetTag("Formal", "Good afternoon");

        Console.WriteLine(
            $"Activity: {activity?.OperationName}, Tags: {String.Join(", ", activity?.Tags?.Select(tag => $"{tag.Key}: {tag.Value}") ?? Array.Empty<string>())}");

        HttpClient httpClient = new();
        var logger = loggerFactory.CreateLogger("HelloOpenTelemetry");
        logger.LogInformation("Sample endpoint called");
        logger.LogWarning("Sample warning log");
        logger.LogError("Sample error log");
        logger.LogDebug("Sample debug log");
        logger.LogTrace("Sample trace log");
        await httpClient.GetStringAsync("https://example.com");
        return "Hello World !!!";
    })
    .WithName("GetHelloWorld")
    .WithOpenApi();

app.MapGet("/exception",  (ILoggerFactory loggerFactory) =>
    {
        var ex =  new Exception("This is an exception");
        
        var logger = loggerFactory.CreateLogger("HelloOpenTelemetry");
        logger.LogError(ex, "An exception occurred");
        
        throw new HttpRequestException("Random error occurred (simulated)", 
            ex, 
            (HttpStatusCode)Random.Shared.Next(400, 600));
    })
    .WithName("GetHelloWorldException")
    .WithOpenApi();

app.MapGet("/meter", (IMeterFactory meterFactory) => {
    var meter = meterFactory.Create("HelloOpenTelemetryApiMeter");
    var counter = meter.CreateCounter<long>("counter_meter");
    counter.Add(1);
    
    return "Meter incremented";
})
.WithName("GetHelloWorldMeter")
.WithOpenApi();

app.Run();
