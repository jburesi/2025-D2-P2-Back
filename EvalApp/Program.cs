using EvalApp;
using EvalApp.Repository;
using EvalApp.Repository.Contracts;
using EvalApp.Repository.Repositories;
using EvalApp.Services;
using EvalApp.Services.Contracts;
using Microsoft.Azure.Functions.Worker;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text.Json;

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("local.settings.json")
    .Build();

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureServices(services =>
    {
        services.AddDbContext<EvalAppDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("DatabaseConnection"));
        });
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();
        services.Configure<JsonSerializerOptions>(options =>
        {
            options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        });
        services.AddScoped<IEventRepository, EventRepository>();
        services.AddScoped<IEventService, EventService>();
        services.AddAutoMapper(cfg => cfg.AddProfile<AutoMapperMappingProfile>());
    })
    .Build();

host.Run();
