using Deliver.Api;

var builder = WebApplication.CreateBuilder(args);

var app = builder
    .ConfigureServices()
    .ConfigurePipeline();

app.MapGet("/", () => "Welcome to Deliver!");

app.Run();
