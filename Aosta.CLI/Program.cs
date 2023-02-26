// See https://aka.ms/new-console-template for more information

using Aosta.Core;
using Aosta.Core.Data.Enums;
using Realms;
using Aosta.Core.Data.Models;
using Aosta.Core.Extensions;
using JikanDotNet;
using AiringStatus = Aosta.Core.Data.Enums.AiringStatus;
using ContentObject = Aosta.Core.Data.Models.ContentObject;

namespace Aosta.CLI;

internal static class Program
{
    private static readonly AostaDotNet Core = new(
        new RealmConfiguration(Path.Combine(AppContext.BaseDirectory, "aosta.realm"))
        {
            IsReadOnly = false,
            ShouldDeleteIfMigrationNeeded = true
        });

    public static async Task Main(string[] args)
    {
        await Core.UpdateMyAnimeListData(1);

        using var realm = Core.GetInstance();

        Console.WriteLine(realm.Find<JikanContentObject>(1).Episodes);
/*
        await realm.WriteAsync(() =>
        {
            realm.Add(anime);
        });

        Console.WriteLine(realm.First<ContentObject>().Title);
*/
        return;

        /*
        using (var r = Core.GetInstance())
        {
            Console.WriteLine(r.Config.DatabasePath);
        }

        Guid id = await Core.WriteContentAsync(new ContentObject());
        using var realm = Core.GetInstance();
        Console.WriteLine(realm.Find<ContentObject>(id).Title);
        Console.WriteLine(realm.All<ContentObject>().Count());
        */
    }
}