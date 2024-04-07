using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.model
{
    internal class WeatherDataCompressed
    {
        public string Date { get; set; }
        public string DayOfTheWeek { get; set; }
        public string Description { get; set; }
        public string TemperatureDay { get; set; }
        public Avalonia.Media.Imaging.Bitmap IconDay { get; set; }
        public string TemperatureNight { get; set; }
        public Avalonia.Media.Imaging.Bitmap IconNight { get; set; }
    }
}
