using Avalonia.Controls;
using Avalonia.Input;
using System.Diagnostics.Tracing;

namespace WeatherApp
{
    public partial class MainWindow : Window
    {
        private readonly DataContextMainWindow dcMainWindow;
        public MainWindow()
        {
            InitializeComponent();
            dcMainWindow = new DataContextMainWindow();
            DataContext = dcMainWindow;
        }

        public void TextBoxUpdate(object? sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (sender is TextBox textBox)
                {
                    dcMainWindow.UpdateForecast(textBox.Text);
                    FocusManager.ClearFocus();
                    textBox.Text = "Enter city..";
                }
            }
        }

        public void TextBoxClear(object? sender, GotFocusEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                textBox.Clear();
            }
        }
    }
}