namespace AlbedoTeam.Sdk.MessageConsumer.Configuration
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Abstractions;
    using MassTransit;
    using Microsoft.Extensions.Logging;

    public class BusRunner : IBusRunner
    {
        private readonly IBusControl _bus;
        private readonly ILogger<BusRunner> _logger;

        public BusRunner(IBusControl bus, ILogger<BusRunner> logger)
        {
            _bus = bus;
            _logger = logger;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await _bus.StartAsync(cancellationToken);
            _logger.LogInformation("Bus is started at {Address} running at: {Time}", _bus.Address.Authority,
                DateTimeOffset.Now);
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await _bus.StopAsync(cancellationToken);
            _logger.LogInformation("Bus is stopped at {Time}", DateTimeOffset.Now);
        }

        public string Who => $"Bus is running at {_bus.Address.Authority} running at: {DateTimeOffset.Now}";
    }
}