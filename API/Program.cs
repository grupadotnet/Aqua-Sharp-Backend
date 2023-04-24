global using Microsoft.EntityFrameworkCore;
global using Models.Entities;
global using AutoMapper;
using System.Text.Json.Serialization;
using Aqua_Sharp_Backend.Contexts;
using Aqua_Sharp_Backend.Interfaces;
using Aqua_Sharp_Backend.Middleware;
using Aqua_Sharp_Backend.Services;
using Aqua_Sharp_Backend.Seeder;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
builder.Services.AddControllers().AddJsonOptions(options => 
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddScoped<AquariumSeeder>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<Context>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("Default"));
});


#region Inject services
builder.Services.AddScoped<ErrorHandlingMiddleware>();

builder.Services.AddScoped<IAquariumService, AquariumService>();
builder.Services.AddScoped<IConfigService, ConfigService>();
builder.Services.AddScoped<IDeviceService, DeviceService>();
builder.Services.AddScoped<IMeasurementService, MeasurementService>();

builder.Services.AddHostedService<MqttClientService>();
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.

var scope = app.Services.CreateScope();
var seeder = scope.ServiceProvider.GetRequiredService<AquariumSeeder>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    seeder.Seed();
}

app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
