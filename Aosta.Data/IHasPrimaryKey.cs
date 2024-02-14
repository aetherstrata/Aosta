namespace Aosta.Data.Database;

public interface IHasPrimaryKey<out T> where T : struct
{
    public T PrimaryKey { get; }
}
