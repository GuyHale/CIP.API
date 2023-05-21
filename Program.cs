using CIP.API.Identity;
using CIP.API.Interfaces;
using CIP.API.Models;
using CIP.API.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Data.Entity.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddIdentityCore<ApiUser>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<CIPIdentityDbContext>();

builder.Services.AddDbContext<CIPIdentityDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityDb") ?? string.Empty));

builder.Host.ConfigureLogging(logging =>
{
    logging.ClearProviders();
    logging.AddConsole();
});

builder.Services
    .AddSingleton<IDapperWrapper, DapperWrapper>()
    .AddSingleton<IDbConnectionFactory, SqlConnectionFactory>()
    .AddSingleton<ICryptocurrencyRetrieval, CryptocurrencyRetrieval>()
    .AddSingleton<ICustomAuthenticationService, CustomAuthenticationService>();

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
