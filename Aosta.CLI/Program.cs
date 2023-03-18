// See https://aka.ms/new-console-template for more information

using Aosta.Core;
using Aosta.Core.Data.Models;
using Realms;

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
        await Core.CreateJikanContentAsync(1);

        /*
        using var realm = Core.GetInstance();

        await realm.WriteAsync(() =>
        {
            foreach (var content in realm.All<ContentObject>())
            {
                content.Title = "Sugo";
            }
        });

        realm.Dispose();
        */

        await Core.UpdateJikanContentAsync(1, true);

        /*
        await realm.WriteAsync(() =>
        {
            realm.Add(anime);
        });

        Console.WriteLine(realm.First<ContentObject>().Title);
*/

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