using FluentValidation;
using Instituto.Mappers;
using Instituto.Modelos;
using Instituto.Repositorios;
using Instituto.Validators;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IMongoClient, MongoClient>(
    sp => new MongoClient(builder.Configuration.GetConnectionString("MongoDBCS"))
);

// Validadodres
builder.Services.AddScoped<IValidator<CrearEstudianteDTO>, EstudianteValidator>();

// Repositorios
builder.Services.AddScoped<IEstudianteRepositorio, EstudianteRepositorio>();

// Mappers
builder.Services.AddAutoMapper(typeof(EstudianteMapper));

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
