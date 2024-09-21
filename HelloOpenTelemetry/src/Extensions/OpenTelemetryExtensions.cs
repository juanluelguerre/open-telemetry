// ReSharper disable StringLiteralTypo

using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace HelloOpenTelemetry.Extensions;

public static class OpenTelemetryExtensions
{
    public static void AddCustomOpenTelemetry(this WebApplicationBuilder builder)
    {
        var resourceBuilder = ResourceBuilder.CreateDefault()
            .AddService(serviceName: "helloopentelemetry", serviceVersion: "1.0.0");

        builder.Services.AddOpenTelemetry()
            .WithTracing(tracing => tracing
                .SetResourceBuilder(resourceBuilder)
                .AddSource("HelloOpenTelemetry")
                .AddAspNetCoreInstrumentation()
                .AddHttpClientInstrumentation()
                .AddConsoleExporter()
                .AddOtlpExporter(otlp =>
                {
                    // If OTEL_EXPORTER_OTLP_ENDPOINT is set (take a look docker-compose.yml) otherwise set the otlp.Endpoint value.
                    // if none of the above is set, the default value (http://localhost:4317) will be used
                    otlp.Endpoint = new Uri("http://jaeger:4317");
                    otlp.Protocol = OpenTelemetry.Exporter.OtlpExportProtocol.Grpc;
                })
            )
            .WithMetrics(metrics => metrics
                .AddAspNetCoreInstrumentation()
                .AddHttpClientInstrumentation()
                .AddConsoleExporter()
                .AddPrometheusExporter()
            );

        builder.Logging.AddOpenTelemetry(logging => { logging.AddConsoleExporter(); });
    }
}
