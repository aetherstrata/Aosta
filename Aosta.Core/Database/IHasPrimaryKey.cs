namespace Aosta.Core.Database;

public interface IHasPrimaryKey<out T> where T : struct
{
    public T ID { get; }
}
