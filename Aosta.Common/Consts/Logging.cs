namespace Aosta.Common.Consts;

public static class Logging
{
    ///<summary> Output template </summary>
    public const string OUTPUT_TEMPLATE =
        "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u}] {Message:lj} <{ThreadId}><{ThreadName}>{NewLine}{Exception}";
}
