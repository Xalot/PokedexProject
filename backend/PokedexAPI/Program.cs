using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using log4net;
using log4net.Config;
using System.Reflection;
using System.Text;
using PokedexAPI.Repositories;
using PokedexAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Configurar log4net
var loggerRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
XmlConfigurator.Configure(loggerRepository, new FileInfo("log4net.config"));

ILog log = LogManager.GetLogger(typeof(Program));
log.Info("Aplicación iniciada, log4net configurado correctamente.");

// Configurar JWT Authentication
var key = Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Key"] ?? throw new ArgumentNullException("Jwt:Key is missing from configuration"));

// Configurar autenticación con JWT
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})

.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});

// Servicio de autorización
builder.Services.AddAuthorization();

// Añadir los controladores
builder.Services.AddControllers();

// Configurar el contexto de datos con la cadena de conexión
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Registrar el repositorio en el contenedor de dependencias
builder.Services.AddScoped<IUserRepository, UserRepository>();

// Registrar el servicio de usuarios
builder.Services.AddScoped<IUserService, UserService>();

// Registrar el servicio de auditoría
builder.Services.AddScoped<IAuditLogRepository, AuditLogRepository>();

// Registrar el servicio de Pokémon
builder.Services.AddHttpClient<IPokemonService, PokemonService>();

// Configurar Swagger con comentarios XML
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "PokedexAPI", Version = "v1" });

    // Definir el esquema de seguridad para JWT
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Por favor ingrese la palabra 'Bearer' [espacio] y luego el token",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });

    // Leer el archivo de comentarios XML generado por el proyecto
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "PokedexAPI v1");
    });
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
