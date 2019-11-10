namespace EsUnit.Tests
{
    public interface IConvertAggregate
    {
        IAggregate Apply(IAggregate previousVersion);
    }
}