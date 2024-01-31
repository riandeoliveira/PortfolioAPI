using PortfolioAPI.Context;
using Microsoft.EntityFrameworkCore;
using PortfolioAPI.Repositories.Interfaces;
using PortfolioAPI.Repositories;
using PortfolioAPI.Services.Interfaces;
using PortfolioAPI.Services;
using PortfolioAPI.Entities;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddControllers()
    .AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower);

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// repositories
builder.Services.AddScoped<IBaseRepository<User>, BaseRepository<User>>();

// services
builder.Services.AddScoped<IUserService, UserService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.Run();
app.UseHttpsRedirection();
