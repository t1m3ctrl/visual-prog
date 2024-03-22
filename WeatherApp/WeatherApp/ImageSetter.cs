using Avalonia.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp
{
    internal class ImageSetter
    {
        static public Avalonia.Media.Imaging.Bitmap SetImage(string type)
        {
            Avalonia.Media.Imaging.Bitmap image = new Avalonia.Media.Imaging.Bitmap($"..\\..\\..\\assets\\{type}.png");
            return image;
        }
        static public Avalonia.Media.ImageBrush SetImageBrush(string type)
        {
            Avalonia.Media.Imaging.Bitmap image = SetImage(type);
            Avalonia.Media.ImageBrush imageBrush = new Avalonia.Media.ImageBrush {Source = image};
            return imageBrush;
        }
    }
}
