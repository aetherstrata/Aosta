// Copyright (c) Davide Pierotti <d.pierotti@live.it>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Linq;
using System.Reactive;
using System.Threading.Tasks;

using Aosta.Ava.Extensions;
using Aosta.Core;
using Aosta.Core.Database.Mapper;
using Aosta.Jikan.Models.Response;

using ReactiveUI;

using Splat;

namespace Aosta.Ava.ViewModels;

public class AnimeSearchResultViewModel : ReactiveObject
{
    private readonly AostaDotNet _aosta = Locator.Current.GetSafely<AostaDotNet>();

    public AnimeSearchResultViewModel(AnimeResponse response, bool found)
    {
        _missing = !found;

        Title = response.Titles.First(x => x.Type == "Default").Title;
        Banner = response.Images?.JPG?.ImageUrl;
        Score = response.Score;

        AddCommand = ReactiveCommand.CreateFromTask(() =>
        {
            if (!CanBeAdded)
            {
                _aosta.Log.Debug("User tried adding already existing anime from Jikan: {Id}", response.MalId);
                return Task.CompletedTask;
            }

            var realmTask = _aosta.Realm.WriteAsync(r =>
            {
                var jikanMetadata = response.ToModel();

                r.Add(jikanMetadata.ToRealmModel());
            });

            CanBeAdded = false;

            _aosta.Log.Information("Adding new anime from Jikan: {Id}", response.MalId);

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
