using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.IO;
using CFKC.VPV.Services.DecisionMatrix;
using CFKC.VPV.Entities;
using CFKC.VPV.Services;


namespace CFKC.VPV
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                    .AddJsonOptions(opt =>
                    {
                        opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    });

            services.AddSingleton(Configuration);

            services.AddEntityFrameworkSqlServer();

            services.AddScoped<SqlParcelData>();

            services.AddScoped<CFKCAddressApiData>();

            services.AddScoped<MatrixResolver>();

            // Row Number for paging is required when using 
            // System.Linq   Skip() and Take() operaters with SQL Express 08
            services.AddDbContext<CodeForKcDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("LAN"),
                                 sqlOpt => sqlOpt.UseRowNumberForPaging()));
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
                {
                    HotModuleReplacement = true
                });
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            // This will change later just a shim for now.
            app.UseNodeModules(
                root: Directory.GetCurrentDirectory(),
                enableDirectoryBrowsing: env.IsDevelopment());

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapSpaFallbackRoute(
                    name: "spa-fallback",
                    defaults: new { controller = "Home", action = "Index" });
            });
        }
    }
}
