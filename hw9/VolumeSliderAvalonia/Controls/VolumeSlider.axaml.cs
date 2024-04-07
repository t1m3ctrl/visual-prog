using Avalonia;
using Avalonia.Controls.Primitives;
using ReactiveUI;
using System.Diagnostics;
using System.Reactive;
using System.Windows.Input;

namespace VolumeSliderAvalonia;

public class VolumeSlider : TemplatedControl
{
    public static readonly StyledProperty<bool> IsSliderVisibleProperty =
            AvaloniaProperty.Register<VolumeSlider, bool>("IsSliderVisible");

    public bool IsSliderVisible
    {
        get => GetValue(IsSliderVisibleProperty);
        set
        {
            SetValue(IsSliderVisibleProperty, value);
        }
    }

    public ReactiveCommand<Unit, Unit> CloseButtonCommand { get; }

    private void CloseButtonAction()
    {
        IsSliderVisible = false;
    }

    public ReactiveCommand<Unit, Unit> OpenSliderCommand { get; }

    private void RectangleTappedAction()
    {
        IsSliderVisible = true;
    }

    public VolumeSlider()
    {
        CloseButtonCommand = ReactiveCommand.Create(CloseButtonAction);
        OpenSliderCommand = ReactiveCommand.Create(RectangleTappedAction);
    }
}