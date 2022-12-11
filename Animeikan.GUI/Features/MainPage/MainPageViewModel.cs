using CommunityToolkit.Mvvm.ComponentModel;

using System.Windows.Input;

namespace Animeikan.GUI.ViewModels;

[ObservableObject]
public partial class MainPageViewModel
{
  public ICommand AddAnimeCommand => new Command(async () =>
  {
    await AppShell.Current.GoToAsync(nameof(Views.AddAnimePage));
  });

  public ICommand GoToCommand => new Command<Type>(
    async (Type pageType) =>
    {
      await AppShell.Current.GoToAsync($"{pageType.Name}");
    });
}
