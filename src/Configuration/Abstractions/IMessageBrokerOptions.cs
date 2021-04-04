namespace AlbedoTeam.Sdk.MessageConsumer.Configuration.Abstractions
{
    public interface IMessageBrokerOptions
    {
        /// <summary>
        ///     Specify the number of messages to prefetch from the message broker
        /// </summary>
        int PrefetchCount { get; set; }

        IHostOptions HostOptions { get; set; }

        IKillSwitchOptions KillSwitchOptions { get; set; }
    }

    public interface IHostOptions
    {
        /// <summary>
        ///     The host name of the broker, or a well-formed URI host address
        /// </summary>
        string Host { get; set; }

        /// <summary>
        ///     Specifies the heartbeat interval, in seconds, used to maintain the connection to RabbitMQ.
        ///     Setting this value to zero will disable heartbeats, allowing the connection to timeout
        ///     after an inactivity period.
        /// </summary>
        ushort HeartbeatInterval { get; set; }

        /// <summary>
        ///     Set the maximum number of channels allowed for the connection
        /// </summary>
        ushort RequestedChannelMax { get; set; }

        /// <summary>
        ///     The requested connection timeout, in milliseconds
        /// </summary>
        int RequestedConnectionTimeout { get; set; }
    }

    public interface IKillSwitchOptions
    {
        /// <summary>
        ///     The number of messages that must be consumed before the kill switch activates.
        /// </summary>
        int ActivationThreshold { get; set; }

        /// <summary>
        ///     The percentage of failed messages that triggers the kill switch. Should be 0-100, but seriously like 5-10.
        /// </summary>
        double TripThreshold { get; set; }

        /// <summary>
        ///     The wait time before restarting the receive endpoint, in seconds
        /// </summary>
        int RestartTimeout { get; set; }
    }
}