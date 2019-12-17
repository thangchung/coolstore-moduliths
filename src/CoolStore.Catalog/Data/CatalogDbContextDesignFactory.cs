using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Moduliths.Domain;
using Moduliths.Infra.Helpers;
using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace CoolStore.Catalog.Data
{
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

            /*Assembly.GetExecutingAssembly()
                .ExportedTypes
                .Where(x => !x.IsGenericTypeDefinition && !x.IsAbstract && x.BaseType == typeof(IdentityBase<Guid>))
                .ToList().ForEach(idType =>
                {
                    var typeOfIdentity = typeof(StronglyTypedIdConverter<>).MakeGenericType(idType);
                    TypeDescriptor.AddAttributes(idType, new TypeConverterAttribute(typeOfIdentity));
                });*/

            return new CatalogDbContext(optionsBuilder.Options);
        }

        public class StronglyTypedIdConverter<TIdentity> : TypeConverter where TIdentity : IdentityBase<Guid>
        {
            public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
            {
                return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
            }

            public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
            {
                var stringValue = value as string;
                if (!string.IsNullOrEmpty(stringValue) && Guid.TryParse(stringValue, out var guid))
                {
                    return IdentityFactory.Create<TIdentity, Guid>(guid);
                }

                return base.ConvertFrom(context, culture, value);
            }
        }
    }
}
