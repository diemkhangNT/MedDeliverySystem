using Medication.API.BackgroundServices;
using Medication.Application.Medication.Handlers;
using Medication.Domain.Interfaces;
using Medication.Infrastructure.Contexts;
using Medication.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IMedicationRepository, MedicationRepository>();
builder.Services.AddScoped<IUpsertMedicationHandler, UpsertMedicationHandler>();
builder.Services.AddScoped<IBroadcastMedicationHandler, BroadcastMedicationHandler>();

builder.Services.AddCors();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
var pharmacyContextOptions = new DbContextOptionsBuilder<MedicationDBContext>().UseSqlServer(connectionString);
builder.Services.AddScoped(context => pharmacyContextOptions);
builder.Services.AddDbContext<MedicationDBContext>(ServiceLifetime.Scoped);
builder.Services.AddHostedService<SqlDbMigrationService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("../swagger/v1/swagger.json", "Medication Service v1"));

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
