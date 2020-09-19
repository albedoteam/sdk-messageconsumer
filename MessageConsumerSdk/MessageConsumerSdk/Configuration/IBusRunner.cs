using System.Threading;
using System.Threading.Tasks;

namespace MessageConsumerSdk.Configuration
{
    public interface IBusRunner
    {
        Task StartAsync(CancellationToken cancellationToken);
        Task StopAsync(CancellationToken cancellationToken);
        string Who { get; }
    }
}