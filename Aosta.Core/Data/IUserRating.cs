using System.Text.Json.Serialization;
using Realms;

namespace Aosta.Core.Data;

public interface IUserRating : IUserScore
{
    /// <summary>The content review.</summary>
    public string Review { get; set; }

    public new string ToString() => $"Score: {Score}\nRating: {Review}";
}