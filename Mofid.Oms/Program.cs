using System.Net;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Orleans.Clustering.Kubernetes;
using Orleans.Configuration;
using Orleans.Streams.Kafka.Config;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddOpenTelemetry()
    .WithMetrics(metrics =>
    {
        metrics
            .AddPrometheusExporter()
            .AddMeter("Microsoft.Orleans");
    })
    .WithTracing(tracing =>
    {
        // Set a service name
        tracing.SetResourceBuilder(
            ResourceBuilder.CreateDefault()
                .AddService(serviceName: "Oms", serviceVersion: "1.0"));

        tracing.AddSource("Microsoft.Orleans.Runtime");
        tracing.AddSource("Microsoft.Orleans.Application");
    });

builder.Host.UseOrleans((ctx, siloBuilder) => {
    //LocalHost Config
    // siloBuilder.UseLocalhostClustering();

    // siloBuilder.AddRedisGrainStorage("RedisStorage", (options) =>
    // {
    //     options.ConfigurationOptions = new ConfigurationOptions
    //     {
    //         EndPoints = { "172.23.44.11:6379"/*"localhost"*/ },
    //         DefaultDatabase = 5
    //     };
    // });
    siloBuilder.AddMemoryGrainStorage("RedisStorage");
    
    //Kuber Config
     siloBuilder.UseKubernetesHosting();
     siloBuilder.UseKubeMembership();
     
     // siloBuilder.UseRedisClustering("172.23.44.11:6379");
     siloBuilder.Configure<ClusterOptions>(options =>
     {
         options.ClusterId = "mofid-oms-cluster";
         options.ServiceId = "mofid-oms";
     });
     
     // siloBuilder.ConfigureEndpoints(siloPort: 11_111, gatewayPort: 30_000, advertisedIP: IPAddress.Parse("10.233.0.160"));
    
    // siloBuilder.AddRedisGrainStorage("PubSubStore", (options) =>
    // {
    //     options.ConfigurationOptions = new ConfigurationOptions
    //     {
    //         EndPoints = { "172.23.44.11:6379"/*"localhost"*/ },
    //         DefaultDatabase = 6
    //     };
    // });
    //
    // siloBuilder.AddKafka("KafkaProvider")
    //     .WithOptions(options =>
    //     {
    //         options.BrokerList = new[] { "localhost:9092" };
    //         options.ConsumerGroupId = "E2EGroup";
    //         options.ConsumeMode = ConsumeMode.StreamStart;
    //     
    //         options
    //             .AddTopic("orders-sent-to-exchange-sender-grain");
    //     })
    //     .AddJson()
    //     .AddLoggingTracker().Build();

    siloBuilder.AddActivityPropagation();
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapPrometheusScrapingEndpoint();

app.Run();