// See https://aka.ms/new-console-template for more information

using Aosta.Core;
using Aosta.Core.Data;

namespace Aosta.CLI;

class Program
{
    internal static AostaDotNet Core = new()
    {
        DatabaseConfiguration = new()
        {
            Location = AppContext.BaseDirectory,
            Configuration = new(AppContext.BaseDirectory)
            {
                SchemaVersion = 2,
                IsReadOnly = false,
                ShouldDeleteIfMigrationNeeded = true
            }
        }
    };

    public static async Task Main(string[] args)
    {
        Guid id = await Core.WriteOnlineAnimeAsync(100);
        var realm = Core.GetInstance();
        Console.WriteLine(realm.Find<ContentDTO>(id).Title);
        realm.Dispose();
    }
}