using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ReflectionAvalonia.Models.User
{
    public class Geo
    {
        [JsonPropertyName("lat")]
        public string Lat { get; set; }
        [JsonPropertyName("lng")]
        public string Lng { get; set; }

        public Geo(string lat, string lng)
        {
            Lat = lat;
            Lng = lng;
        }
    }
}
