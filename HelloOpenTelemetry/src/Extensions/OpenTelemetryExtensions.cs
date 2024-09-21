using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace HelloOpenTelemetry.Extensions;

public static class OpenTelemetryExtensions
{
    public static void AddCustomOpenTelemetry(this WebApplicationBuilder builder)
    {
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
    }
}
