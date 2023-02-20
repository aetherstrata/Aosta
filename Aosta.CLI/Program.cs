// See https://aka.ms/new-console-template for more information

using Aosta.Core;
using Aosta.Core.Data.Realm;
using JikanDotNet;
using Realms;

namespace Aosta.CLI;

internal static class Program
{
    private static readonly AostaDotNet Core = new(new RealmConfiguration(AppContext.BaseDirectory)
    {
        SchemaVersion = 2,
        IsReadOnly = false,
        ShouldDeleteIfMigrationNeeded = true
    });

    public static async Task Main(string[] args)
    {
        Guid id = await Core.WriteAnimeAndEpisodesAsync(22);
        var recommend = new Jikan().GetUserRecommendationsAsync("aetherstrata").Result;
        var realm = Core.GetInstance();
        Console.WriteLine(realm.Find<AnimeObject>(id).Title);
        realm.Dispose();
    }
}