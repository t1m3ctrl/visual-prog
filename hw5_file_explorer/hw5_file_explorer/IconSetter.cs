using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hw5_file_explorer
{
    public class IconSetter
    {
        private Avalonia.Media.Imaging.Bitmap? Icon;
        public Avalonia.Media.Imaging.Bitmap SetIcon(string type)
        {
            Icon = new Avalonia.Media.Imaging.Bitmap($"..\\..\\..\\res\\{type}.png" );
            return Icon;
        }
    }
}
