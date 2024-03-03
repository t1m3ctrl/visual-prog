using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Platform;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;


namespace hw5_file_explorer
{
    public class FileItem
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public Avalonia.Media.Imaging.Bitmap IconPath { get; set; }
        public string Type { get; set; }
    }
    internal class DataContextMainWindow : BaseModel
    {
        private string _currentDirectory;
        private ObservableCollection<FileItem> _collection;
        private bool _showDrives = true;
        private Avalonia.Media.Imaging.Bitmap _image;
        public DataContextMainWindow()
        {
            Collection = new ObservableCollection<FileItem>();
            foreach (string drive in Directory.GetLogicalDrives())
            {
                Collection.Add(new FileItem { Name = drive, Path = "Open" ,IconPath = new Avalonia.Media.Imaging.Bitmap("D:\\dev\\projects\\vuz\\visual-prog\\hw5_file_explorer\\hw5_file_explorer\\res\\disk.png"), Type = "drive" });
            }
            CurrentDirectory = "Drives";
            UpdateCollection();
        }
        public string CurrentDirectory
        {
            get => _currentDirectory;
            set => _ = SetField(ref _currentDirectory, value);
        }
        public ObservableCollection<FileItem> Collection
        {
            get => _collection;
            set => _ = SetField(ref _collection, value);
        }
        public Avalonia.Media.Imaging.Bitmap Image
        {
            get => _image;
            set => SetField(ref _image, value);
        }
        private void UpdateCollection()
        {
            Collection.Clear();

            if (!_showDrives)
            {
                Collection.Add(new FileItem { Name = "..", Path = "Back",IconPath = new Avalonia.Media.Imaging.Bitmap("D:\\dev\\projects\\vuz\\visual-prog\\hw5_file_explorer\\hw5_file_explorer\\res\\upload.png"), Type = "back" });

                try
                {
                    foreach (var directory in Directory.GetDirectories(CurrentDirectory))
                    {
                        Collection.Add(new FileItem { Name = Path.GetFileName(directory), Path = directory, IconPath = new Avalonia.Media.Imaging.Bitmap("D:\\dev\\projects\\vuz\\visual-prog\\hw5_file_explorer\\hw5_file_explorer\\res\\folder.png"), Type = "folder" });
                        Console.WriteLine($"{directory}");
                    }
                    foreach (var file in Directory.GetFiles(CurrentDirectory))
                    {
                        string extension = Path.GetExtension(file);
                        if (extension == ".jpg" || extension == ".jpeg" || extension == ".png" || extension == ".gif" || extension == ".bmp")
                            Collection.Add(new FileItem { Name = Path.GetFileName(file), Path = file, IconPath = new Avalonia.Media.Imaging.Bitmap("D:\\dev\\projects\\vuz\\visual-prog\\hw5_file_explorer\\hw5_file_explorer\\res\\document.png"), Type = "image" });
                        else
                            Collection.Add(new FileItem { Name = Path.GetFileName(file), Path = file, IconPath = new Avalonia.Media.Imaging.Bitmap("D:\\dev\\projects\\vuz\\visual-prog\\hw5_file_explorer\\hw5_file_explorer\\res\\document.png"), Type = "file" });
                    }
                }
                catch (UnauthorizedAccessException ex)
                {
                    Console.WriteLine("The application does not have permissions to open the directory or file: " + ex.Message);
                }

            }
            else
            {
                foreach (string drive in Directory.GetLogicalDrives())
                {
                    Console.WriteLine($"{drive}");
                    Collection.Add(new FileItem { Name = drive, Path = "Open", IconPath = new Avalonia.Media.Imaging.Bitmap("D:\\dev\\projects\\vuz\\visual-prog\\hw5_file_explorer\\hw5_file_explorer\\res\\disk.png"), Type = "drive" });
                }
            }
        }
        public void ItemDoubleClick(string selectedItem)
        {
            if (selectedItem == ".." && Directory.GetParent(CurrentDirectory) != null)
            {
                CurrentDirectory = Directory.GetParent(CurrentDirectory).FullName;
            }
            else if (selectedItem == ".." && Directory.GetParent(CurrentDirectory) == null)
            {
                _showDrives = true;
                CurrentDirectory = "Drives";
            }
            else if (Directory.Exists(Path.Combine(CurrentDirectory, selectedItem)))
            {
                CurrentDirectory = Path.Combine(CurrentDirectory, selectedItem);
                _showDrives = false;
            }
            UpdateCollection();
        }
        public void ImageView(string selectedItem)
        {
            Image = new Avalonia.Media.Imaging.Bitmap(Path.Combine(CurrentDirectory, selectedItem));
        }
    }
}
