using System;

namespace EsUnit.Tests
{
    public interface IMessage
    {
        string CorrelationId { get; }
        string CausationId { get; }
        Guid Id { get; }
    }
}