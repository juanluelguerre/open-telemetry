// ReSharper disable StringLiteralTypo

using OpenTelemetry.Exporter;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace HelloOpenTelemetry.Extensions;

public static class OpenTelemetryExtensions
{
    public static void AddCustomOpenTelemetry(this WebApplicationBuilder builder)
    {
        const string serviceName = "helloopentelemetry";
        const string serviceVersion = "1.0.0";

        var resourceBuilder =
            ResourceBuilder.CreateDefault().AddService(serviceName, serviceVersion);

        builder.Services.AddOpenTelemetry()
            .WithMetrics(
                metrics => metrics
                    .SetResourceBuilder(resourceBuilder)
                    .AddAspNetCoreInstrumentation()
                    .AddHttpClientInstrumentation()
                    .AddConsoleExporter() // TODO: Not recommended for production
                    .AddOtlpExporter(otlp =>
                    {
                        otlp.Endpoint = new Uri("http://otel-collector:4317");
                        otlp.Protocol = OtlpExportProtocol.Grpc;
                    })
                    .AddPrometheusExporter()

            )
            .WithTracing(
                tracing => tracing
                    .SetResourceBuilder(resourceBuilder)
                    .AddSource(serviceName)
                    .AddAspNetCoreInstrumentation()
                    .AddHttpClientInstrumentation()
                    .AddConsoleExporter()
                    .AddOtlpExporter(
                        otlp =>
                        {
                            // If OTEL_EXPORTER_OTLP_ENDPOINT is set (take a look docker-compose.yml) otherwise set the otlp.Endpoint value.
                            // if none of the above is set, the default value (http://localhost:4317) will be used
                            //otlp.Endpoint = new Uri("http://jaeger:4317");
                            //
                            // otlp.Endpoint = new Uri("http://tempo:4317");
                            // otlp.Protocol = OtlpExportProtocol.Grpc;
                            //
                            otlp.Endpoint = new Uri("http://otel-collector:4317");
                        })
            );

        //builder.Logging.AddOpenTelemetry(logging => { logging.AddConsoleExporter(); });
        builder.Logging.AddOpenTelemetry(
            options =>
            {
                options.SetResourceBuilder(resourceBuilder);
                options.AddConsoleExporter();
                options.AddOtlpExporter(
                    opts =>
                    {
                        opts.Endpoint = new Uri("http://otel-collector:4317");
                        opts.Protocol = OtlpExportProtocol.Grpc;
                    });


                options.IncludeFormattedMessage = true;
                options.IncludeScopes = true;
                options.ParseStateValues = true;
            });

    }
}
