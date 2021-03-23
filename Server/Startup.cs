using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MiniLinkLogic.Libraries.MiniLink.Core.Domain;
using MiniLinkLogic.Libraries.MiniLink.Data;
using MiniLinkLogic.Libraries.MiniLink.Data.Context;
using MiniLinkLogic.Libraries.MiniLink.Services;
using System.Linq;

namespace MiniLink.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddMemoryCache();

            services.AddDbContext<MiniLinkContext>(options =>
               options.UseSqlServer( Configuration.GetConnectionString("DefaultConnection")));


            services.AddScoped<IBaseRepository<LinkEntry>, BaseRepository<LinkEntry>>();
            services.AddScoped<IBaseRepository<LinkEntryVisit>, BaseRepository<LinkEntryVisit>>();
            services.AddScoped<ILinkEntryService, LinkEntryService>();


            services.AddControllersWithViews();
            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, MiniLinkContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebAssemblyDebugging();
              
            }
            else
            {
                context.Database.Migrate();
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapFallbackToFile("index.html");
            });
        }
    }
}
