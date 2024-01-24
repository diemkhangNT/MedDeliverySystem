using MedicineOrder.API.BackgroundServices;
using MedicineOrder.Application.Medicine.Handlers;
using MedicineOrder.Application.MedicineOrder.Handlers;
using MedicineOrder.Domain.Interfaces;
using MedicineOrder.Infrastructure.Contexts;
using MedicineOrder.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddCors();



builder.Services.AddScoped<IMedicineRepository, MedicineRepository>();
builder.Services.AddScoped<IMedicineOrderRepository, MedicineOrderRepository>();
builder.Services.AddScoped<IGetListMedicinesHandler, GetListMedicinesHandler>();
builder.Services.AddScoped<IGetListMedicineOrderHandler, GetListMedicineOrderHandler>();
builder.Services.AddScoped<IMessageHandler, MessageHandler>();
builder.Services.AddScoped<IUpsertMedicineOrderHandler, UpsertMedicineOrderHandler>();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
var medicineOrderContextOptions = new DbContextOptionsBuilder<MedicineOrderDbContext>().UseSqlServer(connectionString);
builder.Services.AddScoped(context => medicineOrderContextOptions);
builder.Services.AddDbContext<MedicineOrderDbContext>(ServiceLifetime.Scoped);
builder.Services.AddHostedService<SqlDbMigrationService>();


builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("../swagger/v1/swagger.json", "MedicineOrder API v1"));
app.UseHttpsRedirection();

app.UseCors(builder =>
{
    builder
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader()
        .WithExposedHeaders("*");
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
