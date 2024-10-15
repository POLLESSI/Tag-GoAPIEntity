# MyApi

## Description

MyApi est une API REST construite avec ASP.NET Core et utilise Entity Framework Core pour la gestion des bases de données. L'API fournit des endpoints pour gérer des entités telles que les utilisateurs, les groupes, et les inscriptions.

## Prérequis

- .NET 6/7/8 SDK installé.
- SQL Server ou une autre base de données supportée par Entity Framework Core.
- Entity Framework Core pour la gestion de la base de données.

## Installation

1. Cloner le dépôt :
   git clone https://github.com/username/MyApi.git
   cd MyApi

2. Restaurer les dépendances :
   dotnet restore

3. Configurer la base de données :

   Ouvrez le fichier appsettings.json et configurez la chaîne de connexion pour la base de données :
   
   "ConnectionStrings": {
       "DefaultConnection": "Server=your_server;Database=your_db;User Id=your_user;Password=your_password;"
   }

4. Appliquer les migrations et initialiser la base de données :
   Si vous utilisez Entity Framework Core pour gérer la base de données, appliquez les migrations :
   dotnet ef database update

   Si vous avez besoin de créer de nouvelles migrations :
   dotnet ef migrations add InitialCreate

## Lancement de l'API

1. Compilation et exécution de l'API :
   Vous pouvez exécuter l'application en mode développement avec la commande suivante :
   dotnet run --configuration Debug

   Ou en mode production :
   dotnet run --configuration Release

2. Tester l'API :
   L'API sera disponible par défaut à l'adresse suivante : http://localhost:5000 ou https://localhost:5001 (selon la configuration SSL).

   Vous pouvez accéder à la documentation des endpoints avec Swagger en visitant :
   https://localhost:5001/swagger/index.html

## Commandes utiles

- Nettoyer le projet :
  dotnet clean

- Construire le projet :
  dotnet build

- Exécuter les tests :
  Si vous avez des tests unitaires ou d'intégration configurés, exécutez-les avec :
  dotnet test

## Technologies utilisées

- ASP.NET Core pour l'API web.
- Entity Framework Core pour l'accès aux données.
- SQL Server (ou autre) comme base de données relationnelle.

## Contribuer

Les contributions sont les bienvenues ! Veuillez soumettre une pull request ou ouvrir une issue pour discuter des changements majeurs avant de les proposer.

---

Merci d'utiliser MyApi !
