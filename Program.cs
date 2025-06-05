using Microsoft.EntityFrameworkCore;

using Infrastructure;
using Services.Interfaces;
using Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//Se crea la variable para la cadena de conexión
var Connectionstring = builder.Configuration.GetConnectionString("Connection");

//Registramos el servicio para la conexión
builder.Services.AddDbContext<DatabaseContext>(dbContext => dbContext.UseSqlServer(Connectionstring));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Se agrega los servicios 
builder.Services.AddScoped<IUserService, UserService>();

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
