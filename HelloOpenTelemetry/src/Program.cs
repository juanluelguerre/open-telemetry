using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Trace;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenTelemetry()
    .WithTracing(tracing => tracing
        .AddAspNetCoreInstrumentation()
        .AddHttpClientInstrumentation()
        .AddConsoleExporter()
    )
    .WithMetrics(metrics => metrics
        .AddAspNetCoreInstrumentation()
        .AddHttpClientInstrumentation()
        .AddConsoleExporter()
        .AddPrometheusExporter()
    );

builder.Logging.AddOpenTelemetry(logging =>
{
    // The rest of your setup code goes here
    logging.AddConsoleExporter();
});


var app = builder.Build();


app.UseOpenTelemetryPrometheusScrapingEndpoint();

// Configure the HTTP request pipeline.
app.MapGet("/", () => "Hello World!");

app.Run();
