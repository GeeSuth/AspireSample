var builder = DistributedApplication.CreateBuilder(args);

var cache = builder.AddRedis("cache");

var apiService = builder.AddProject<Projects.AspireSample_ApiService>("apiservice");

var grafana = builder.AddContainer("grafana", "grafana/grafana")
                     .WithHttpEndpoint(targetPort: 3000, name: "http");

builder.AddProject<Projects.AspireSample_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(cache)
    .WithReference(apiService);

builder.Build().Run();
