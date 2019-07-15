using Confluent.Kafka;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Common.Kafka.Serializers
{
    public class MessageSerializer<T> : ISerializer<T>
    {
        public byte[] Serialize(T data, SerializationContext context)
        {
            BinaryFormatter bf = new BinaryFormatter();
            using (var ms = new MemoryStream())
            {
                bf.Serialize(ms, data);
                return ms.ToArray();
            }
        }
    }
}
