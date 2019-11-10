using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Enumerable = System.Linq.Enumerable;

namespace EsUnit.Tests
{

    public abstract class EsTest
    {
        private readonly Aggregate _ut;

        protected EsTest(Aggregate ut)
        {
            _ut = ut;
        }

        [Fact]
        public void TheTest()
        {
            var inputMessages = Given();
            var commandingMessage = When();
            var expectedEvents = Then();
            _ut.Load(inputMessages);
            var actualEvents = _ut.Handle(commandingMessage).ToList();
            Assert.Equal(expectedEvents.Count, actualEvents.Count);
            foreach (var @event in actualEvents)
            {
                Assert.Single(expectedEvents.Where(e => e.Id == @event.Id));
            }
        }

        protected abstract List<Event> Then();

        protected abstract IMessage When();

        protected abstract List<Event> Given();
    }


    public class TestTest : EsTest
    {
        private readonly List<Event> _events;

        public TestTest() : base(new TestAggregate())
        {
            _events = new List<Event>()
            {
                new TestSucceeded(Guid.NewGuid(), "causeId", "correlationId")
            };
        }

        protected override List<Event> Then()
        {
            return _events;
        }

        protected override IMessage When()
        {
            return new TestCommand(_events[0].Id, "causation", "correlation");
        }

        protected override List<Event> Given()
        {
            var newGuid = Guid.NewGuid();
            return new List<Event>()
            {
                new TestEvent(Guid.NewGuid(),"causation1" , "correlation")
            };
        }
    }

    internal class TestAggregate : Aggregate
    {
        public override IAggregate Apply(Event @event)
        {
            return this;
        }

        public override IEnumerable<Event> Handle(IMessage message)
        {
            return new List<Event>()
            {
                new TestSucceeded(message.Id, message.CausationId, message.CorrelationId)
            };
        }
    }

    internal class TestEvent : Event
    {
        public TestEvent(Guid id, string causationId, string correlationId) : base(id, causationId, correlationId)
        {
        }
    }

    internal class TestThingy
    {
        public object DoSomething(List<IMessage> messages)
        {
            throw new NotImplementedException();
        }
    }

    internal class TestSucceeded : Event
    {
        public TestSucceeded(Guid id, string causationId, string correlationId) : base(id, causationId, correlationId)
        {
        }
    }

    internal class TestCommand : Message
    {
        public TestCommand(Guid id, string causationId, string correlationId) : base(id, causationId, correlationId)
        {
        }
    }
}
