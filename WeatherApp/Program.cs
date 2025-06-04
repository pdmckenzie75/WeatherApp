using Core.Interfaces;
using Data;
using Microsoft.EntityFrameworkCore;
using Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<WeatherDbContext>(options =>
    options.UseInMemoryDatabase("WeatherDb"));

// Ensure the database is created during application startup
builder.Services.AddHostedService<DatabaseInitializer>();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddScoped<ILocationsService, LocationsService>();
builder.Services.AddScoped<OpenMateoAPIService>();
builder.Services.AddScoped<IWeatherForecastService, WeatherForecastService>();
builder.Services.AddSingleton<WeatherCache>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

// DatabaseInitializer class
public class DatabaseInitializer : IHostedService
{
    private readonly IServiceProvider _serviceProvider;

    public DatabaseInitializer(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<WeatherDbContext>();
            await context.Database.EnsureCreatedAsync(cancellationToken);
        }
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}
