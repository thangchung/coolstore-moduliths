using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using CoolStore.UI.Blazor.Data;
using CoolStore.UI.Blazor.Services;
using System;
using Toolbelt.Blazor.Extensions.DependencyInjection;

namespace CoolStore.UI.Blazor
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            Env = env;
        }

        private IConfiguration Configuration { get; }

        private IWebHostEnvironment Env { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var builder = services.AddRazorPages();

#if DEBUG
            if (Env.IsDevelopment())
            {
                builder.AddRazorRuntimeCompilation();
            }
#endif

            services.AddServerSideBlazor();
            services.AddSingleton<WeatherForecastService>();

            services.AddI18nText();

            services.AddHttpClient("GraphQLClient",
                c => c.BaseAddress = new Uri($"{Configuration.GetValue<string>("CoolStoreApi:BaseAddress")}/graphql"));
            services.AddGraphQLClient();

            services.AddSingleton<CoolStoreService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
