using Common.DTO;
using System.Threading.Tasks;

namespace Common.Kafka
{
    public interface IMessageProducer<MESSAGE_TYPE> where MESSAGE_TYPE : IMessage
    {
        Task ProduceMessageAsync(string topic, MESSAGE_TYPE messageToProduce);

        Task ProduceMessageAsync(MESSAGE_TYPE messageToProduce);

        void ProduceMessage(MESSAGE_TYPE messageToProduce);
    }
}
