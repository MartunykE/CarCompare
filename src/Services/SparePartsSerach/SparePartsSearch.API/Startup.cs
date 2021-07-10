using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using EventBus.Abstractions;
using EventBusRabbitMQ;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using Serilog;
using SparePartsSearch.API.IntegrationEventHandlers;
using SparePartsSearch.API.IntegrationEvents;
using SparePartsSearch.API.Services;
using SparePartsSearch.API.Services.Abstractions;

namespace SparePartsSearch.API
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
            //services.AddTransient<ISearchService, GoogleSearchService>();
            services.AddTransient<ISearchService, GoogleApiSearchService>(options =>
                new GoogleApiSearchService(Configuration.GetSection("GoogleSearchApiKey").Value, Configuration.GetSection("SearchEngineId").Value));
            services.AddControllers();
            services.AddIntegrationServices(Configuration);
            services.AddEventBus(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            loggerFactory.CreateLogger<Startup>();

            app.UseRouting();

            app.UseCors(x => x
               .AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader());

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            ConfigureServiceBus(app);
        }

        private void ConfigureServiceBus(IApplicationBuilder applicationBuilder)
        {
            var eventBus = applicationBuilder.ApplicationServices.GetRequiredService<IEventBus>();
            eventBus.Subscribe<TestIntegrationEvent, TestIntegrationEventHandler>();
            eventBus.Subscribe<AskForSparePartsPricesIntegrationEvent, AskForSparePartsPricesIntegrationEventHandler>();
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
                    RequestedConnectionTimeout = TimeSpan.FromSeconds(5),
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
            //services.AddTransient<ISparePartsIntegrationEventService, SparePartsIntegrationEventService>();
            //TOOD: ADD handlers
            services.AddTransient<AskForSparePartsPricesIntegrationEventHandler>();
            services.AddTransient<TestIntegrationEventHandler>();

            return services;
        }
    }
}
