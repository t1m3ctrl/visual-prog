using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp
{
    internal class Background : BaseModel
    {
        private Avalonia.Media.Imaging.Bitmap _backgroundDay = ImageSetter.SetImage("light");
        private Avalonia.Media.Imaging.Bitmap _backgroundNight = ImageSetter.SetImage("dark");
        private Avalonia.Media.Imaging.Bitmap _backgroundLoad = ImageSetter.SetImage("bgload");
        private Avalonia.Media.Imaging.Bitmap _backgroundCurrent;

        public Avalonia.Media.Imaging.Bitmap BackgroundLoad
        {
            get => _backgroundLoad;
            set => _ = SetField(ref _backgroundLoad, value);
        }

        public Avalonia.Media.Imaging.Bitmap BackgroundDay
        {
            get => _backgroundDay;
            set => _ = SetField(ref _backgroundDay, value);
        }

        public Avalonia.Media.Imaging.Bitmap BackgroundNight
        {
            get => _backgroundNight;
            set => _ = SetField(ref _backgroundNight, value);
        }

        public Avalonia.Media.Imaging.Bitmap BackgroundCurrent
        {
            get => _backgroundCurrent;
            set => _ = SetField(ref _backgroundCurrent, value);
        }


        public void SetBackground(long unix_time, long sunrise, long sunset)
        {
            if (unix_time < sunset && unix_time > sunrise)
            {
                //Debug.WriteLine("SET BG DAY");
                BackgroundCurrent = BackgroundDay;
            }
            else 
            {
                //Debug.WriteLine("SET BG NIGHT");
                BackgroundCurrent = BackgroundNight;   
            }
        }
    }
}
