namespace AlbedoTeam.Sdk.MessageConsumer.Configuration
{
    using Abstractions;

    internal class MessageBrokerOptions : IMessageBrokerOptions
    {
        public MessageBrokerOptions()
        {
            HostOptions = new HostOptions();
            KillSwitchOptions = new KillSwitchOptions();
        }

        public int PrefetchCount { get; set; } = 1;
        public IHostOptions HostOptions { get; set; }
        
        /// <summary>
        /// A Kill Switch monitors a receive endpoint and automatically stops and restarts the endpoint in the presence of consumer faults. The options
        /// can be configured to adjust the trip threshold, restart timeout, and exceptions that are observed by the kill switch. When configured on the bus,
        /// a kill switch is installed on every receive endpoint.
        /// </summary>
        public IKillSwitchOptions KillSwitchOptions { get; set; }
    }

    public class HostOptions : IHostOptions
    {
        public string Host { get; set; }
        public ushort HeartbeatInterval { get; set; } = 3;
        public ushort RequestedChannelMax { get; set; } = 12;
        public int RequestedConnectionTimeout { get; set; } = 60000;
    }

    public class KillSwitchOptions : IKillSwitchOptions
    {
        public int ActivationThreshold { get; set; } = 10;
        public double TripThreshold { get; set; } = 0.15;
        public int RestartTimeout { get; set; } = 60;
    }
}