global using Microsoft.EntityFrameworkCore;
global using Models.Entities;
using Aqua_Sharp_Backend.Contexts;
using Aqua_Sharp_Backend.Interfaces;
using Aqua_Sharp_Backend.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<Context>(options => { options.UseNpgsql(builder.Configuration.GetConnectionString("Default")); });

#region Dependency Injection
builder.Services.AddScoped<IAquariumService, AquariumService>();
builder.Services.AddScoped<IMeasurmentService, MeasurmentService>();
builder.Services.AddScoped<IConfigService, ConfigService>();

#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
