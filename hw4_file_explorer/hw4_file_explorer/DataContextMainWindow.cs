using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FileExplorer
{
    internal class DataContextMainWindow : BaseModel
    {
        private string _currentDirectory;
        private ObservableCollection<string> _collection;
        private bool _showDrives = true;
        public DataContextMainWindow()
        {
            Collection = new ObservableCollection<string>(Directory.GetLogicalDrives());
            CurrentDirectory = "";
            UpdateCollection();
        }
        public string CurrentDirectory
        {
            get => _currentDirectory;
            set => _ = SetField(ref _currentDirectory, value);
        }
        public ObservableCollection<string> Collection
        {
            get => _collection;
            set => _ = SetField(ref _collection, value);
        }
        private void UpdateCollection()
        {
            Collection.Clear();

            if (!_showDrives)
            {
                Collection.Add("..");

                try
                {   
                    foreach (var directory in Directory.GetDirectories(CurrentDirectory))
                    {
                        Collection.Add(Path.GetFileName(directory));
                    }
                    foreach (var file in Directory.GetFiles(CurrentDirectory))
                    {
                        Collection.Add(Path.GetFileName(file));
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
                    Collection.Add(drive);
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
                CurrentDirectory = "";
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
