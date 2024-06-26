using System.Text.Json.Serialization;
using Aqua_Sharp_Backend.Contexts;
using Aqua_Sharp_Backend.Interfaces;
using Aqua_Sharp_Backend.Middleware;
using Aqua_Sharp_Backend.Services;
using Aqua_Sharp_Backend.Seeder;
using Microsoft.AspNetCore.Identity;
using Aqua_Sharp_Backend;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using NLog.Web;
using Microsoft.AspNetCore.Authorization;
using Aqua_Sharp_Backend.Authorization;
using Microsoft.OpenApi.Models;
using Aqua_Sharp_Backend.SignalR;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var authenticationSettings = new AuthenticationSettings();
builder.Services.AddSingleton(authenticationSettings);
builder.Configuration.GetSection("Authentication").Bind(authenticationSettings);
builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = "Bearer";
    option.DefaultScheme = "Bearer";
    option.DefaultChallengeScheme = "Bearer";
}).AddJwtBearer(cfg =>
{
    cfg.RequireHttpsMetadata = false;
    cfg.SaveToken = true;
    cfg.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = authenticationSettings.JwtIssuer,
        ValidAudience = authenticationSettings.JwtIssuer,
        ValidateLifetime= true,
        ClockSkew = TimeSpan.Zero,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSettings.JwtKey)),
    };
});
builder.Services.AddControllers().AddJsonOptions(options => 
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddScoped<AquariumSeeder>();
builder.Services.AddScoped<IPasswordHasher<Auth>, PasswordHasher<Auth>>();
builder.Services.AddScoped<IAuthorizationHandler, AquariumResourceOperationRequirementHandler>();
builder.Services.AddScoped<IUserContextService, UserContextService>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSignalR();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => {
    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
        new OpenApiSecurityScheme
        {
            Reference= new OpenApiReference
            {
                Type=ReferenceType.SecurityScheme,
                Id="Bearer"
            }
        },
        new string[]{}
        }
    });
});
builder.Services.AddDbContext<Context>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});
builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
});

#region Inject services
builder.Services.AddScoped<ErrorHandlingMiddleware>();

builder.Services.AddScoped<IAquariumService, AquariumService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IDeviceService, DeviceService>();
builder.Services.AddScoped<IMeasurementService, MeasurementService>();
builder.Services.AddSingleton<IMqqtClientService, MqqtService>();
builder.Services.AddHostedService<MeasurementMqttClientService>();

builder.Services.AddCors();
#endregion

// Setup NLog for Dependency injection
builder.Host.UseNLog();

var app = builder.Build();

// Configure the HTTP request pipeline.

var scope = app.Services.CreateScope();
var seeder = scope.ServiceProvider.GetRequiredService<AquariumSeeder>();
seeder.Migrate();

if (app.Environment.IsDevelopment())
{
    seeder.Seed();
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseHttpsRedirection();
app.UseRouting();
app.UseMiddleware<WebSocketsMiddleware>();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.UseCors(
x => x
            .AllowAnyMethod()
            .AllowAnyHeader()
            .SetIsOriginAllowed(origin => true) // allow any origin
            .AllowCredentials()
               ); // allow credentials

app.MapHub<AquaSharpHub>("/aquahub");

app.Run();
