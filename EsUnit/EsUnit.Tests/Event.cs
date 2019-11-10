using System;

namespace EsUnit.Tests
{
    public class Event : Message
    {
        public Event(Guid id, string causationId, string correlationId) : base(id, causationId, correlationId)
        {
        }
    }
}