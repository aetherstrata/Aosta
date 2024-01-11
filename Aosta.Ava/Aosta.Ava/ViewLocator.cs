using System;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using ReactiveUI;
using Splat;

namespace Aosta.Ava;

public class ViewLocator : IDataTemplate, IViewLocator
{
    public IViewFor? ResolveView<T>(T? viewModel, string? contract = null)
    {
        string? viewModelName = viewModel?.GetType().FullName;
        string viewTypeName = viewModelName.TrimEnd("Model".ToCharArray());

        try
        {
            var viewType = Type.GetType(viewTypeName);

            if (viewType != null) return Activator.CreateInstance(viewType) as IViewFor;

            this.Log().Error($"Could not find the view {viewTypeName} for view model {viewModelName}.");
            throw new InvalidOperationException($"Could not find the view {viewTypeName} for view model {viewModelName}.");
        }
        catch
        {
            this.Log().Error($"Could not instantiate view {viewTypeName}.");
            throw;
        }
    }

    public Control? Build(object? data)
    {
        if (data is null) return null;

        string name = data.GetType().FullName!.Replace("ViewModel", "View", StringComparison.Ordinal);
        var type = Type.GetType(name);

        if (type != null)
        {
            return (Control)Activator.CreateInstance(type)!;
        }

        return new TextBlock { Text = "Not Found: " + name };
    }

    public bool Match(object? data)
    {
        return data is ReactiveObject;
    }
}
