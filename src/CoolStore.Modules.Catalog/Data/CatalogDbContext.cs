using CoolStore.Modules.Catalog.Domain;
using CoolStore.Protobuf.Catalogs.V1;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Moduliths.Infra.Extensions;
using Moduliths.Infra.Helpers;
using System;
using System.Collections.Generic;

namespace CoolStore.Modules.Catalog.Data
{
    public sealed class CatalogDbContext : DbContext
    {
        public CatalogDbContext(DbContextOptions<CatalogDbContext> options)
           : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CatalogDbContext).Assembly);

            //seed data
            var categoryModels = "categories.json".ReadData<List<CategoryDto>>(AppContext.BaseDirectory);
            //Console.WriteLine(categoryModels.SerializeObject());
            foreach (var cat in categoryModels)
            {
                modelBuilder.Entity<Category>().HasData(
                    Category.Of(
                        (CategoryId)cat.Id.ConvertTo<Guid>(),
                        cat.Name
                        )
                );
            }

            var productModels = "products.json".ReadData<List<CatalogProductDto>>(AppContext.BaseDirectory);
            //Console.WriteLine(productModels.SerializeObject());
            foreach (var prod in productModels)
            {
                modelBuilder.Entity<Product>().HasData(
                    Product.Of(
                        (ProductId)prod.Id.ConvertTo<Guid>(),
                        new CreateProductRequest
                        {
                            Name = prod.Name,
                            Description = prod.Description,
                            ImageUrl = prod.ImageUrl,
                            Price = prod.Price,
                            InventoryId = prod.InventoryId,
                            CategoryId = prod.CategoryId
                        })
                );
            }
        }
    }

    public class CatalogDbContextDesignFactory : IDesignTimeDbContextFactory<CatalogDbContext>
    {
        public CatalogDbContext CreateDbContext(string[] args)
        {
            var connString = ConfigurationHelper.GetConfiguration(AppContext.BaseDirectory)?.GetConnectionString("MainDb");
            var optionsBuilder = new DbContextOptionsBuilder<CatalogDbContext>()
                .UseSqlServer(
                    connString,
                    sqlOptions =>
                    {
                        sqlOptions.MigrationsAssembly(GetType().Assembly.FullName);
                        sqlOptions.EnableRetryOnFailure(15, TimeSpan.FromSeconds(30), null);
                    }
                );

            Console.WriteLine(connString);
            return new CatalogDbContext(optionsBuilder.Options);
        }
    }
}
