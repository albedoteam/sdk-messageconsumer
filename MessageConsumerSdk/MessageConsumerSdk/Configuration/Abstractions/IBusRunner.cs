using System.Threading;
using System.Threading.Tasks;

namespace MessageConsumerSdk.Configuration.Abstractions
{
    public interface IBusRunner
    {
        string Who { get; }
        Task StartAsync(CancellationToken cancellationToken);
        Task StopAsync(CancellationToken cancellationToken);
    }
}