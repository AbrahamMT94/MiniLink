using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MiniLinkLogic.Libraries.MiniLink.Core.Domain;
using MiniLinkLogic.Libraries.MiniLink.Data;
using MiniLinkLogic.Libraries.MiniLink.Data.Context;
using MiniLinkLogic.Libraries.MiniLink.Services;
using System;
using VisitProcessor.VisitProcessingService;

namespace MiniLink.VisitProcessor
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
            

            // sets up dbcontext, could probably use context pooling in this scenario
            services.AddDbContext<MiniLinkContext>(options =>
               options.UseSqlServer( Configuration.GetConnectionString("DefaultConnection")));

            // added repositories
            services.AddScoped<IBaseRepository<LinkEntry>, BaseRepository<LinkEntry>>();
            services.AddScoped<IBaseRepository<LinkEntryVisit>, BaseRepository<LinkEntryVisit>>();

            // added link entry service
            services.AddScoped<ILinkEntryService, LinkEntryService>();

            services.AddMassTransit(x =>
            {
                x.AddConsumer<VisitProcessorBackgroundService>();
                x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
                {                 
                    cfg.Host(new Uri("rabbitmq://rabbitmq:5672"), h =>
                    {
                        h.Username("guest");
                        h.Password("guest");
                    });
                    cfg.ReceiveEndpoint("visitQueue", ep =>
                    {
                        ep.PrefetchCount = 16;
                    
                        ep.ConfigureConsumer<VisitProcessorBackgroundService>(provider);
                    });
                }));
            });

            services.AddMassTransitHostedService();
        }
        public void Configure(IApplicationBuilder app)
        {
            app.UseRouting();          
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.

    }
}
