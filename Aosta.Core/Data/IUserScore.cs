namespace Aosta.Core.Data;

public interface IUserScore
{
    public const int MaximumScore = 100;
    public const int MinimumScore = 0;

    /// <summary>The content score.</summary>
    /// <remarks>It must be between 0 and 100.</remarks>
    public int Score { get; set; }

    public string ToString() => $"Score: {Score}";
}