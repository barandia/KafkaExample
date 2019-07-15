using Common;
using Common.DTO;
using Common.Kafka;

namespace Producer
{
    public class WriteRequestMessageProducer
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        private static readonly object padlock = new object();
        private static IMessageProducer<WriteRequestMessage> instance = null;

        public static IMessageProducer<WriteRequestMessage> GetInstance(string bootstrapServers)
        {
            if (instance == null)
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new MessageProducer<WriteRequestMessage>(bootstrapServers);
                    }
                }
            }

            return instance;
        }

        public static IMessageProducer<WriteRequestMessage> GetInstance()
        {
            if (instance == null)
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new MessageProducer<WriteRequestMessage>(Constants.DEFAULT_BOOTSTRAPSERVERS);
                    }
                }
            }

            return instance;
        }
    }
}
