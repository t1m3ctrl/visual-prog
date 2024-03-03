using Avalonia.Controls;
using Avalonia.Input;
using FileExplorer;
using SkiaSharp;
using System;
using System.IO;

namespace hw4_file_explorer
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        public void DoubleTappedCommand(object? sender, TappedEventArgs e)
        {
            if (sender is ListBox listBox && listBox.DataContext is DataContextMainWindow dcMainWindow)
            {
                if (e.Source is Control control && control.DataContext is FileItem item)
                {
                    dcMainWindow.ItemDoubleClick(item.Name);
                }
            }
        }
    }
}