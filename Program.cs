using Microsoft.EntityFrameworkCore;

using MyApi.Data;
using MyApi.Extensions;

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

// Configuration du contexte de la base de données
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 25)))
    );

var app = builder.Build();

// Configuration du pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwaggerConfiguration(); // Utilisation de Swagger
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
