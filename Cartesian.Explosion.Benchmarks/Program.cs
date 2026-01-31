using BenchmarkDotNet.Running;
using Cartesian.Explosion.Benchmarks.Presentation.Benchmarks;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    // Run benchmarks in here
    BenchmarkRunner.Run<CartesionBenchmark>();
}

app.Run();