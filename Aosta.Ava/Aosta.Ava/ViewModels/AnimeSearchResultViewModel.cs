// Copyright (c) Davide Pierotti <d.pierotti@live.it>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Linq;
using System.Reactive;
using System.Threading.Tasks;

using Aosta.Core;
using Aosta.Core.Database.Mapper;
using Aosta.Core.Database.Models;
using Aosta.Core.Database.Models.Jikan;
using Aosta.Jikan.Models.Response;

using ReactiveUI;

namespace Aosta.Ava.ViewModels;

public class AnimeSearchResultViewModel : ReactiveObject
{
    public AnimeSearchResultViewModel(AnimeResponse response, AostaDotNet aosta)
    {
        _missing = !aosta.Realm.Exists<JikanAnime>(response.MalId);

        Title = response.Titles.First(x => x.Type == "Default").Title;
        Banner = response.Images?.JPG?.ImageUrl;
        Score = response.Score;

        AddCommand = ReactiveCommand.CreateFromTask(() =>
        {
            if (!CanBeAdded)
            {
                aosta.Log.Debug("User tried adding already existing anime from Jikan: {Id}", response.MalId);
                return Task.CompletedTask;
            }

            var realmTask = aosta.Realm.WriteAsync(r =>
            {
                var jikanMetadata = response.ToJikanAnime();

                r.Add(jikanMetadata);
                r.Add(new Anime { Jikan = jikanMetadata });
            });

            CanBeAdded = false;

            aosta.Log.Information("Adding new anime from Jikan: {Id}", response.MalId);

            return realmTask;
        });
    }

    private bool _missing;
    public bool CanBeAdded
    {
        get => _missing;
        set => this.RaiseAndSetIfChanged(ref _missing, value);
    }

    public string? Title { get; }

    public string? Banner { get; }

    public double? Score { get; }

    public ReactiveCommand<Unit, Unit> AddCommand { get; }
}
