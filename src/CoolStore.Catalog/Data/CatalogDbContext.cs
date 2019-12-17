using CoolStore.Catalog.Domain;
using CoolStore.Protobuf.Catalogs.V1;
using Microsoft.EntityFrameworkCore;
using Moduliths.Infra.Extensions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace CoolStore.Catalog.Data
{
    public class CatalogDbContext : DbContext
    {
        public CatalogDbContext(DbContextOptions<CatalogDbContext> options)
           : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CatalogDbContext).Assembly);

            //seed data
            var seedData = Path.GetFullPath("products.json", AppContext.BaseDirectory);
            using StreamReader sr = new StreamReader(seedData);
            var readData = sr.ReadToEnd();
            var productModels = JsonConvert.DeserializeObject<List<CatalogProductDto>>(readData);

            foreach (var prod in productModels)
            {
                modelBuilder.Entity<Product>().HasData(
                    Product.Of((ProductId)(prod.Id.ConvertTo<Guid>()), new CreateProductRequest
                    {
                        Name = prod.Name,
                        Description = prod.Description,
                        ImageUrl = prod.ImageUrl,
                        Price = prod.Price,
                        InventoryId = prod.InventoryId
                    })
                );
            }
        }
    }
}
