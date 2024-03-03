using Avalonia.Controls;
using Avalonia.Input;

namespace hw5_file_explorer
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
        public void TappedCommand(object? sender, TappedEventArgs e)
        {
            if (sender is ListBox listBox && listBox.SelectedItem is FileItem fileItem && fileItem.Type == "image")
            {
                dcMainWindow.ImageView(fileItem.Name);
            }
        }
        public void DoubleTappedCommand(object? sender, TappedEventArgs e)
        {
            if (sender is ListBox listBox && listBox.SelectedItem is FileItem fileitem) 
            {
                dcMainWindow.ItemDoubleClick(fileitem.Name);
            }
        }
    }
}