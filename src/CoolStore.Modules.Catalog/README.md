### Data Migration

```
$ dotnet build
```

```
$ dotnet ef migrations add InitCatalogDbContext -c CatalogDbContext -o Data/Migrations
```

```
$ dotnet ef database update
```
