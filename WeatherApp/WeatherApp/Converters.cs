using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp
{
    internal class Converters
    {
        static public string DegToCompass(int num)
        {
            var val = (int)((num / 22.5) + 0.5);
            string[] arr = { "N", "NNE", "NE", "ENE", "E", "ESE", "SE", "SSE", "S", "SSW", "SW", "WSW", "W", "WNW", "NW", "NNW" };
            return arr[(val % 16)];
        }

        static public string UnixTimeToTime(long unix_time)
        {
            DateTimeOffset utcDateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(unix_time);
            return utcDateTimeOffset.ToString("HH:mm:ss");
        }

        static public string UnixTimeToDate(long unix_time)
        {
            DateTimeOffset utcDateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(unix_time);
            return utcDateTimeOffset.ToString("dd.MM");
        }

        static public string UnixTimeToDayOfTheWeek(long unix_time)
        {
            DateTimeOffset utcDateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(unix_time);
            DateTime dateTime = utcDateTimeOffset.DateTime;
            DayOfWeek dayOfWeek = dateTime.DayOfWeek;
            return dayOfWeek.ToString();
        }
    }
}
