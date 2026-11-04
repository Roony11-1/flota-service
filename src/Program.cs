using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using RentACar.src.Api.Mapper;
using RentACar.src.Core.Application.Service;
using RentACar.src.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);


builder.Services
    .AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseInMemoryDatabase("RentACar");
});

builder.Services.AddControllers();

builder.Services.AddScoped<SucursalService>();
builder.Services.AddScoped<VehiculoService>();

builder.Services.AddScoped<SucursalMapper>();
builder.Services.AddScoped<VehiculoMapper>();



var app = builder.Build();

app.MapControllers();

app.Run();