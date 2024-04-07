using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tmds.DBus.Protocol;

namespace WeatherApp.model
{
    internal class WeatherData
    {
        public string CurrentTime { get; set; }
        public string City { get; set; }
        public string Temperature { get; set; }
        public string Description { get; set; }
        public Avalonia.Media.Imaging.Bitmap Icon { get; set; }

        // Details 
        public string FeelsLike { get; set; }
        public string Humidity { get; set; }
        public string WindSpeed { get; set; }  
        public string WindDirection {  get; set; }
        public string Pressure { get; set; }
        public string Sunrise { get; set; }
        public string Sunset { get; set;}
    }
}
