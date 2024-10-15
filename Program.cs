using Microsoft.EntityFrameworkCore;
using AdminDevicesAPI.Data;
using AdminDevicesAPI;
using AdminDevicesAPI.Interfaces;
using AdminDevicesAPI.Repositories;
using AdminDevicesAPI.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DeviceContext>(options =>
    options.UseInMemoryDatabase("DeviceDatabase"));

builder.Services.AddScoped<IDeviceRepository, DeviceRepository>();
builder.Services.AddScoped<IDeviceService, DeviceService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyHeader()
              .AllowAnyMethod()
              .AllowAnyOrigin();
    });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

var app = builder.Build();

app.UseCors("AllowAll");

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    SeedData.Initialize(services);
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Admin Devices API v1");
    });
}

app.UseAuthorization();
app.MapControllers();

app.Run();
