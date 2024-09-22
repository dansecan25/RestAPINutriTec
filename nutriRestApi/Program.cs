using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using NutriRestApi.Services;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text.Json.Serialization;
using System.Text.Json;
using Serilog;
using Serilog.Events; // (optional, if needed for log levels)
using Serilog.Sinks.File; // For the File sink


var builder = WebApplication.CreateBuilder(args);

// Configure Serilog
Log.Logger = new LoggerConfiguration()
    .WriteTo.File("logs/log-.log", rollingInterval: RollingInterval.Day) // Logs to file (daily)
    .CreateLogger();

// Use Serilog as the logging provider
builder.Host.UseSerilog(Log.Logger);

// Configurar servicios
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        options.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });

// Registrar otros servicios
builder.Services.AddSingleton<UserService>(); // Registrar UserService
builder.Services.AddSingleton<ClientService>(); // Registrar ClientService
builder.Services.AddSingleton<NutritionistService>(); // Registrar NutritionistService
builder.Services.AddSingleton<AdminService>(); // Registrar AdminService
builder.Services.AddSingleton<ProductService>(); // Registrar ProductService
builder.Services.AddSingleton<DishService>(); // Registrar DishService

// Configurar CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp",
        builder =>
        {
            builder.WithOrigins("http://localhost:4200") // Origen de tu aplicación Angular
                   .AllowAnyHeader() // Permitir cualquier encabezado
                   .AllowAnyMethod(); // Permitir cualquier método HTTP (GET, POST, etc.)
        });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "NutriRestApi", Version = "v1" });
});

var app = builder.Build();

// Configurar la canalización HTTP.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "NutriRestApi V1");
    });
}
else
{
    app.UseHttpsRedirection();
}

// Aplicar la política de CORS antes de la autorización
app.UseCors("AllowAngularApp");

app.UseAuthorization();

app.MapControllers();

app.Run();