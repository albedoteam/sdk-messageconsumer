﻿using System;
using GreenPipes;
using MassTransit;
using MessageConsumerSdk.Configuration;
using MessageConsumerSdk.Configuration.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace MessageConsumerSdk
{
    public static class ConsumerExtensions
    {
        public static IServiceCollection AddMessageBrokerConsumer(
            this IServiceCollection services,
            IConfiguration configuration,
            Action<IConsumerRegistration> consumers,
            Action<IDestinationQueueMapper> queues = null)
        {
            services.Configure<ConsumerOptions>(configuration.GetSection(nameof(ConsumerOptions)));
            services.AddSingleton<IConsumerOptions>(provider =>
                provider.GetRequiredService<IOptions<ConsumerOptions>>().Value);

            services.AddTransient<IBusRunner, BusRunner>();

            var sp = services.BuildServiceProvider();
            var options = sp.GetService<IConsumerOptions>();

            if (options == null)
                throw new ArgumentNullException(nameof(options));

            if (string.IsNullOrWhiteSpace(options.BrokerHost))
                throw new InvalidOperationException("Can not start the service without a valid Message Broker Host");

            services.AddMassTransit(x =>
            {
                x.SetKebabCaseEndpointNameFormatter();
                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.UseMessageRetry(r =>
                    {
                        r.Intervals(3000, 15000, 30000);
                        r.Ignore<ArgumentNullException>();
                    });

                    cfg.Host(options.BrokerHost);
                    cfg.ConfigureEndpoints(context);
                });

                services.AddSingleton(x);
                services.AddScoped<IConsumerRegistration, ConsumerRegistration>();
                services.AddScoped<IDestinationQueueMapper, DestinationQueueMapper>();

                var provider = services.BuildServiceProvider();

                var cr = provider.GetService<IConsumerRegistration>();
                consumers.Invoke(cr);

                var qm = provider.GetService<IDestinationQueueMapper>();
                queues?.Invoke(qm);
            });

            return services;
        }
    }
}