using Microsoft.EntityFrameworkCore;

using MyApi.Extensions;
using MyApi.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// Configuration des services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();
builder.Services.AddAuthorization();

builder.Services.AddAutoMapper(typeof(Program));

// Appel de l'extension pour enregistrer les services
builder.Services.AddCustomServices();

// Configuration de Swagger
builder.Services.AddSwaggerConfiguration();

// Ajouter la politique CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp", policy =>
    {
        policy.WithOrigins("http://localhost:4200") // Origine Angular
              .AllowAnyHeader()                   // Autoriser tous les en-têtes
              .AllowAnyMethod();                  // Autoriser toutes les méthodes (GET, POST, etc.)
    });
});

// Configuration du contexte de la base de données
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 25)))
    );

// Configuration de l'authentification JWT
builder.Services.AddJwtAuthentication(builder.Configuration);

var app = builder.Build();

// Configuration du pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwaggerConfiguration(); // Utilisation de Swagger
}

app.UseCors("AllowAngularApp");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
