using Microsoft.EntityFrameworkCore;
using Pharmacy.API.BackgroundServices;
using Pharmacy.Application.Pharmacy.Handlers;
using Pharmacy.Domain.Interfaces;
using Pharmacy.Infrastructure.Contexts;
using Pharmacy.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IGetListPharmacyHandler, GetListPharmacyHandler>();
builder.Services.AddScoped<IBroadcastPharmaciesHandler, BroadcastPharmaciesHandler>();
builder.Services.AddScoped<IPharmacyRepository, PharmacyRepository>();

builder.Services.AddCors();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
var pharmacyContextOptions = new DbContextOptionsBuilder<PharmacyDbContext>().UseSqlServer(connectionString);
builder.Services.AddScoped(context => pharmacyContextOptions);
builder.Services.AddDbContext<PharmacyDbContext>(ServiceLifetime.Scoped);
builder.Services.AddHostedService<SqlDbMigrationService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("../swagger/v1/swagger.json", "Pharmacy Query API v1"));

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
