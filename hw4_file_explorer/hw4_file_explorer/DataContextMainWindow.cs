using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Platform;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FileExplorer
{
    public class FileItem
    {

        public string Name { get; set; }
        public Avalonia.Media.Imaging.Bitmap IconPath { get; set; }
    }
    internal class DataContextMainWindow : BaseModel 
    {
        private string _currentDirectory;
        private ObservableCollection<FileItem> _collection;
        private bool _showDrives = true;
        public DataContextMainWindow()
        {
            Collection = new ObservableCollection<FileItem>();
            foreach (string drive in Directory.GetLogicalDrives()) 
            { 
                Collection.Add(new FileItem { Name = drive, IconPath = new Avalonia.Media.Imaging.Bitmap("D:\\dev\\projects\\vuz\\visual-prog\\hw4_file_explorer\\hw4_file_explorer\\res\\disk.png")});
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
        private void UpdateCollection()
        {
            Collection.Clear();

            if (!_showDrives)
            {
                Collection.Add(new FileItem{ Name = "..", IconPath = new Avalonia.Media.Imaging.Bitmap("D:\\dev\\projects\\vuz\\visual-prog\\hw4_file_explorer\\hw4_file_explorer\\res\\upload.png") });

                try
                {   
                    foreach (var directory in Directory.GetDirectories(CurrentDirectory))
                    {
                        Collection.Add(new FileItem{ Name = Path.GetFileName(directory), IconPath = new Avalonia.Media.Imaging.Bitmap("D:\\dev\\projects\\vuz\\visual-prog\\hw4_file_explorer\\hw4_file_explorer\\res\\folder.png") });
                        Console.WriteLine($"{directory}");
                    }
                    foreach (var file in Directory.GetFiles(CurrentDirectory))
                    {
                        Collection.Add(new FileItem{ Name = Path.GetFileName(file), IconPath = new Avalonia.Media.Imaging.Bitmap("D:\\dev\\projects\\vuz\\visual-prog\\hw4_file_explorer\\hw4_file_explorer\\res\\document.png") });
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
                    Collection.Add(new FileItem { Name = drive, IconPath = new Avalonia.Media.Imaging.Bitmap("D:\\dev\\projects\\vuz\\visual-prog\\hw4_file_explorer\\hw4_file_explorer\\res\\disk.png") });
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
    }
}
