using Microsoft.EntityFrameworkCore;
using PokedexAPI.Repositories;
using PokedexAPI.Services;


var builder = WebApplication.CreateBuilder(args);

// Configurar el contexto de datos con la cadena de conexión
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Registrar el repositorio en el contenedor de dependencias
builder.Services.AddScoped<IUserRepository, UserRepository>();

// Registrar el servicio de usuarios
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<IAuditLogRepository, AuditLogRepository>();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
