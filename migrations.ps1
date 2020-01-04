$catalogPath = "$PSScriptRoot\src\CoolStore.Modules.Catalog"
$inventoryPath = "$PSScriptRoot\src\CoolStore.Modules.Inventory"

Set-Location $catalogPath
& Get-ChildItem -Path "$catalogPath\Data\Migrations" -Include *.* -Recurse | ForEach-Object { $_.Delete()}
& dotnet build
& dotnet ef migrations add InitCatalogDbContext -c CatalogDbContext -o Data/Migrations
& dotnet ef database update

Set-Location $inventoryPath
& Get-ChildItem -Path "$inventoryPath\Data\Migrations" -Include *.* -Recurse | ForEach-Object { $_.Delete()}
& dotnet build
& dotnet ef migrations add InitInventoryDbContext -c InventoryDbContext -o Data/Migrations
& dotnet ef database update
