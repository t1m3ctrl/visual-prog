using Avalonia.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Avalonia.Controls.Documents;
using WeatherApp.services;
using WeatherApp.model;
using static System.Net.WebRequestMethods;
using static System.Net.Mime.MediaTypeNames;

namespace WeatherApp
{
    internal class DataContextMainWindow : BaseModel
    {
        private const string ApiKey = "cdff8ed78ba2088ce15329baa3136760";

        private WeatherData _weatherData;
        private GeoData _geoData;
        private ObservableCollection<WeatherDataCompressed> _weatherDataList;
        private Background _bg;

        public Background BG
        {
            get => _bg;
            set => _ = SetField(ref _bg, value);
        }
        public WeatherData WeatherData
        {
            get => _weatherData;
            set => SetField(ref _weatherData, value);
        }
        public GeoData GeoData
        {
            get => _geoData;
            set => SetField(ref _geoData, value);
        }
        public ObservableCollection<WeatherDataCompressed> WeatherDataList
        {
            get => _weatherDataList;
            set => SetField(ref _weatherDataList, value);
        }
        public DataContextMainWindow()
        {
            WeatherData = new WeatherData {City = "Novosibirsk"};
            GeoData = new GeoData();
            WeatherDataList = new ObservableCollection<WeatherDataCompressed>();
            BG = new Background();

            GetForecast(WeatherData.City);

            AutoUpdate(10800000); // 3 часа
            //AutoUpdate(600000); // 10 минут
        }

        private async Task GetWeatherAsync(double lat, double lon)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string response = await client.GetStringAsync($"https://api.openweathermap.org/data/2.5/weather?lat={lat}&lon={lon}&appid={ApiKey}&units=metric");
                    JObject json = JObject.Parse(response);

                    long TZoffset = (long)json["timezone"];

                    BG.SetBackground((long)json["dt"] + TZoffset, (long)json["sys"]["sunrise"] + TZoffset, (long)json["sys"]["sunset"] + TZoffset);

                    WeatherData = new WeatherData
                    {
                        // Main Forecast

                        CurrentTime = Converters.UnixTimeToTime((long)json["dt"] + TZoffset),
                        City = json["name"].ToString(),
                        Temperature = Math.Round((decimal)json["main"]["temp"]).ToString() + " °C",
                        Description = json["weather"][0]["description"].ToString(),
                        Icon = ImageSetter.SetImage(json["weather"][0]["icon"].ToString()),

                        // Details Sunrise

                        Sunrise = Converters.UnixTimeToTime((long)json["sys"]["sunrise"] + TZoffset),
                        Sunset = Converters.UnixTimeToTime((long)json["sys"]["sunset"] + TZoffset),
                        
                        // Details Wind

                        WindDirection = Converters.DegToCompass((int)json["wind"]["deg"]),
                        WindSpeed = json["wind"]["speed"].ToString() + " km/h",
                        Humidity = json["main"]["humidity"].ToString() + "%",
                        FeelsLike = Math.Round((decimal)json["main"]["feels_like"]).ToString() + " °C",
                        Pressure = json["main"]["pressure"].ToString() + " mb",
                    };
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Ошибка при получении погоды: {ex.Message}");
            }
        }
        private async Task GetGeoAsync(string city)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string response = await client.GetStringAsync($"https://api.openweathermap.org/geo/1.0/direct?q={city}&limit=5&appid={ApiKey}");
                    JArray jsonArray = JArray.Parse(response);

                    GeoData = new GeoData
                    {
                        City = jsonArray[0]["name"].ToString(),
                        lat = (double)jsonArray[0]["lat"],
                        lon = (double)jsonArray[0]["lon"],
                    };
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Ошибка при получении геолокации: {ex.Message}");
            }

        }
        private async Task GetWeather5Async(double lat, double lon)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string response = await client.GetStringAsync($"https://api.openweathermap.org/data/2.5/forecast?lat={lat}&lon={lon}&appid={ApiKey}&units=metric");
                    JObject json = JObject.Parse(response);
                    long TZoffset = (long)json["city"]["timezone"];
                    Debug.WriteLine($"TZ OFFSET = {TZoffset}");

                    WeatherDataList.Clear();

                    for (int i = 0; i < 5; i++)
                    {
                        int current_day = i*8;
                        WeatherDataList.Add(
                            new WeatherDataCompressed {
                                Date = Converters.UnixTimeToDate((long)json["list"][current_day]["dt"] + TZoffset),
                                DayOfTheWeek = Converters.UnixTimeToDayOfTheWeek((long)json["list"][current_day]["dt"] + TZoffset),
                                Description = json["list"][current_day]["weather"][0]["main"].ToString(),
                                IconNight = ImageSetter.SetImage(json["list"][current_day + 1]["weather"][0]["icon"].ToString()),
                                TemperatureNight = Math.Round((decimal)json["list"][current_day + 1]["main"]["temp"]).ToString() + " °C",
                                IconDay = ImageSetter.SetImage(json["list"][current_day + 5]["weather"][0]["icon"].ToString()),
                                TemperatureDay = Math.Round((decimal)json["list"][current_day + 5]["main"]["temp"]).ToString() + " °C",
                            }
                        );
                    }

                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Ошибка при получении погоды на 5 дней: {ex.Message}");
            }
        }

        private void AutoUpdate(int millisec)
        {
            Task task = null;
            task = new Task(() =>
            {
                while (true)
                {
                    task.Wait(millisec);
                    GetForecast(WeatherData.City);
                }
            });
            task.Start();
        }
        public async Task GetForecast(string city)
        {
            await GetGeoAsync(city);
            await GetWeatherAsync(GeoData.lat, GeoData.lon);
            await GetWeather5Async(GeoData.lat, GeoData.lon);
        }
        public async Task UpdateForecast(string text) 
        {
            await GetForecast(text);
        }
        public async Task ReloadForecast(object sender)
        {
            await GetForecast(WeatherData.City);
        }
    }
}
