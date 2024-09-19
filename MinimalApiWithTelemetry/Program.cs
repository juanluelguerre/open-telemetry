using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using OpenTelemetry.Metrics;
using OpenTelemetry.Logs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenTelemetry()
    .WithTracing(tracing => tracing
        // The rest of your setup code goes here
        .AddConsoleExporter()
    )
    .WithMetrics(metrics => metrics
        // The rest of your setup code goes here
        .AddConsoleExporter()
    );

builder.Logging.AddOpenTelemetry(logging => {
    // The rest of your setup code goes here
    logging.AddConsoleExporter();
});


var app = builder.Build();



// Configure the HTTP request pipeline.
app.MapGet("/", () => "Hello World!");

app.Run();
