using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;
using System;
using Tmds.DBus.Protocol;

namespace hw2;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }
    public void ClickHandler(object sender, RoutedEventArgs args)
    {
        if (sender is Button btn)
        {
            if (Color.TryParse(btn.Content?.ToString(), out var color))
            {
                rectangle.Fill = new SolidColorBrush(color);
            }
        }
    }
}