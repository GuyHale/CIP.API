using CIP.API.Interfaces;
using CIP.API.Services;
using System.Data.Entity.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Host.ConfigureLogging(logging =>
{
    logging.ClearProviders();
    logging.AddConsole();
});

builder.Services
    .AddSingleton<IDapperWrapper, DapperWrapper>()
    .AddSingleton<IDbConnectionFactory, SqlConnectionFactory>()
    .AddSingleton<ICryptocurrencyRetrieval, CryptocurrencyRetrieval>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseRouting()
    .UseEndpoints(endpoints =>
    {
        endpoints.MapGet("/", async context =>
        {
            await context.Response.WriteAsync($"{System.Reflection.Assembly.GetExecutingAssembly().GetName()?.Name} running");
        });
    });

await app.RunAsync();
