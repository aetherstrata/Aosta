// Copyright (c) Davide Pierotti <d.pierotti@live.it>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Threading;

using Aosta.Ava.Extensions;

using Avalonia;
using Avalonia.Animation.Easings;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;
using Avalonia.Threading;

using Splat;

using ILogger = Serilog.ILogger;

namespace Aosta.Ava.Controls;

// ReSharper disable InconsistentNaming
public class AnimatedPopup : ContentControl
{
    public AnimatedPopup()
    {
        _origOpacity = Opacity;

        Opacity = 0;

        _sizingTimer = new Timer(state =>
        {
            if (_foundSizing) return;

            _foundSizing = true;

            Dispatcher.UIThread.InvokeAsync(() =>
            {
                _desiredSize = DesiredSize - Margin;

                updateAnimation();
            });
        });

        _animationTimer = new DispatcherTimer
        {
            Interval = TimeSpan.FromSeconds(1.0 / 60),
        };

        _animationTimer.Tick += animationTick;

        _underlay = new Border
        {
            IsVisible = false,
            VerticalAlignment = VerticalAlignment.Stretch,
            HorizontalAlignment = HorizontalAlignment.Stretch,
            Background = Brushes.Transparent,
            CornerRadius = new CornerRadius(0),
            BorderThickness = new Thickness(0),
            ZIndex = 10,
        };

        _underlay.Tapped += (_, _) => Close();
    }

    #region Private Members

    private readonly ILogger _log = Locator.Current.GetSafely<ILogger>();

    private readonly SineEaseInOut _easing = new();
    private readonly DispatcherTimer _animationTimer;
    private readonly double _origOpacity;
    private readonly Timer _sizingTimer;
    private readonly Control _underlay;

    private int _currentTick;
    private Size _desiredSize;
    private bool _firstAnimation = true;
    private bool _foundSizing;
    private int _totalTicks;

    #endregion

    #region Control Properties

    private TimeSpan _animationTime = TimeSpan.FromSeconds(0.3);

    public static readonly DirectProperty<AnimatedPopup, TimeSpan> AnimationTimeProperty =
        AvaloniaProperty.RegisterDirect<AnimatedPopup, TimeSpan>(
            nameof(AnimationTime), o => o.AnimationTime, (o, v) => o.AnimationTime = v);

    public TimeSpan AnimationTime
    {
        get => _animationTime;
        set => SetAndRaise(AnimationTimeProperty, ref _animationTime, value);
    }

    private bool _isOpen;

    public static readonly DirectProperty<AnimatedPopup, bool> IsOpenProperty =
        AvaloniaProperty.RegisterDirect<AnimatedPopup, bool>(
            nameof(IsOpen), o => o.IsOpen, (o, v) => o.IsOpen = v);

    public bool IsOpen
    {
        get => _isOpen;
        set
        {
            if (_isOpen == value) return;

            updateAnimation();

            SetAndRaise(IsOpenProperty, ref _isOpen, value);
        }
    }

    private bool _animateWidth = true;

    public static readonly DirectProperty<AnimatedPopup, bool> AnimateWidthProperty =
        AvaloniaProperty.RegisterDirect<AnimatedPopup, bool>(
            nameof(AnimateWidth), o => o.AnimateWidth, (o, v) => o.AnimateWidth = v);

    public bool AnimateWidth
    {
        get => _animateWidth;
        set => SetAndRaise(AnimateWidthProperty, ref _animateWidth, value);
    }

    private bool _animateHeight = true;

    public static readonly DirectProperty<AnimatedPopup, bool> AnimateHeightProperty =
        AvaloniaProperty.RegisterDirect<AnimatedPopup, bool>(
            nameof(AnimateHeight), o => o.AnimateHeight, (o, v) => o.AnimateHeight = v);

    public bool AnimateHeight
    {
        get => _animateHeight;
        set => SetAndRaise(AnimateHeightProperty, ref _animateHeight, value);
    }

    #endregion

    public void Open() => IsOpen = true;

    public void Close() => IsOpen = false;

    public override void Render(DrawingContext context)
    {
        if (!_foundSizing)
        {
            _sizingTimer.Change(50, int.MaxValue);
        }

        base.Render(context);
    }

    private void animationTick(object? sender, EventArgs args)
    {
        if (_firstAnimation)
        {
            _firstAnimation = false;

            completeAnimation();

            Opacity = _origOpacity;

            return;
        }

        if (_isOpen && _currentTick > _totalTicks ||
            !_isOpen && _currentTick == 0)
        {
            _animationTimer.Stop();

            completeAnimation();

            return;
        }

        _currentTick += _isOpen ? 1 : -1;

        _log.Verbose("Animation tick: {Tick}", _currentTick);

        double progress = (double)_currentTick / _totalTicks;

        if (_animateWidth) Width = _desiredSize.Width * _easing.Ease(progress);

        if (_animateHeight) Height = _desiredSize.Height * _easing.Ease(progress);
    }

    private void updateAnimation()
    {
        if (!_foundSizing) return;

        _totalTicks = (int)(_animationTime / _animationTimer.Interval);
        _animationTimer.Start();
    }

    private void completeAnimation()
    {
        if (_isOpen)
        {
            Width = double.NaN;
            Height = double.NaN;

            if (Parent is Grid g)
            {
                insertIntoGrid(_underlay, g);
            }
        }
        else
        {
            if (_animateHeight) Height = 0;

            if (_animateWidth) Width = 0;

            if (Parent is Grid g && g.Children.Contains(_underlay))
            {
                g.Children.Remove(_underlay);
            }
        }
    }

    private static void insertIntoGrid(Control control, Grid g)
    {
        control.IsVisible = true;

        if (g.RowDefinitions.Count > 0)
        {
            control.SetValue(Grid.RowSpanProperty, g.RowDefinitions.Count);
        }

        if (g.ColumnDefinitions.Count > 0)
        {
            control.SetValue(Grid.ColumnSpanProperty, g.ColumnDefinitions.Count);
        }

        g.Children.Insert(0, control);
    }
}
