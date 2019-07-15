using Confluent.Kafka;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Common.Kafka.Serializers
{
    public class MessageDeserializer<T> : IDeserializer<T>
    {
        public T Deserialize(ReadOnlySpan<byte> data, bool isNull, SerializationContext context)
        {
            using (var memStream = new MemoryStream())
            {
                var binForm = new BinaryFormatter();
                memStream.Write(data.ToArray(), 0, data.Length);
                memStream.Seek(0, SeekOrigin.Begin);
                var obj = (T)binForm.Deserialize(memStream);
                return obj;
            }
        }
    }
}
