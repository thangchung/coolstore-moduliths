using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Moduliths.Infra.Helpers;
using System;

namespace CoolStore.Modules.Localization.Data
{
    public sealed class LocalizationDbContext : DbContext
    {
        public LocalizationDbContext(DbContextOptions<LocalizationDbContext> options)
           : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Domain.Culture> Cultures { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(LocalizationDbContext).Assembly);
        }
    }

    public class LocalizationDbContextDesignFactory : IDesignTimeDbContextFactory<LocalizationDbContext>
    {
        public LocalizationDbContext CreateDbContext(string[] args)
        {
            var connString = ConfigurationHelper.GetConfiguration(AppContext.BaseDirectory)?.GetConnectionString("MainDb");
            var optionsBuilder = new DbContextOptionsBuilder<LocalizationDbContext>()
                .UseSqlServer(
                    connString ?? throw new InvalidOperationException(),
                    sqlOptions =>
                    {
                        sqlOptions.MigrationsAssembly(GetType().Assembly.FullName);
                        sqlOptions.EnableRetryOnFailure(15, TimeSpan.FromSeconds(30), null);
                    }
                );

            Console.WriteLine(connString);
            return new LocalizationDbContext(optionsBuilder.Options);
        }
    }
}
