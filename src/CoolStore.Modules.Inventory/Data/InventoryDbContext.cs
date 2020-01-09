using CoolStore.Protobuf.Inventories.V1;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Moduliths.Infra.Extensions;
using Moduliths.Infra.Helpers;
using System;
using System.Collections.Generic;
using System.Text.Json;

namespace CoolStore.Modules.Inventory.Data
{
    public class InventoryDbContext : DbContext
    {
        public InventoryDbContext(DbContextOptions<InventoryDbContext> options)
           : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Domain.Inventory> Inventories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(InventoryDbContext).Assembly);

            //seed data
            var invModels = "inventories.json".ReadData<List<InventoryDto>>(AppContext.BaseDirectory);
            Console.WriteLine(JsonSerializer.Serialize(invModels));
            foreach (var inv in invModels)
            {
                modelBuilder.Entity<Domain.Inventory>().HasData(
                    Domain.Inventory.Of((Domain.InventoryId)inv.Id.ConvertTo<Guid>(), inv.Location, inv.Description, inv.Website)
                );
            }
        }
    }

    public class InventoryDbContextDesignFactory : IDesignTimeDbContextFactory<InventoryDbContext>
    {
        public InventoryDbContext CreateDbContext(string[] args)
        {
            var connString = ConfigurationHelper.GetConfiguration(AppContext.BaseDirectory)?.GetConnectionString("MainDb");
            var optionsBuilder = new DbContextOptionsBuilder<InventoryDbContext>()
                .UseSqlServer(
                    connString,
                    sqlOptions =>
                    {
                        sqlOptions.MigrationsAssembly(GetType().Assembly.FullName);
                        sqlOptions.EnableRetryOnFailure(15, TimeSpan.FromSeconds(30), null);
                    }
                );

            Console.WriteLine(connString);
            return new InventoryDbContext(optionsBuilder.Options);
        }
    }
}
