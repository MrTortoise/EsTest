using System;
using System.Collections.Generic;
using System.Linq;

namespace EsUnit.Tests
{
    public interface IAggregate
    {
        int Version { get; }
        Dictionary<int, IConvertAggregate> VersionConverters { get; }
        IAggregate Load(IEnumerable<Event> events);
        IAggregate Apply(Event @event);
        IEnumerable<Event> Handle(IMessage message);
    }

    public abstract class Aggregate : IAggregate
    {
        public int Version { get; } = 0;
        public Dictionary<int, IConvertAggregate> VersionConverters { get; } = new Dictionary<int, IConvertAggregate>();
        public IAggregate Load(IEnumerable<Event> events)
        {
            return events.Aggregate(this, (acc, current) => acc.Apply((dynamic)current));
        }

        public abstract IAggregate Apply(Event @event);
     
        public abstract IEnumerable<Event> Handle(IMessage message);

        public class UnhandledEventException : Exception
        {
            public UnhandledEventException(Event @event) : base($"Unhandled event in Aggregate: {@event.EventType}, {@event.CorrelationId}")
            {
            }
        }
    }


}