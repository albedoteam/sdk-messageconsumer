namespace AlbedoTeam.Sdk.MessageConsumer.Configuration.Abstractions
{
    using System.Threading;
    using System.Threading.Tasks;

    public interface IBusRunner
    {
        string Who { get; }
        Task StartAsync(CancellationToken cancellationToken);
        Task StopAsync(CancellationToken cancellationToken);
    }
}