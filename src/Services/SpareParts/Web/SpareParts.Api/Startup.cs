using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using EventBus.Abstractions;
using EventBusRabbitMQ;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using Serilog;
using SpareParts.Application;
using SpareParts.Application.IntegrationEvents.Services;
using SpareParts.Persistence;
namespace SpareParts.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplication();
            services.AddPersistance(Configuration);
            services.AddControllers();
            services.AddIntegrationServices(Configuration);
            services.AddEventBus(Configuration);
            services.AddSwaggerGen();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "SpareParts API");
                    c.RoutePrefix = string.Empty;
                });
            }
            

            loggerFactory.CreateLogger<Startup>();

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors("CorsPolicy");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            ConfigureEventBus(app);
        }

        protected void ConfigureEventBus(IApplicationBuilder app)
        {
            var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();
            //TODO: subscribe to events
        }
    }

    public static class CustomExtentionMethods
    {
        public static IServiceCollection AddIntegrationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IRabbitMQPersistentConnection>(sp =>
            {
                var logger = sp.GetRequiredService<ILogger<RabbitMQPersistentConnection>>();
                var factory = new ConnectionFactory()
                {
                    HostName = configuration["EventBusConnection"],
                    UserName = configuration["EventBusUserName"],
                    Password = configuration["EventBusPassword"],
                    RequestedConnectionTimeout = TimeSpan.FromSeconds(3),
                    Port = int.Parse(configuration["EventBusPort"]),
                    DispatchConsumersAsync = true,
                };
                var retryCount = 5;
                if (!string.IsNullOrEmpty(configuration["EventBusRetryCount"]))
                {
                    retryCount = int.Parse(configuration["EventBusRetryCount"]);
                }

                return new RabbitMQPersistentConnection(factory, logger, retryCount);
            });

            return services;
        }

        public static IServiceCollection AddEventBus(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddSingleton<IEventBus, EventBusRabbitMQ.EventBusRabbitMQ>(sp =>
            {
                var subscriptionClientName = configuration["SubscriptionClientName"];
                var rabbitMQPersisentConnection = sp.GetRequiredService<IRabbitMQPersistentConnection>();
                var logger = sp.GetRequiredService<ILogger<EventBusRabbitMQ.EventBusRabbitMQ>>();
                var eventBusSubscriptionManager = sp.GetRequiredService<IEventBusSubscriptionManager>();
                var lifetimeScope = sp.GetRequiredService<ILifetimeScope>();

                var retryCount = 5;
                if (!string.IsNullOrEmpty(configuration["EventBusRetryCount"]))
                {
                    retryCount = int.Parse(configuration["EventBusRetryCount"]);
                }

                return new EventBusRabbitMQ.EventBusRabbitMQ(rabbitMQPersisentConnection, logger, eventBusSubscriptionManager, lifetimeScope, subscriptionClientName, retryCount);
            });
            
            services.AddSingleton<IEventBusSubscriptionManager, InMemoryEventBusSubscriptionManager>();
            services.AddTransient<ISparePartsIntegrationEventService, SparePartsIntegrationEventService>();
            //TOOD: ADD handlers

            return services;
        }
    }
}
