using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hw5_file_explorer
{
    public class FileItem
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public Avalonia.Media.Imaging.Bitmap Icon { get; set; }
        public string Type { get; set; }
    }
}
