using System;
using Newtonsoft.Json;

namespace EsUnit.Tests
{
    public class Message : IMessage
    {
        [JsonConstructor]
        public Message(Guid id, string causationId, string correlationId)
        {
            Id = id;
            CausationId = causationId;
            CorrelationId = correlationId;
        }

        public string CorrelationId { get; }
        public string CausationId { get; }
        public Guid Id { get; }
        public string EventType => this.GetType().FullName;
    }
}