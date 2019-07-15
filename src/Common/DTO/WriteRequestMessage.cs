using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.DTO
{
    [Serializable]
    public class WriteRequestMessage : IMessage
    {
        public enum WriterTypes
        {
            Csv,
            Excel,
            Txt
        }

        public string Id { get; set; }
        public string ScanId { get; set; }
        public string FilePathDest { get; set; }
        public string SourceDirectory { get; set; } 
        public bool Done { get; set; }
        public WriterTypes WriterType { get; set; }
        public String Payload { get; set; }

        public WriteRequestMessage()
        {
            Id = Guid.NewGuid().ToString();
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
