using DAL;
using DAL.Repositories;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);

// DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Реєстрація репозиторію
builder.Services.AddScoped<IGameRepository, GameRepository>();

builder.Services.AddControllers();
var app = builder.Build();

app.MapControllers();
app.Run();