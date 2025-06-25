# aspnetcore-identity-example

## Identity pages
### command to generate Identity Pages
1. ``dotnet aspnet-codegenerator identity -dc ApplicationDbContext --files "Account.Login;Account.Logout;Account.Register"``
2. You can find the generated pages at ``yourdomain.domain/Identity/Account/Login``

## Database update
### command to add new migration
1. ``dotnet ef migrations add <YourNewMigrationName>``
2. ``dotnet ef database update ``