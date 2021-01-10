using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using RabbitMQExample.API.Contracts.Services;
using RabbitMQExample.API.EventBus;
using RabbitMQExample.API.Services;
using RabbitMQExample.DataAccess.Access;
using RabbitMQExample.DataAccess.Config;
using RabbitMQExample.DataAccess.Contracts;

namespace RabbitMQExample.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "RabbitMQExample.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "RabbitMQExample.API", Version = "v1" });
            });

            services.AddHostedService<EventBusListener>();

            services.AddScoped<IItemService, ItemService>();
            services.AddScoped<IEventPublisherService, EventPublisherService>();
            services.Configure<DbSettings>(Configuration.GetSection(nameof(DbSettings)));
            services.AddSingleton<IDbSettings>(x => x.GetRequiredService<IOptions<DbSettings>>().Value);
            services.AddSingleton(typeof(IDbClient<>), typeof(DbClient<>));
        }
    }
}