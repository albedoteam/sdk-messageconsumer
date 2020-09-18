using System.Collections.Generic;

namespace MessageConsumerSdk.Configuration
{
    public interface IConsumerOptions
    {
        public string BrokerHost { get; set; }
        public List<string> Queues { get; set; }
    }
}