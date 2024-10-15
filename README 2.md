# Projet de Gestion des Activités Scolaires (API ASP.NET Core)

## Description
Ce projet est une API de gestion des activités scolaires pour les écoles. Elle permet de gérer les utilisateurs, les activités, et les inscriptions tout en intégrant un système d'authentification JWT pour sécuriser l'accès à certaines fonctionnalités.

### Principales fonctionnalités :
- **CRUD des utilisateurs** : Inscription et gestion des utilisateurs (élèves, enseignants, administrateurs).
- **Gestion des activités** : Création et gestion des activités (sportives, culturelles, etc.).
- **Gestion des inscriptions** : Les utilisateurs peuvent s'inscrire à des activités via l'API.
- **Authentification et Autorisation JWT** : Sécurisation des endpoints avec JSON Web Token (JWT).
- **Swagger** : Documentation API via Swagger, avec support pour l'authentification via JWT.

## Technologies utilisées :
- **ASP.NET Core** 6.0
- **Entity Framework Core** (EF Core) avec MySQL
- **JWT (JSON Web Token)** pour l'authentification
- **AutoMapper** pour les transformations entre modèles et DTO
- **Swagger/OpenAPI** pour la documentation de l'API

## Prérequis

- .NET SDK 6.0
- MySQL 8.0 ou plus récent
- Visual Studio Code ou un autre éditeur compatible
- Outil de gestion des packages comme `NuGet` pour les dépendances

## Installation

1. **Cloner le repository** :
  ```bash
    git clone <url-du-repository>
    cd <nom-du-projet>
  ```
2. **Configurer la base de données MySQL** :
- Créez une base de données MySQL.
- Mettez à jour la chaîne de connexion dans `appsettings.json` :
  ```json
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=school_db;User=root;Password=your_password;"
  }
  ```
3. **Restaurer les packages NuGet** :
  ```bash
    dotnet restore
  ```

4. **Appliquer les migrations à la base de données** :
  ```bash
    dotnet ef database update
  ```

4. **Lancer l'application** :
  ```bash
    dotnet run
  ```