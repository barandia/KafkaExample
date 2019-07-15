using System;
using System.Threading.Tasks;
using Common.DTO;
using Common.Kafka.Serializers;
using Confluent.Kafka;


namespace Common.Kafka
{
    public class MessageProducer<MESSAGE_TYPE> : IMessageProducer<MESSAGE_TYPE> where MESSAGE_TYPE : IMessage
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        private static ProducerConfig Config;
        private static IProducer<Null, MESSAGE_TYPE> Producer;

        public MessageProducer(string bootstrapServers)
        {
            Config = new ProducerConfig { BootstrapServers = bootstrapServers };
            Producer = new ProducerBuilder<Null, MESSAGE_TYPE>(Config)
                                .SetValueSerializer(new MessageSerializer<MESSAGE_TYPE>()).Build();
        }

        public async Task ProduceMessageAsync(string topic, MESSAGE_TYPE messageToProduce)
        {
            try
            {
                var result = await Producer.ProduceAsync(topic, new Message<Null, MESSAGE_TYPE> { Value = messageToProduce });
                Logger.Debug($"Delivered '{result.Value}' to '{result.TopicPartitionOffset}'");
            }
            catch (ProduceException<Null, string> e)
            {
                Logger.Error($"Delivery failed: {e.Error.Reason}");
            }
        }

        public async Task ProduceMessageAsync(MESSAGE_TYPE messageToProduce)
        {
            await ProduceMessageAsync(Constants.DEFAULT_TOPIC, messageToProduce);
        }

        public void ProduceMessage(MESSAGE_TYPE messageToProduce)
        {
            try
            {
                void handler(DeliveryReport<Null, MESSAGE_TYPE> r) =>
                                    Console.WriteLine(!r.Error.IsError
                                        ? $"Delivered '{r.Value}' to '{r.TopicPartitionOffset}'"
                                        : $"Delivery failed: {r.Error.Reason}");

                Producer.Produce(Constants.DEFAULT_TOPIC, new Message<Null, MESSAGE_TYPE> { Value = messageToProduce }, handler);
            }
            catch (ProduceException<Null, string> e)
            {
                Logger.Error($"Delivery failed: {e.Error.Reason}");
            }
        }
    }
}
